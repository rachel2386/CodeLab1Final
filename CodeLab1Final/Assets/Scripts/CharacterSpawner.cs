
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class CharacterSpawner : MonoBehaviour
{
    //a static list to recycle agents 
    //(in AgentBehavior script)when agent reach dest, add to list, disable
    //reset pos, set active, remove from list
    
    //randomize Type at spawn
    
    
    public static List<GameObject> CharacterPool = new List<GameObject>();

    private float _screenHeight;

    private float _screenWidth;

    private Vector2 _spawnLocation;
   private int maxNum = 5;
    private int charNum;
    private int spawnInterval = 8;
    private Camera _myCam;

    private bool invoked = false;
    //private InputField intervalInput;

    // Start is called before the first frame update
    private void Start()
    {
        
       _myCam = Camera.main;
       //intervalInput = GameObject.Find("IntervalInput").GetComponent<InputField>();
        _screenHeight = _myCam.orthographicSize * 2;
        _screenWidth = _screenHeight/2 * _myCam.aspect;
        _spawnLocation.y = -_screenHeight;
        Invoke("SpawnNewCharacters", 1);
       //Invoke("SpawnNewCharacters",1);
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
       if(!invoked)
        
        if (CharacterPool.Count == 0) return;
        for (int i = 0; i < CharacterPool.Count; i++)
        {
            Destroy(CharacterPool[i]);
            CharacterPool.Remove(CharacterPool[i]);
           
        }
//        _spawnLocation.x = Random.Range(-_screenWidth, _screenWidth);
//        CharacterPool[0].transform.position = _spawnLocation;
//        CharacterPool[0].SetActive(true);
//        CharacterPool.Remove(CharacterPool[0]);
        
    }

    private void SpawnNewCharacters()
    {
        
        
        if(GameManager.gameState == 0)
        for (int i = 0; i < maxNum; i++)
        {
            var spawnedObj = Instantiate(Resources.Load<GameObject>("Prefabs/Red"));
            spawnedObj.transform.position = Vector3.up * -_screenHeight/2 + Vector3.right * Random.Range(-_screenWidth, _screenWidth);
        }

        maxNum += 5;
        AgentBehavior.InitSpeed += 0.1f;
        print(AgentBehavior.InitSpeed);
        Invoke("SpawnNewCharacters", spawnInterval);
    }

//    public void SetIntervalTime()
//    {
//        spawnInterval = int.Parse(intervalInput.text);
//        intervalInput.gameObject.SetActive(false);
//    }
}
