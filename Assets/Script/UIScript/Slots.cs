using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public InventoryUI ownerInven;
    public Image image;
    public GameObject emptySprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI effectText;
    public TextMeshProUGUI itemCountText;
    public Item item;
    bool isConsum;
    int errorCode = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponent<Slots>() != null)
        {
            Item targetItem = eventData.pointerEnter.GetComponent<Slots>().item;
            if (targetItem is RecoverableItem)
            {
                if(ownerInven.owner.Hp < 100) //player의 Hp가 100이상일때 아이템 사용 불가하게
                {
                    int useCount = --((RecoverableItem)targetItem).count;
                    targetItem.Use(ownerInven.owner);
                    CountItem(useCount);
                }
                else
                    return;
            }
            else if (targetItem is EquipmentItem)
            {
                targetItem.Use(ownerInven.owner);
                ownerInven.owner.weaponEquip.GetComponent<weaponSlot>().AddEquipItem(targetItem);
                effectText.text = "Equip";
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        //target아이템에는 마우스로 잡은 아이템이 아니라 마우스를 놓고 싶은 위치의 아이템을 저장 wood를 Rock위치로
        //마우스를 놓은 곳에 현재 아이템 wood wood
        //마우스를 집었던 곳에 targetItem인 Rock을 넣어줌

        //Axe -> Wood 
        //Axe -> Axe target=>wood
        //wood -> Axe item=>Axe
        Item targetItem;

        if (eventData.pointerEnter.GetComponent<Slots>() != null) 
        {
            targetItem = eventData.pointerEnter.GetComponent<Slots>().item; //targetItem에는 현재 포인터
            eventData.pointerEnter.GetComponent<Slots>().SetItem(item); //현재포인터에는 이 slot이 가지고 있는 아이템
            SetItem(targetItem);
        }
        if(eventData.pointerEnter.GetComponent<ComSlot>() != null) //조합대에 마우스를 놓았을 경우
        {
            if(eventData.pointerEnter.GetComponent<ComSlot>().CombineSetItem(item) != errorCode) //리턴값이 errorCode가 아니면
            {
                if(isConsum)
                {
                    int useCount = --((ConsumableItem)item).count;
                    CountItem(useCount);
                }
                else
                {
                    SetItem(null);
                }
            }
            else
            {
                return;
            }
        }
    }

    public void SetItem(Item setItem)
    {
        item = setItem;
        CheckItem(item);
        isConsum = item is ConsumableItem;
        if (item != null)
        {
            image.sprite = item.sprite;
            nameText.text = item.name;
            if(effectText != null)
            {
                effectText.text = "" + item.effect;
            }
            if (isConsum)
            {
                CountItem(((ConsumableItem)item).count);
            }
        }
        emptySprite.SetActive(item == null);
    }

    public void CountItem(int count)
    {
        if (count <= 0)
        {
            itemCountText.gameObject.SetActive(false);
            SetItem(null);
        }
        else
        {
            itemCountText.gameObject.SetActive(true);
            itemCountText.SetText(count.ToString());
        }
    }

    public void CheckItem(Item item)
    {
        if(item == null || item is EquipmentItem) //바꿔진 아이템이 null이거나 장착하는 아이템일 경우
        {
            itemCountText.gameObject.SetActive(false); //갯수를 표시하는 텍스트를 꺼줌
        }
        else
        {
            itemCountText.gameObject.SetActive(true);
        }
    }
}
