using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float moveSpeed = 5f;

    bool move = false;
    Vector3 targetPos;

    private void Start() 
    {
        CameraMoveToStart();
    }
    
    private void Update() 
    {
        CameraMove(); 
        StartingCameraPosition();   
    }

    private void CameraMove()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || 
            Mathf.Abs(Input.GetAxis("Vertical")) > 0  )
            {
                float x = mainCamera.transform.position.x;
                float y = mainCamera.transform.position.y;
                x += Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
                y += Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

                mainCamera.transform.position = new Vector3(x,y,-10);
            }
    }

    public void CameraMoveToStart()
    {
        move = true;
        float x = GameObject.Find("NecroMan").GetComponent<NecroMan>().transform.position.x;
        float y = GameObject.Find("NecroMan").GetComponent<NecroMan>().transform.position.y;
        targetPos = new Vector3(x,y,-10);
    }

    private void StartingCameraPosition()
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
