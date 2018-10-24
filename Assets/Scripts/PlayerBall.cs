using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBall:MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 startingPosition;

    private bool inPlay = false;
    private float goalDelay = 0;
    public Vector3 posA;
    public Vector3 posB;

    private void Start()
    {
        
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        transform.position = startingPosition;
        ConstrainRB();
    }

    void Update()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red);
    }

    public void ConstrainRB()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnconstrainRB()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }

    public void HitGoal(Vector3 goalPos)
    {
        StartCoroutine(WaitAndMove(goalDelay, transform.position, goalPos));
    }

    void InitializeReferences()
    {

    }

    private void OnMouseOver()
    {
        // Highlight
        if(Input.GetMouseButtonDown(0))
        {
            if(!inPlay)
            {
                UnconstrainRB();
                inPlay = true;
            }
            else
            {
                transform.position = startingPosition;
                inPlay = false;
                ConstrainRB();
            }
        }
    }

    IEnumerator WaitAndMove(float delayTime, Vector3 ballPos, Vector3 goalPos)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while(Time.time - startTime <= 1)
        { // until one second passed
            transform.position = Vector3.Lerp(ballPos, goalPos, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        if(transform.position != goalPos) transform.position = goalPos;

        ConstrainRB();
    }
}
