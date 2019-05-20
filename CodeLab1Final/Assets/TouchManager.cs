using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera _camera;

   public static bool HoldingAgent = false; //is player holding sth already?

   
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

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
              hitInfo.collider.gameObject.GetComponent<AgentBehavior>().autoControl = false;
                HoldingAgent = true;
          
            }
           

    }
}
