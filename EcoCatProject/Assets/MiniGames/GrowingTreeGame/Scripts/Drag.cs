using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 defaultposition;//드롭하면 다시 원위치로 보내기위한 변수     
    public static Vector2 currentPos;

    void Start()
    {

    }

    void Update()
    {

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)//드래그시작할 때

    {
        defaultposition = this.transform.position;
    }


    void IDragHandler.OnDrag(PointerEventData eventData)//드래그중일 때 
    {
        currentPos = Input.mousePosition;
        this.transform.position = currentPos;
    }


    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//드래그 끝났을 때 
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (this.transform.name == "wateringpot")
        {
            this.transform.position = defaultposition;
        }
        else if (this.transform.name == "seed" || this.transform.name == "sun")
        {
            this.transform.position = currentPos;
        }
      //  Debug.Log(currentPos);


    }

}
