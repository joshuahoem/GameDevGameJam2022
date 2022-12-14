using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyTracker : MonoBehaviour
{
    public int difficulty = 0;
    public int deathCount = 0;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI deathText;

    private void Awake() 
    {
        //planketon (dont destory if there is 1, destroy if there is more than 1)
        int numDifficultyTracker = FindObjectsOfType<DifficultyTracker>().Length;
        if (numDifficultyTracker>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        levelText.text = (difficulty+1).ToString();
        deathText.text = deathCount.ToString();

    }

    public void IncreaseDifficulty()
    {
        difficulty++;
        levelText.text = (difficulty+1).ToString();
    }

    public void AddDeath()
    {
        deathCount++;
        deathText.text = deathCount.ToString();
    }
}
