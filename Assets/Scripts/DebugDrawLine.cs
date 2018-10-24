using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
  public class DebugDrawLine:MonoBehaviour
  {



    // Update is called once per frame
    void Update()
    {
      Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red);
    }
  }
}