using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : IData
{
    public int currentLevel;

    public PlayerData(Player player) {
        currentLevel = player.currentLevel;
    }

}
