using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovementParabola : MonoBehaviour
{
    Vector2 startingPos;
    Vector2 destinationPos;
    [SerializeField] Transform[] destinations;
    [SerializeField] [Range(0.01f,1f)] float minSpeed; //cant be bigger than max
    [SerializeField] [Range(1.01f,10f)] float maxSpeed; //cant be smaller than min
    [SerializeField] int framesNeededToWait = 100;
    int framesWaited;
    [SerializeField] float howCloseBeforeNewDestination;


    private void Start() 
    {
        GetNewDestination();
    }

    private void Update() 
    {
        float currentDistance = Vector2.Distance(transform.position, startingPos);
        float totalDistance = Vector2.Distance(startingPos, destinationPos);
        float arcSpeed = (4 * (minSpeed - maxSpeed))/Mathf.Pow(totalDistance+0.01f,2);
        float speed = arcSpeed * Mathf.Pow(currentDistance - (totalDistance/2), 2) + maxSpeed;

        transform.position = Vector2.MoveTowards(transform.position, destinationPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destinationPos) < howCloseBeforeNewDestination)
        {
            framesWaited += 1;
            if (framesWaited>= framesNeededToWait)
            {
                GetNewDestination();
            }
        }
    }

    private void GetNewDestination()
    {
        framesWaited = 0;
        int randIndex = Random.Range(0,destinations.Length);
        destinationPos = destinations[randIndex].position;
        startingPos = transform.position;
    }

}
