using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera _camera;

   public static bool HoldingAgent = false;
    //match 3 game
    //chara move to a random spot on top of screen --check
    //player touch and drag to move charas
    //if 3 charac are with the same type, 
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            
          if(!HoldingAgent)
            MoveAgentWithTouch();

           
        }
        
        
    }

    void MoveAgentWithTouch()
    {
        Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction,Mathf.Infinity);
        Debug.DrawRay(ray.origin,ray.direction,Color.red);
   

            if(hitInfo.collider != null)
            {
               print("collider detected");
               hitInfo.collider.gameObject.GetComponent<AgentBehavior>().autoControl = false;
                HoldingAgent = true;
//                Vector3 touchPos = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
//                touchPos.z = 0;
//                hitInfo.transform.position = touchPos;
                
            }
           

    }
}
