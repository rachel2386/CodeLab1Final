using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterType : MonoBehaviour
{
  
   public Color ImgColor;

   private void Start()
   {
      ImgColor = GetComponent<SpriteRenderer>().color;
   }
}
