using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public bool unlockedClassCleric = false;
    [SerializeField] public bool unlockedClassGladiator = false;
    [SerializeField] public bool unlockedClassHerbalist = false;
    [SerializeField]public bool unlockedClassTrapper = false;
    [SerializeField] public bool unlockedClassWarlock = false;
    [SerializeField] public bool unlockedClassWeaponMage = false;
    [SerializeField] public bool unlockedRaceFaerie = false;
    [SerializeField] public bool unlockedRaceFuzzbold = false;
    [SerializeField] public bool unlockedRaceGargoyle = false;
    [SerializeField] public bool unlockedRaceSkeleton = false;

    private void Start() {
        LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        unlockedClassCleric = data.unlockedClassCleric;
        unlockedClassGladiator = data.unlockedClassGladiator;
        unlockedClassHerbalist = data.unlockedClassHerbalist;
        unlockedClassTrapper = data.unlockedClassTrapper;
        unlockedClassWarlock = data.unlockedClassWarlock;
        unlockedClassWeaponMage = data.unlockedClassWeaponMage;
        unlockedRaceFaerie = data.unlockedRaceFaerie;
        unlockedRaceFuzzbold = data.unlockedRaceFuzzbold;
        unlockedRaceGargoyle = data.unlockedRaceGargoyle;
        unlockedRaceSkeleton = data.unlockedRaceSkeleton;
        
    }
}
