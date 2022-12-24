using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public event EventHandler<AbilityPanelManager.UnlockAbilityEventArgs> onAbilityClicked;

    public void OnAbilityClickedFunction(AbilitySaveObject _abilitySO)
    {
        onAbilityClicked?.Invoke(this, new AbilityPanelManager.UnlockAbilityEventArgs { _ability = _abilitySO });
    }
    
}
