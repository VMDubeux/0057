using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour, IPointerUpHandler, IPointerClickHandler
{
    Transform imageSold;
    Animator animator;
    Button button;
    [SerializeField] bool selected;
    PlayerShop playerShop;
    [SerializeField] Transform canvasStore;

    void Start()
    {
        imageSold = transform.Find("ImageSold");
        animator = transform.GetComponent<Animator>();
        button = transform.GetComponent<Button>();
        canvasStore = transform.parent.parent.parent;
        playerShop = FindAnyObjectByType<PlayerShop>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (selected == false)
        {
            Debug.Log(selected);
            //gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Animator>().GetBool("Selected") && selected == false && playerShop.canBuy)
        {
            Debug.Log(playerShop.canBuy + " Can Buy");
            selected = true;
            button.enabled = false;
            imageSold.gameObject.SetActive(true);
        }
        else if (gameObject.GetComponent<Animator>().GetBool("Selected") && selected == false && !playerShop.canBuy)
        {
            Debug.Log(playerShop.canBuy + " Can't Buy");
        }
    }
}
