using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private float lakeSize;
    private Vector2 heroPos;
    private GameObject hero;
    [SerializeField] private float speedMultiplier = 4f;
    private float enemySpeed;
    DifficultyTracker difficultyTracker;
    [SerializeField] int difficultyMultiplier = 10;

    void Start()
    {
        speed = FindObjectOfType<Hero>().speed;
        lakeSize = FindObjectOfType<Lake>().lakeSize;
        hero = FindObjectOfType<Hero>().gameObject;
        difficultyTracker = FindObjectOfType<DifficultyTracker>();


        enemySpeed = (speed * (speedMultiplier + (difficultyMultiplier * difficultyTracker.difficulty)));


        //float enemySpeed = ((speed * speedMultiplier) / (2 * Mathf.PI)) * (2 * lakeSize * Mathf.PI);
    }

   
    void Update()
    {
        heroPos = hero.transform.position;

        MoveEnemy();
    }

     private void MoveEnemy() 
     {

        // if ((enemySpeed < speed * Time.deltaTime * speedMultiplier))
        // {
        //     enemySpeed = speed * Time.deltaTime * speedMultiplier;
        // }
        
        int targetAngle = Mathf.FloorToInt(Mathf.Rad2Deg * Mathf.Atan(heroPos.y / heroPos.x));
        int currentAngle = Mathf.FloorToInt(Mathf.Rad2Deg * Mathf.Atan(transform.position.y / transform.position.x));
        if (heroPos.x == 0) { targetAngle = 90 * Mathf.FloorToInt(Mathf.Clamp((lakeSize * heroPos.y),-1, 1));}
        if (transform.position.x == 0) { currentAngle = 90 * Mathf.FloorToInt(Mathf.Clamp((lakeSize * transform.position.y),-1, 1));}
        // float xPos = lakeSize * Mathf.Cos(targetAngle);
        // float yPos = lakeSize * Mathf.Sin(targetAngle);

        if (heroPos.x < 0) { targetAngle += 180; }
        else if (heroPos.x > 0 && heroPos.y < 0) { targetAngle += 360; }

        if (transform.position.x < 0) { currentAngle += 180; }
        else if (transform.position.x > 0 && transform.position.y < 0) { currentAngle += 360; }

        if (targetAngle == -90) {targetAngle += 360;}
        if (currentAngle == -90) {currentAngle += 360;}



        // Debug.Log(heroPos.y + " hero Y");
        // Debug.Log(heroPos.x + " hero X");
        // Debug.Log(transform.position.y + " current Y");
        // Debug.Log(transform.position.x + " current X");

        Debug.Log(targetAngle + " angle");
        Debug.Log(currentAngle + " current");

        // Debug.Log(enemySpeed);

        if (Mathf.Abs(currentAngle - targetAngle) < 2) { return; }

        if ((targetAngle >= 270 && currentAngle < 90) || (currentAngle >= 270 && targetAngle < 90) )
        {
            float direction = Mathf.Clamp((currentAngle - targetAngle),-1,1);
            transform.RotateAround(new Vector3(0,0,0), new Vector3(0,0,1), direction * enemySpeed);
            return;
        }

        if (currentAngle < targetAngle || currentAngle > targetAngle)
        {
            float direction = Mathf.Clamp((targetAngle - currentAngle),-1,1);
            transform.RotateAround(new Vector3(0,0,0), new Vector3(0,0,1), direction * enemySpeed);
        }

     }
}
