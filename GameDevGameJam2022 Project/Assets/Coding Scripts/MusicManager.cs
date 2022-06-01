using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip song1, song2, song3;
    int currentSceneIndex, oldSceneIndex;
    AudioSource audioSource;

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

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadSong() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex == 0 || currentSceneIndex == 1 || 
            currentSceneIndex == 2 || currentSceneIndex == 5)
        {
            if (oldSceneIndex == 0 || oldSceneIndex == 1 ||
                oldSceneIndex == 2 || oldSceneIndex == 4 || 
                oldSceneIndex == 5)
            {
                    
                if (audioSource.clip != song1)
                {
                    audioSource.clip = song1;
                    audioSource.Play();
                }
                else
                {
                    //Debug.Log("keep don't do anything");
                }
            }
        }
        else
        {
            if (currentSceneIndex == 3)
            {
                audioSource.clip = song2;
            }
            else if (currentSceneIndex == 4)
            {
                audioSource.clip = song3;
            }

            audioSource.Play();
        }

        oldSceneIndex = currentSceneIndex;
    }
}
