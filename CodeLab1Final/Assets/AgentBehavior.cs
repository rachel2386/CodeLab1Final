using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Vector2 _destination;
    public float moveSpeed = 20;
    [HideInInspector] public bool autoControl = true;
    private Camera _camera;
    private Vector3 touchPos;
    private float camHeight;
    private float camWidth;

    void Start()
    {
        _camera = Camera.main;
        myRB = GetComponent<Rigidbody2D>();
        camHeight = _camera.orthographicSize * 2;
        camWidth = camHeight * _camera.aspect;
        
        _destination = new Vector2(Random.Range(0, camWidth), camHeight - transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if  (Input.touchCount == 0)
        {
            GameManager.HoldingAgent = false;
            autoControl = true;
        }
       
        
        if (autoControl)
        {
           //if position != destination
            MoveTowardsDest();
        }
        else
        {
           FollowTouch();
        }

      
    }

    void MoveTowardsDest()
    {
        myRB.position = Vector2.MoveTowards(myRB.position, _destination, Mathf.Sin(Time.deltaTime)* moveSpeed);
        if (myRB.position.x >= camWidth || myRB.position.x < -camWidth)
        {
            myRB.velocity = -myRB.velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //move towards the opposite of collided agent
        if(autoControl)
        myRB.AddForce(-(other.rigidbody.position - myRB.position).normalized * moveSpeed/2, ForceMode2D.Impulse);
    }

    private void FollowTouch()
    {
        
        Vector3 touchPosition = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        touchPosition.z = transform.position.z;
        transform.position = touchPosition;
        Debug.DrawRay(Vector3.zero, _camera.ScreenToWorldPoint(Input.GetTouch(0).position), Color.red);

    }
}
