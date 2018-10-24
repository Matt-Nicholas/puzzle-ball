using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
  public class ItemController:MonoBehaviour
  {


    public void FlipHorizontal()
    {
      transform.eulerAngles = transform.eulerAngles + 180f * Vector3.up;
    }  
  }
}