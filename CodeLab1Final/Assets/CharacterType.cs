using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//assign a type for the character spawned 
public class CharacterType : MonoBehaviour
{

   public int typeIndex;
   private Color ImgColor;

   private void Awake()
   {
     typeIndex = Random.Range(1, 4);
      ImgColor = GetComponent<SpriteRenderer>().color;
      
      if(typeIndex == 1)
         ImgColor = Color.red;
      else if (typeIndex == 2)
       ImgColor = Color.blue;
      else
         ImgColor = Color.green;

      GetComponent<SpriteRenderer>().color = ImgColor;
   }
}
