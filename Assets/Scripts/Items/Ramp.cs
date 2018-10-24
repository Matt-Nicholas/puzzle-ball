using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : GamePiece {

  public virtual void OnMouseOver() {
    // visual or audio effect to let play know they are ready to interact
    if(Input.GetMouseButtonDown(1)) {
      Interact();
    }
  }

  public override void Interact() {
    InvertX();
  }
}
