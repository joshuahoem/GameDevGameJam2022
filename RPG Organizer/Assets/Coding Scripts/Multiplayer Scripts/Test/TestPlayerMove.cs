using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestPlayerMove : MonoBehaviour
{
    PhotonView view;

    private void Start()
    {
        float randomX = Random.Range(-2, 2);
        float randomY = Random.Range(-5, 4);

        transform.position = new Vector2 (randomX, randomY);

        GetComponent<SpriteRenderer>().color = new Color(
        Random.Range(0f, 1f), 
        Random.Range(0f, 1f), 
        Random.Range(0f, 1f)
        );

        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!view.IsMine) {return;}
        if (Input.GetKey("up"))
        {
            transform.Translate(Vector2.up * Time.deltaTime);
        }
        
        if (Input.GetKey("down"))
        {
            transform.Translate(Vector2.down * Time.deltaTime);
        }
    }
}
