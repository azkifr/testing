using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerCard : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public GameObject object_Drag;
    public GameObject object_Game;
    public GameObject container;
    private GameObject object_DragInstance;
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        object_DragInstance= Instantiate(object_Drag, container.transform);
        object_DragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
