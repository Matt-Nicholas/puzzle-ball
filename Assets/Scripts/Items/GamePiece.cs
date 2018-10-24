using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePiece:MonoBehaviour
{
    public string name = "unknown";

    private LayerMask layerMask;
    private Vector3 startingPos = new Vector3();
    Quaternion startingRotation = new Quaternion();
    private Vector3 screenPoint;
    private bool isDragging;
    private bool isSnapped = false;
    private bool inSnapRange = false;

    private float maxHitDistance = 0.7f;
    private int currentZRot = 0;
    
    private UnityEvent snapToSurfaceEvent;
    private UnityAction<string> _eventListener;

    private void Awake()
    {
        
        layerMask = LayerMask.NameToLayer("Snappable");
        startingPos = transform.position;
        startingRotation = transform.rotation;
    }

    private void Update()
    {
        inSnapRange = false;

        Hit hit = GetClosestHit();
        if(hit != null)
        {
            inSnapRange = true;

            transform.Rotate(transform.rotation.x, transform.rotation.y, hit.zRotation);
            currentZRot = hit.zRotation;

            //float halfWidth = hit.hit.transform.GetComponent<Renderer>().bounds.size.x;
            //float halfHeight = hit.hit.transform.GetComponent<Renderer>().bounds.size.y;


            //if(currentZRot == 0)
            //{
            //    snapPos = new Vector3(hit.hit.transform.position.x, hit.hit.transform.position.y + halfHeight, hit.hit.transform.position.z);
            //}

            //if(!isDragging)
            //{
            //    transform.position = snapPos;
            //}
            //else if(currentZRot == 90)
            //{
            //    snapPos = new Vector3(hit.hit.transform.position.x + (rt.rect.width / 2), hit.hit.transform.position.y, hit.hit.transform.position.z);
            //}
            //else if(currentZRot == 180)
            //{
            //    snapPos = new Vector3(hit.hit.transform.position.x + rt.rect.width, hit.hit.transform.position.y - (rt.rect.height / 2), hit.hit.transform.position.z);
            //}
            //else if(currentZRot == 270)
            //{
            //    snapPos = new Vector3(hit.hit.transform.position.x - (rt.rect.width / 2), hit.hit.transform.position.y, hit.hit.transform.position.z);
            //}
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if(!isSnapped)
        {
            transform.position = startingPos;
            transform.rotation = startingRotation;
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 dragPos = Camera.main.ScreenToWorldPoint(curScreenPoint);
        if(inSnapRange)
        {
            double x = Math.Round(dragPos.x * 2, MidpointRounding.AwayFromZero) / 2;
            double y = Math.Round(dragPos.y * 2, MidpointRounding.AwayFromZero) / 2;
            Vector3 snapPos = new Vector3((float)x, (float)y, dragPos.z);

            transform.position = new Vector3(snapPos.x, snapPos.y, 0);
            isSnapped = true;
        }
        else
        {
            transform.position = new Vector3(dragPos.x, dragPos.y, 0);
            isSnapped = false;
        }
    }

    public void OnMouseOver()
    {
        // visual or audio effect to let play know they are ready to interact
        if(Input.GetMouseButtonDown(1))
        {

        }
    }

    private Hit GetClosestHit()
    {
        Hit closestHit = null;

        Dictionary<int, Vector3> rays = new Dictionary<int, Vector3>()
        {
            {0, new Vector3(0, -1, 0) },
            {90, new Vector3(1, 0, 0) },
            {180, new Vector3(0, 1, 0) },
            {270, new Vector3(-1, 0, 0) }
        };

        foreach(var ray in rays)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(ray.Value), out hit, maxHitDistance, layerMask))
            {
                if(closestHit == null || hit.distance < closestHit.hit.distance)
                {
                    closestHit = new Hit(hit, ray.Key);
                }
            }
        }
        return closestHit;
    }

    public virtual void Interact()
    {
        // Play rotation sound
    }

    public void InvertX()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    
    public void InvertY()
    {
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    public void RotateDegrees(float degrees)
    {
        transform.eulerAngles = transform.eulerAngles + degrees * Vector3.forward;
    }

    class Hit
    {
        public RaycastHit hit;
        public int zRotation;

        public Hit(RaycastHit hit, int rot)
        {
            hit = hit;
            zRotation = rot;
        }
    }
}
