using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : GamePiece {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void OnMouseOver() {
    // visual or audio effect to let play know they are ready to interact
    if(Input.GetMouseButtonDown(1)) {
      Interact();
    }
  }

  public override void Interact() {
    base.Interact();

    RotateDegrees(90);
  }
}
