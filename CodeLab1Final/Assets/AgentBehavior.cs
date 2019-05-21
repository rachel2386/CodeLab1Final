using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


//match 3 game
//chara move to a random spot on top of screen --check
//player touch and drag to move charas--check
//reset pos when reaching destination--check
//if 3 charac are with the same type, Destroy--check


public class AgentBehavior : MonoBehaviour
{
    private float _activateTimer = 3f; // not matchable until this amount of time has passed. (To prevent auto match on spawn)
    
    //movement stuff
    private Rigidbody2D myRB;
    private Vector2 _destination;
    public float moveSpeed = 20;
    [HideInInspector] public bool autoControl = true;
    private GameManager _gameManager;
    
    //camera reference
    private Camera _camera;
    private float camHeight;
    private float camWidth;
    
    //touchPos for manual control
    private Vector3 touchPos;
    
    //match detection (sphere overlap) & matched list
    private Collider2D[] stuffInSphere;
    private List<GameObject> _charWithSameType = new List<GameObject>();
    private int myType;

    private void Start()
    {
        _camera = Camera.main;
        camHeight = _camera.orthographicSize * 2;
        camWidth = camHeight/2 * _camera.aspect;
        
        
        myRB = GetComponent<Rigidbody2D>();
       _destination = new Vector2(Random.Range(-camWidth + transform.localScale.x, camWidth -transform.localScale.x), camHeight - transform.localScale.y);
        moveSpeed *= Random.Range(0.6f, 1f);

        myType = GetComponent<CharacterType>().typeIndex;

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

  private void Update()
    {
//      Debug.DrawLine(Vector3.right * -camWidth,Vector3.right *  camWidth,Color.yellow);
//      Debug.DrawLine(Vector3.up * -camHeight/2,Vector3.up *  camHeight/2,Color.magenta);

        if (GameManager.gameState != 0) return;
        if (Input.touchCount == 0)
        {
            TouchManager.HoldingAgent = false;
            autoControl = true;
        }


        if (autoControl)
        {
            if (Vector2.Distance((Vector2) transform.position, _destination) >= 0.5f)
            {
                MoveTowardsDest();
                if (MatchableTimer())
                {
                    DetectMatch();
                }
            }
            else
                RecycleToPool(gameObject);
        }
        else
        {
            FollowTouch();
        }

    }

   private bool MatchableTimer()
    {
        bool matchable = false;
        if (_activateTimer > 0)
            _activateTimer -= Time.deltaTime;
        else
            matchable = true;
        
        return matchable;
    }

    private void MoveTowardsDest()
    {
        myRB.position = Vector2.MoveTowards(myRB.position, _destination, Mathf.Sin(Time.deltaTime)* moveSpeed);
        if (myRB.position.x >= camWidth || myRB.position.x < -camWidth)
        {
            Vector2 newVel = myRB.velocity;
            //newVel.x--;
            myRB.velocity = -newVel;
            //print(newVel);
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
 
    }

    private void RecycleToPool(GameObject objToRecycle)
    {
        CharacterSpawner.CharacterPool.Add(objToRecycle);
        _gameManager.damagePlayer();
        gameObject.SetActive(false);
    }
    
    
    
    private void DetectMatch()
    {
        
        stuffInSphere = Physics2D.OverlapCircleAll(transform.position, 0.5f);
       
        if(stuffInSphere.Length > 0)
        foreach (Collider2D col in stuffInSphere)
        {
            
            if (col.gameObject.GetComponent<CharacterType>().typeIndex == myType)
            {
               if (!_charWithSameType.Contains(col.gameObject))
                {
                    
                    _charWithSameType.Add(col.gameObject);
                }
           }
        }
       
        if (_charWithSameType.Count > 0)
        {
           StartCoroutine(ResetMatchList());
        }

        if (_charWithSameType.Count < 3) return;
        foreach (GameObject matchedObj in _charWithSameType)
        {
           Destroy(matchedObj);
            print("Matched!");

  
        }
            
        Destroy(gameObject);
        _charWithSameType.Clear();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    IEnumerator ResetMatchList()
    {
        yield return new WaitForSeconds(0.5f);
        _charWithSameType.Clear();
        
    }
}
