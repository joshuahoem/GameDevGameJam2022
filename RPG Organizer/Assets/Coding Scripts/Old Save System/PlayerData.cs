using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool unlockedClassCleric;
    public bool unlockedClassGladiator;
    public bool unlockedClassHerbalist;
    public bool unlockedClassTrapper;
    public bool unlockedClassWarlock;
    public bool unlockedClassWeaponMage;
    public bool unlockedRaceFaerie;
    public bool unlockedRaceFuzzbold;
    public bool unlockedRaceGargoyle;
    public bool unlockedRaceSkeleton;

    public PlayerData (Player player)
    {
    //Classes
        unlockedClassCleric = player.unlockedClassCleric;
        unlockedClassGladiator = player.unlockedClassGladiator;
        unlockedClassHerbalist = player.unlockedClassHerbalist;
        unlockedClassTrapper = player.unlockedClassTrapper;
        unlockedClassWarlock = player.unlockedClassWarlock;
        unlockedClassWeaponMage = player.unlockedClassWeaponMage;

    //Races
        unlockedRaceFaerie = player.unlockedRaceFaerie;
        unlockedRaceFuzzbold = player.unlockedRaceFuzzbold;
        unlockedRaceGargoyle = player.unlockedRaceGargoyle;
        unlockedRaceSkeleton = player.unlockedRaceSkeleton;
    }
    


}
