using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterType : MonoBehaviour
{
  
   public Color ImgColor;

   private void Start()
   {
      print("assigned color");
      ImgColor = GetComponent<SpriteRenderer>().color;
   }
}
