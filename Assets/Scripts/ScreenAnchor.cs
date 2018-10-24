using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnchor:MonoBehaviour {

  Camera cam;
  Transform bg;
  new Vector2 widthAndHeight;

  // Use this for initialization
  void Start() {
    cam = FindObjectOfType<Camera>();
    bg = transform.GetChild(0);
    widthAndHeight = new Vector2(bg.transform.localScale.x, bg.transform.localScale.y);
  }

  // Update is called once per frame
  void Update() {
    var v3Pos = new Vector3(0.0f, 1.0f, 1);
    var topRightCorner = FindObjectOfType<Camera>().ViewportToWorldPoint(v3Pos);


    transform.position = new Vector3(topRightCorner.x , topRightCorner.y, topRightCorner.z);
  }
}
