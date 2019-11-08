using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePiece:MonoBehaviour
{
    public string name = "unknown";
    [SerializeField]
    ValidSnapCheck _validSnapCheck;

    private Vector3 screenPoint;
    private Vector3 startingPos = new Vector3();
    private LayerMask layerMask;
    private Quaternion startingRotation = new Quaternion();

    private bool isSnapped = false;
    private bool inSnapRange = false;

    private float maxHitDistance = 0.5f;
    private int currentZRot = 0;


    private void Awake()
    {

        layerMask = LayerMask.NameToLayer("Snappable");
        startingPos = transform.position;
        startingRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    private void OnMouseUp()
    {
        if(!isSnapped)
        {
            transform.position = startingPos;
            transform.rotation = startingRotation;
        }
    }

    void OnMouseDrag()
    {
        inSnapRange = false;

        Hit hit = GetClosestHit();
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 dragPos = Camera.main.ScreenToWorldPoint(curScreenPoint);

        if(hit != null && (Vector3.Distance(dragPos, hit.hit.transform.position + hit.hit.normal) < 1) && ValidSnapPosition())
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, hit.zRotation);
            currentZRot = hit.zRotation;

            double x = hit.hit.normal.x == 0 ? Math.Round(dragPos.x * 2, MidpointRounding.AwayFromZero) / 2 : hit.hit.transform.position.x + (((Math.Abs(hit.hit.transform.localScale.x) / 2) + (Math.Abs(transform.localScale.x) / 2)) * Math.Sign(hit.hit.normal.x));
            double y = hit.hit.normal.y == 0 ? Math.Round(dragPos.y * 2, MidpointRounding.AwayFromZero) / 2 : hit.hit.transform.position.y + (((Math.Abs(hit.hit.transform.localScale.y) / 2) + (Math.Abs(transform.localScale.y) / 2)) * Math.Sign(hit.hit.normal.y));
              
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
                    closestHit = new Hit(new RaycastHit(), ray.Key)
                    {
                        hit = hit
                    };
                }
            }
        }
        return closestHit;
    }

    private bool ValidSnapPosition()
    {
        return _validSnapCheck.IsValid();

        List<Vector3> rays = new List<Vector3>()
            {
                {new Vector3(0, 0, 1) },
                {new Vector3(0, 0, -1) }
            };

        foreach(var ray in rays)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(ray), out hit, maxHitDistance, layerMask))
            {
                return false;
            }
        }

        return true;
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