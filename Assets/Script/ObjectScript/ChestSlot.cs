using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryUI moveInven;
    public Item item;
    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI classText;
    public GameObject emptySprite;
    public TextMeshProUGUI countText;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerEnter.GetComponent<ChestSlot>() != null)
        {
            moveInven.AddItem(item);
            SetItem(null);
        }
    }

    public void SetItem(Item setItem)
    {
        item = setItem;
        if (item != null)
        {
            image.sprite = item.sprite;
            nameText.text = item.name;
            if (classText != null)
            {
                classText.text = "" + item.effect;
            }
        }
        emptySprite.SetActive(item == null);
    }
}
