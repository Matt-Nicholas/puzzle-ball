using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2 {
  public class DisplayEndLevelPanel : MonoBehaviour {


    //private void Start()
    //{
    //  DisplayEndLevelPanel.ShowButtons();
    //}

    //public static void ShowButtons()
    //{
      
    //}

    private void OnGUI()
    {
      GUI.Button(new Rect(100, 100, 20, 20), "1");
      GUI.Button(new Rect(120, 100, 20, 20), "2");
      GUI.Button(new Rect(140, 100, 20, 20), "3");
      GUI.Button(new Rect(160, 100, 20, 20), "4");
      GUI.Button(new Rect(180, 100, 20, 20), "5");
      GUI.Button(new Rect(100, 130, 20, 20), "6");
      GUI.Button(new Rect(120, 130, 20, 20), "7");
      GUI.Button(new Rect(140, 130, 20, 20), "8");
      GUI.Button(new Rect(160, 130, 20, 20), "9");
      GUI.Button(new Rect(180, 130, 20, 20), "10");
    }
  }
}