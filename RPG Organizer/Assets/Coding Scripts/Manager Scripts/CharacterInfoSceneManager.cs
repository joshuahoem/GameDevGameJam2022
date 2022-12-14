using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SaveLoadManager>().LoadCharacter();
    }

}
