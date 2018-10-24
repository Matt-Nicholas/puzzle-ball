using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fan:GamePiece
{

    public Vector3 force = Vector3.zero;

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    // Internal list that tracks objects that enter this object's "zone"
    private List<Collider> objects = new List<Collider>();

    void FixedUpdate()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            Rigidbody body = objects[i].attachedRigidbody;
            body.AddForce(force);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        objects.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        objects.Remove(other);
    }

    public virtual void OnMouseOver()
    {
        // visual or audio effect to let play know they are ready to interact
        if(Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        InvertX();

        float newX = -force.x;

        force = new Vector3(newX, force.y, force.z);
        //transform.localScale = new Vector3(1, 1, -1);

    }
}

