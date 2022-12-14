using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;

    private void Start() {
        winScreen.SetActive(false);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        FindObjectOfType<DifficultyTracker>().IncreaseDifficulty();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
