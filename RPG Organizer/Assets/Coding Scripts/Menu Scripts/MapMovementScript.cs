using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovementScript : MonoBehaviour
{
    private Vector3 newPosition;
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    [SerializeField] [Range(0.01f,1f)] private float lerpSpeed = 0.05f;

    private void Start() 
    {
        newPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * lerpSpeed);

        if(Vector3.Distance(transform.position, newPosition) < 1f)
        {
            GetNewPosition();
        }    
    }

    void GetNewPosition()
    {
        var xPos = Random.Range(min.x, max.x);
        var yPos = Random.Range(min.y, max.y);

        newPosition = new Vector3(xPos,yPos,0);
    }


}
