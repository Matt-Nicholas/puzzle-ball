using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2 {
  public class FlipObject : MonoBehaviour {

    Transform myTransform;
    void Start() {
      myTransform = transform;
    }

    // Update is called once per frame
    void Update() {
      if(Input.GetMouseButtonDown(0))
      {
        FlipHorizontal();
      }
      if(Input.GetMouseButtonDown(1))
      {
        FlipVertical();
      }
    }
    public void FlipHorizontal()
    {
      myTransform.eulerAngles = transform.eulerAngles + 180f * Vector3.up;
    }
    public void FlipVertical()
    {
      myTransform.eulerAngles = transform.eulerAngles + 180f * Vector3.left;
    }
  }
}