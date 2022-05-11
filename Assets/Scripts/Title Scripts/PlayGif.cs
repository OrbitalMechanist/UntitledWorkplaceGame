 using UnityEngine;
 using UnityEngine.UI;
 
 public class PlayGif : MonoBehaviour
 {
     private Image image;
 
     void Start()
     {
         image = GetComponent<Image>();
     }
     private void Update()
     {
         image.sprite = GetComponent<SpriteRenderer>().sprite;
     }
 }