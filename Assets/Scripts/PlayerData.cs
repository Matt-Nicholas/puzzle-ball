using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{

    private string _playerName = "";
    private int _highestLevelUnlocked = 1;
    private int _stars = 0;

    public string PlayerName { get { return _playerName; } set { _playerName = value; } }
    public int Level { get { return _highestLevelUnlocked; } set { _highestLevelUnlocked = value; } }
    public int Stars { get { return _stars; } set { _stars = value; } }


    public PlayerData(string playerName, int level, int stars)
    {
        this._playerName = playerName;
        this._highestLevelUnlocked = level;
        this._stars = stars;
    }
}
