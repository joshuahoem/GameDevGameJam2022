using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirectionTest : MonoBehaviour
{
    [SerializeField] GameObject arrowObject;
    [SerializeField] GameObject startingObject;
    [SerializeField] GameObject endingObject;
    [SerializeField] Transform parentTransform;
    [SerializeField] float scaleX;


    GameObject arrowInstance;

    private void Start() {
        GameObject _arrow = Instantiate(arrowObject, transform.position, Quaternion.identity);
        _arrow.transform.SetParent(parentTransform,false);
        arrowInstance = _arrow;
    }
    private void Update() {
        UpdateArrow();
    }

    private void UpdateArrow()
    {
        Vector2 spawnLocation = Vector3.Lerp(startingObject.transform.position, endingObject.transform.position, 0.5f);

        float deltaX = endingObject.transform.position.x - startingObject.transform.position.x;
        float deltaY = endingObject.transform.position.y - startingObject.transform.position.y;
        float radians = Mathf.Atan(deltaY / deltaX);
        float degrees = radians * (180f / Mathf.PI);
        if (deltaX < 0) { degrees += 180; }

        Vector3 spawnRotation = new Vector3(0,0,degrees);

        float scaleFactor = (Vector2.Distance(startingObject.transform.position, endingObject.transform.position) / scaleX) * 0.75f - 0.55f;

        // GameObject arrow = Instantiate(arrowObject, transform.position, Quaternion.identity);
        arrowInstance.transform.eulerAngles = spawnRotation;
        arrowInstance.transform.position = spawnLocation;
        // GameObject.FindWithTag("ArrowBody").transform.localScale = new Vector3(scaleFactor,1,1);
        arrowInstance.transform.localScale = new Vector3(scaleFactor,1,1);

        // Debug.Log(Vector2.Distance(startingObject.transform.position, endingObject.transform.position));

    }
}
