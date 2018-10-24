using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_dev : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    Vector3 pos = transform.position;

    if(Input.GetKey(KeyCode.RightArrow)) {
      float newX = pos.x + (20 * Time.deltaTime);
      transform.position = new Vector3(newX, pos.y, pos.z);
    }
    else if(Input.GetKey(KeyCode.LeftArrow)) {
      float newX = pos.x - (20 * Time.deltaTime);
      transform.position = new Vector3(newX, pos.y, pos.z);
    }
  }
}
