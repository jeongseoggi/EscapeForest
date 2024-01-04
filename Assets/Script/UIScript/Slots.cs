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
                if(ownerInven.owner.Hp < 100) //player�� Hp�� 100�̻��϶� ������ ��� �Ұ��ϰ�
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

        //target�����ۿ��� ���콺�� ���� �������� �ƴ϶� ���콺�� ���� ���� ��ġ�� �������� ���� wood�� Rock��ġ��
        //���콺�� ���� ���� ���� ������ wood wood
        //���콺�� ������ ���� targetItem�� Rock�� �־���

        //Axe -> Wood 
        //Axe -> Axe target=>wood
        //wood -> Axe item=>Axe
        Item targetItem;

        if (eventData.pointerEnter.GetComponent<Slots>() != null) 
        {
            targetItem = eventData.pointerEnter.GetComponent<Slots>().item; //targetItem���� ���� ������
            eventData.pointerEnter.GetComponent<Slots>().SetItem(item); //���������Ϳ��� �� slot�� ������ �ִ� ������
            SetItem(targetItem);
        }
        if(eventData.pointerEnter.GetComponent<ComSlot>() != null) //���մ뿡 ���콺�� ������ ���
        {
            if(eventData.pointerEnter.GetComponent<ComSlot>().CombineSetItem(item) != errorCode) //���ϰ��� errorCode�� �ƴϸ�
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
        if(item == null || item is EquipmentItem) //�ٲ��� �������� null�̰ų� �����ϴ� �������� ���
        {
            itemCountText.gameObject.SetActive(false); //������ ǥ���ϴ� �ؽ�Ʈ�� ����
        }
        else
        {
            itemCountText.gameObject.SetActive(true);
        }
    }
}
