using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xMin, xMax, yMin, yMax;

    bool move = false;
    Vector3 targetPos;

    private void Start() 
    {
        CameraMoveToNecroMan();
    }

    private void Update() 
    {
        CameraMove(); 
        CameraMoveScript();   
    }

    private void CameraMove()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || 
            Mathf.Abs(Input.GetAxis("Vertical")) > 0  )
            {
                float xValue = mainCamera.transform.position.x;
                float yValue = mainCamera.transform.position.y;
                xValue += Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
                yValue += Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

                float x = Mathf.Clamp(xValue, xMin, xMax);
                float y = Mathf.Clamp(yValue, yMin, yMax);


                mainCamera.transform.position = new Vector3(x,y,-10);
            }
    }

    public void CameraMoveToNecroMan()
    {
        move = true;
        float xValue = GameObject.Find("NecroMan").GetComponent<NecroMan>().transform.position.x;
        float yValue = GameObject.Find("NecroMan").GetComponent<NecroMan>().transform.position.y;
        
        float x = Mathf.Clamp(xValue, xMin, xMax);
        float y = Mathf.Clamp(yValue, yMin, yMax);
        
        targetPos = new Vector3(x,y,-10);
    }

    public void CameraMoveToTarget(float xValue, float yValue)
    {
        move = true;

        float x = Mathf.Clamp(xValue, xMin, xMax);
        float y = Mathf.Clamp(yValue, yMin, yMax);

        targetPos = new Vector3(x,y,-10);
    }

    private void CameraMoveScript()
    {
        if (!move) {return;}

        transform.position = Vector3.MoveTowards(mainCamera.transform.position, 
            targetPos, Time.deltaTime * moveSpeed);

        if (mainCamera.transform.position == targetPos)
        {
            move = false;
        }
    }
}
