using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnapBlock : MonoBehaviour {

    [SerializeField]
    private Transform baseBlock;

    private SnapParams snapParams;
    private bool mouseIsIn;
    private List<GameObject> parentBlocks = new List<GameObject>();

    void Start ()
    {
        snapParams = ScriptableObject.CreateInstance<SnapParams>();
        snapParams.snappableTransform = this.transform;
        snapParams.xFlip = baseBlock.position.x == transform.position.x;
        snapParams.yFlip = baseBlock.position.y == transform.position.y;
	}
    
    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.CompareTag("SnapBlock"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("SnapBlock"))
        {

            if(!parentBlocks.Contains(other.gameObject)){
                parentBlocks.Add(other.gameObject);

            }

            if(parentBlocks.Count > 1)
            {

                Destroy(gameObject);
            }

        }
    }

    private void OnMouseEnter()
    {
        if(mouseIsIn) return;
        mouseIsIn = true;

        string jsonVars = JsonUtility.ToJson(snapParams);
        EventManager.TriggerEvent("snap", jsonVars);
    }

    private void OnMouseExit()
    {
        mouseIsIn = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
