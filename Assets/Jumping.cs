using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jumping : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    bool isPressed = false;
    public GameObject player;
    public float Force; 

    void Update()
    {
        
         if (isPressed){
                    player.transform.Translate(0, Force * Time.deltaTime, 0);}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
