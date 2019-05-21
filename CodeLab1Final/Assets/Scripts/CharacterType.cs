using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//assign a type for the character spawned 
public class CharacterType : MonoBehaviour
{

   public int typeIndex;
   public Sprite Type1Sprite;
   public Sprite Type2Sprite;
   public Sprite Type3Sprite;
   public Sprite Type4Sprite;

   private void Awake()
   {
     typeIndex = Random.Range(1, 5);
    
      
      if(typeIndex == 1)
         GetComponent<SpriteRenderer>().sprite =Type1Sprite;
      else if (typeIndex == 2)
         GetComponent<SpriteRenderer>().sprite =Type2Sprite;
      else if (typeIndex == 3)
         GetComponent<SpriteRenderer>().sprite =Type3Sprite;
      else
         GetComponent<SpriteRenderer>().sprite =Type4Sprite;

      //gameObject.AddComponent<CapsuleCollider2D>();
   }
}
