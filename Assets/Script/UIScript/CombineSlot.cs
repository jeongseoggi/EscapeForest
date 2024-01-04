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
    public GameObject ok; //ok이미지가 들어간 게임오브젝트
    public GameObject cancle; //cancle이미지가 들어간 게임오브젝트
    public TextMeshProUGUI errorMessage;
    public List<Item> items = new List<Item>(); //슬롯에 들어간 아이템 리스트
    string itemName; //아이템 리스트에서 아이템의 이름을 합친 후 저장하기 위한 변수


    public ComSlot[] slot = new ComSlot[2];

    public void itemCombine(Item item) //슬롯에 아이템이 들어오면 해당 함수 실행
    {
        items.Add(item); //list에 아이템 저장

        if(items.Count >= 2) //list의 수가 2개면
        {
            CompareTo(); //아이템 정렬 함수 실행
            itemName = items[0].name + items[1].name; //list에 들어간 아이템의 이름을 더해줘서 저장
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter == ok) //클릭한 게임오브젝트가 ok 변수에 들어간 게임 오브젝트와 같다면
        {
            if (!GameManager.instance.dic.combineDic.ContainsKey(itemName)) //조합 사전에 없을경우
            {
                StartCoroutine(ShowMessage()); //조합 불가능 메세지 출력 코루틴
                return;
            }
            else
            {
                Item comItem = GameManager.instance.dic.combineDic[itemName]; //게임매니저에 있는 조합사전에서 아이템이름으로 된 아이템을 찾아서 넣어줌
                GetComponentInParent<InventoryUI>().AddItem(comItem); //인벤토리에 해당 아이템을 넣어줌
                for(int i = 0; i < slot.Length; i++) //넣어준 후에 조합대에 들어간 아이템 초기화
                {
                    slot[i].CombineSetItem(null);
                }
            }
        }
        if(eventData.pointerEnter == cancle) //취소버튼을 누르면
        {
            for(int i = 0; i < items.Count; i++) 
            {
                GetComponentInParent<InventoryUI>().AddItem(items[i]); //AddItem으로 인벤토리에 아이템을 넣어주고
                slot[i].CombineSetItem(null); //조합대에 들어가 있는 아이템 초기화
            }
        }
        items.Clear(); //ok나 cancle버튼을 누른 이후 리스트 초기화
        itemName = ""; //List에서 합치 아이템 이름도 초기화
    }

    IEnumerator ShowMessage()
    {
        errorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        errorMessage.gameObject.SetActive(false);
    }

    public void CompareTo() 
    {
        //List에 들어간 0번째의 아이템 코드가
        //1번째의 아이템 코드보다 클 경우 아이템 번호가 작은 순서로 정렬 
        if (items[0].itemCode > items[1].itemCode) 
        {
            items.Insert(0, items[1]);
            items.RemoveAt(2);
        }
    }
}
