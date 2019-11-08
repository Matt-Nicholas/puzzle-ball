using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    public void Play()
    {
        gm.LoadMaxLevel();
    }

}
