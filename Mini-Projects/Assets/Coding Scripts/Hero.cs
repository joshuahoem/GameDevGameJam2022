using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    LevelManager levelManager;
    [SerializeField] GameObject winScreen;
    bool win = false;


    private void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (win) {return;}
        MoveHero();        
    }

    private void MoveHero()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0)
        {
            Vector2 newPos = new Vector2(Input.GetAxisRaw("Horizontal") + 
                transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, 
                newPos, speed);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical"))>0)
        {
            Vector2 newPos = new Vector2(transform.position.x, 
                Input.GetAxisRaw("Vertical") + transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, 
                newPos, speed);
        }

        // Debug.Log( speed * Time.deltaTime + " hero");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (win) {return;}
        //restart level
        if (other.name == FindObjectOfType<Enemy>().gameObject.name)
        {
            FindObjectOfType<DifficultyTracker>().AddDeath();
            levelManager.RestartLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        win = true;
        winScreen.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
