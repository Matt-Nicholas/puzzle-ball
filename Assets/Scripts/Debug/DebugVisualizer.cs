using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVisualizer : MonoBehaviour {

    public bool drawTrail;

    private float _lineTimer;
    private Vector3 _previousPos = new Vector3();
	
    
	void Start () {
        _previousPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       
        if(drawTrail) DrawTrail();
	}

    void DrawTrail()
    {
        _lineTimer += Time.deltaTime;
        if(_lineTimer < .2f) return;
        if(_previousPos.Equals(transform.position)) return;
        _lineTimer = 0f;

        Color c = new Color(255, 0, 0);
        Debug.DrawLine(_previousPos, transform.position, Color.red, 30);
        _previousPos = transform.position;
    }
}
