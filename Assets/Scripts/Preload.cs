using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {

	
	void Start () {

        //SceneManager.LoadScene("Main");

        if(FindObjectOfType<GameManager>() == null)
        {

            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name.Equals("Main")) continue;

                Scene sceneToReload = SceneManager.GetSceneAt(i);
                if(sceneToReload == null) continue;
                GameManager.Instance.Preload(sceneToReload);
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
