using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombineSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject ok; //ok�̹����� �� ���ӿ�����Ʈ
    public GameObject cancle; //cancle�̹����� �� ���ӿ�����Ʈ
    public TextMeshProUGUI errorMessage;
    public List<Item> items = new List<Item>(); //���Կ� �� ������ ����Ʈ
    string itemName; //������ ����Ʈ���� �������� �̸��� ��ģ �� �����ϱ� ���� ����


    public ComSlot[] slot = new ComSlot[2];

    public void itemCombine(Item item) //���Կ� �������� ������ �ش� �Լ� ����
    {
        items.Add(item); //list�� ������ ����

        if(items.Count >= 2) //list�� ���� 2����
        {
            CompareTo(); //������ ���� �Լ� ����
            itemName = items[0].name + items[1].name; //list�� �� �������� �̸��� �����༭ ����
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter == ok) //Ŭ���� ���ӿ�����Ʈ�� ok ������ �� ���� ������Ʈ�� ���ٸ�
        {
            if (!GameManager.instance.dic.combineDic.ContainsKey(itemName)) //���� ������ �������
            {
                StartCoroutine(ShowMessage()); //���� �Ұ��� �޼��� ��� �ڷ�ƾ
                return;
            }
            else
            {
                Item comItem = GameManager.instance.dic.combineDic[itemName]; //���ӸŴ����� �ִ� ���ջ������� �������̸����� �� �������� ã�Ƽ� �־���
                GetComponentInParent<InventoryUI>().AddItem(comItem); //�κ��丮�� �ش� �������� �־���
                for(int i = 0; i < slot.Length; i++) //�־��� �Ŀ� ���մ뿡 �� ������ �ʱ�ȭ
                {
                    slot[i].CombineSetItem(null);
                }
            }
        }
        if(eventData.pointerEnter == cancle) //��ҹ�ư�� ������
        {
            for(int i = 0; i < items.Count; i++) 
            {
                GetComponentInParent<InventoryUI>().AddItem(items[i]); //AddItem���� �κ��丮�� �������� �־��ְ�
                slot[i].CombineSetItem(null); //���մ뿡 �� �ִ� ������ �ʱ�ȭ
            }
        }
        items.Clear(); //ok�� cancle��ư�� ���� ���� ����Ʈ �ʱ�ȭ
        itemName = ""; //List���� ��ġ ������ �̸��� �ʱ�ȭ
    }

    IEnumerator ShowMessage()
    {
        errorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        errorMessage.gameObject.SetActive(false);
    }

    public void CompareTo() 
    {
        //List�� �� 0��°�� ������ �ڵ尡
        //1��°�� ������ �ڵ庸�� Ŭ ��� ������ ��ȣ�� ���� ������ ���� 
        if (items[0].itemCode > items[1].itemCode) 
        {
            items.Insert(0, items[1]);
            items.RemoveAt(2);
        }
    }
}
