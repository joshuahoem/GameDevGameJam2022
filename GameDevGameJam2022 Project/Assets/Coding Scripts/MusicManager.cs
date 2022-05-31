using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip song1, song2, song3;
    int currentSceneIndex, oldSceneIndex;
    private void Awake() {
        //planketon (dont destory if there is 1, destroy if there is more than 1)
        int numMusicManager = FindObjectsOfType<MusicManager>().Length;
        if (numMusicManager>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex == 0 || currentSceneIndex == 1 || currentSceneIndex == 2 || 
            currentSceneIndex == 5 && oldSceneIndex == 0 || oldSceneIndex == 1 || 
            oldSceneIndex == 2 || oldSceneIndex == 5)
        {
            Debug.Log("keep don't do anything");
            return;
        }
        else
        {
            if (currentSceneIndex == 3)
            {
                GetComponent<AudioSource>().clip = song2;
            }
            else if (currentSceneIndex == 4)
            {
                GetComponent<AudioSource>().clip = song3;
            }
        }

        oldSceneIndex = currentSceneIndex;
    }
}
