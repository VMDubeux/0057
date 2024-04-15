using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    bool _dragging;
    Transform _objectToDrag;
    Card _card;
    Vector2 _offset;
    Vector3 _savedPosition;
    void Awake(){
        _card = GetComponentInParent<Card>();
        _objectToDrag = _card.transform;
    }
    void Update()
    {
        if(_dragging){
            _objectToDrag.position = Mouse.current.position.ReadValue()-_offset;
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        _dragging = true;
        _offset = eventData.position - new Vector2(_objectToDrag.position.x, _objectToDrag.position.y);
        _savedPosition = _card.Rect.anchoredPosition3D;
    }
    public void OnPointerUp(PointerEventData eventData){
        _dragging = false;
        EventSystem.current.SetSelectedGameObject(null);
        //Debug.Log(Mouse.current.position.ReadValue().y);
        if(Mouse.current.position.ReadValue().y>=180 && _card.CanPlay()){
            StateMachine.Instance.CardsToPlay.Enqueue(_card);
        }else{
            _card.Move(_savedPosition, 0.2f, ()=>{});
        }
    }
}
