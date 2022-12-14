using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingletonManager : MonoBehaviour
{
    public string selectedCharacter;
    public int test = 0;
    private void Awake() 
    {
        //planketon (dont destory if there is 1, destroy if there is more than 1)
        int numSingletonManager = FindObjectsOfType<SingletonManager>().Length;
        if (numSingletonManager>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    public void RememberCharacterFileNumber()
    {  
        test++;
        this.selectedCharacter = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("test");
    }
}