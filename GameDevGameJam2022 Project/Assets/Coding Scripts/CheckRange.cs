using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour
{
    GameObject targetedPiece;
    [SerializeField] GameObject availableSpacesPrefab;
    public List<GameObject> tiles = new List<GameObject>();
    BoardManager boardManager; 

    private void Start() {
        boardManager = FindObjectOfType<BoardManager>();
    }

    public void Range(int pieceCheckValue, int range, bool on)
    {
        if (!on)
        {
            foreach (GameObject tile in tiles)
            {
                Destroy(tile.gameObject);
            }

            // tiles.Clear();

            return;
        }

        NecroMan[] pieces = FindObjectsOfType<NecroMan>();

        foreach (NecroMan piece in pieces)
        {
            if (piece.pieceValue == pieceCheckValue)
            {
                targetedPiece = piece.gameObject;
                break;
            }
        }

        int x = Mathf.FloorToInt(targetedPiece.transform.position.x);
        int y = Mathf.FloorToInt(targetedPiece.transform.position.y);

        for (int a = x - range; a <= x + range; a++)
        {
            for (int b = y - range; b <= y + range; b++)
            {
                if (boardManager.gridMaker.GetValue(a,b) == 0)
                {
                    GameObject tile = Instantiate(availableSpacesPrefab, new Vector3(a,b) + new Vector3(0.5f,0.5f), Quaternion.identity);
                    tiles.Add(tile);
                }
            }
        }

    }

    public bool CheckSpaces(Vector3 targetPos)
    {
        foreach (GameObject tile in tiles)
        {
            if (RoundVector(targetPos) == RoundVector(tile.transform.position))
            {
                tiles.Clear();
                return true;
            }
        }

        tiles.Clear();
        return false;
    }

    private Vector3 RoundVector(Vector3 target)
    {
        float x,y;

        x = Mathf.Floor(target.x) + 0.5f;
        y = Mathf.Floor(target.y) + 0.5f;

        return new Vector3 (x,y);
        
    }
}
