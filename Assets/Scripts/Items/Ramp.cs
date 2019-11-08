using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ramp:GamePiece
{
    
    public virtual void OnMouseOver()
    {
        // TODO: visual or audio effect to let play know they are ready to interact.. If game goes to something other thatn mobile....


        if(Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        InvertX();
    }
}
