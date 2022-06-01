using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Works only on Canvas images/ canvas elements

    bool moreThanOneObject = false;
    [SerializeField] RectTransform rectTransform;
    Vector3 startingPos;
    [SerializeField] private Canvas canvas;
    static GridMaker<int> grid;
    MouseControl mouseControl;
    CreatureList creatureList;
    InventoryItemData thisItemData;
    CheckRange checkRange;
    

    private void Start() 
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        grid = FindObjectOfType<BoardManager>().gridMaker;
        mouseControl = FindObjectOfType<MouseControl>();
        thisItemData = GetComponent<ItemSlot>().recordedData;
        creatureList = FindObjectOfType<CreatureList>();
        checkRange = FindObjectOfType<CheckRange>();
        startingPos = rectTransform.position;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("click");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("begin");
        rectTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f); //bring down to size of square
        
        if (!mouseControl)
        {
            mouseControl = FindObjectOfType<MouseControl>();
        }
        mouseControl.hoverSquareEnabled = true;
        

        if (!checkRange)
        {
            checkRange = FindObjectOfType<CheckRange>();
        }
        checkRange.Range(1,1,true); //1 for necromancer, 1 for range
        
        //subtract from inventory
        if (InventorySystem.current.GetStackSize(thisItemData)==1)
        {
            moreThanOneObject = false;
            FindObjectOfType<InventorySystem>().Remove(thisItemData);
            return;
        }
        else
        {
                
            int numOfIterations = 0;
            moreThanOneObject = true;

            while (InventorySystem.current.GetStackSize(thisItemData) > 0)
            {
                numOfIterations++;
                FindObjectOfType<InventorySystem>().Remove(thisItemData);
            }

            for (int i = 0; i < numOfIterations-1; i++)
            {
                FindObjectOfType<InventorySystem>().Add(thisItemData);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) {canvas = FindObjectOfType<Canvas>();}
        rectTransform.anchoredPosition += (eventData.delta / canvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mouseControl.hoverSquareEnabled = false;
        Vector3 targetPosition = GetMouseWorldPosition();
        checkRange.Range(1,1,false); //1 for necromancer, 1 for range


        //check if in place that can be spawned
        if (checkRange.CheckSpaces(targetPosition) && grid.InBounds(targetPosition) && grid.GetValue(targetPosition) == 0
            && (!(GameObject.Find("NecroMan").GetComponent<NecroMan>().summonedThisTurn)))
        {
            SpawnCreature(targetPosition);
        }
        else
        {
            //return to inventory
            if (!moreThanOneObject)
            {
                FindObjectOfType<InventoryManager>().inventoryItems.Remove(gameObject);
                FindObjectOfType<InventorySystem>().Add(thisItemData);

                Destroy(gameObject);
            }
        }
    }

    

    private void SpawnCreature(Vector3 targetPos)
    {
        //
        if (creatureList.creatures.TryGetValue(thisItemData.displayName, out GameObject creatureToSpawn))
        {
            //success
            GameObject newpiece = Instantiate (creatureToSpawn, RoundVector(targetPos), Quaternion.identity);
            NecroMan necroMan = newpiece.GetComponent<NecroMan>();

            if (necroMan.sfxPieceSummoning != null)
            {
                necroMan.GetComponent<AudioSource>().PlayOneShot(necroMan.sfxPieceSummoning, .5f);
            }

            if (grid == null)
            {
                grid = FindObjectOfType<BoardManager>().gridMaker;
            }
            grid.SetValue(targetPos, necroMan.pieceValue);
            necroMan.team = NecroMan.Team.Player;

            FindObjectOfType<TeamManager>().playerTeam.Add(newpiece);
            FindObjectOfType<TeamManager>().allPieces.Add(newpiece);

            FindObjectOfType<InventoryManager>().inventoryItems.Remove(gameObject);

            GameObject.Find("NecroMan").GetComponent<NecroMan>().summonedThisTurn = true;
        }
        else
        {
            //failure
            Debug.Log("failure");
        }

        InventorySystem system = FindObjectOfType<InventorySystem>();
        int stackSize = system.GetStackSize(thisItemData);

        if (stackSize <= 0 || moreThanOneObject)
        {
            StartCoroutine(DestroyObject());
        }
        else if (stackSize >= 1 && !moreThanOneObject)
        {
            transform.position = startingPos;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());

            rectTransform.localScale = new Vector3(1f, 1f, 1f); //bring back to normal size
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }



    public static Vector3 GetMouseWorldPosition() 
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ() 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) 
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) 
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private Vector3 RoundVector(Vector3 target)
    {
        float x,y;

        x = Mathf.Floor(target.x) + 0.5f;
        y = Mathf.Floor(target.y) + 0.5f;

        return new Vector3 (x,y);
        
    }
}
