using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComSlot : MonoBehaviour
{
    public Image image;
    public Item item;

    public int CombineSetItem(Item cItem)
    {
        item = cItem;
        if (item != null)  //변수로 받은 아이템이 조합가능한 아이템인지 확인
        {
            GetComponentInParent<CombineSlot>().itemCombine(item);
            image.sprite = item.sprite;
            return 1;
        }
        else//아무런 아이템이 안들어왔을 때 에러코드인 0 리턴
        {
            image.sprite = null;
            return 0;
        }

    }
}
