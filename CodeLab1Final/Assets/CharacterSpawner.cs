using System.Collections.Generic;
using UnityEngine;

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

    private Camera _myCam;
    // Start is called before the first frame update
    private void Start()
    {
        _myCam = Camera.main;
        _screenHeight = _myCam.orthographicSize * 2;
        _screenWidth = _screenHeight/2 * _myCam.aspect;
        _spawnLocation.y = -_screenHeight;
       // InvokeRepeating("SpawnNewCharacters",1,1);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
       
        if (CharacterPool.Count <= 0) return;
        _spawnLocation.x = Random.Range(-_screenWidth, _screenWidth);
        CharacterPool[0].transform.position = _spawnLocation;
        CharacterPool[0].SetActive(true);
        CharacterPool.Remove(CharacterPool[0]);
        
    }
    
    void SpawnNewCharacters()
    {
        if (CharacterPool.Count != 0) return;
        var spawnedObj = Instantiate(Resources.Load<GameObject>("Prefabs/Red"));
        spawnedObj.transform.position = Vector3.up * -_screenHeight/2 + Vector3.right * Random.Range(-_screenWidth, _screenWidth);


    }
}
