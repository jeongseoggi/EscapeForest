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
        if (item != null)  //������ ���� �������� ���հ����� ���������� Ȯ��
        {
            GetComponentInParent<CombineSlot>().itemCombine(item);
            image.sprite = item.sprite;
            return 1;
        }
        else//�ƹ��� �������� �ȵ����� �� �����ڵ��� 0 ����
        {
            image.sprite = null;
            return 0;
        }

    }
}
