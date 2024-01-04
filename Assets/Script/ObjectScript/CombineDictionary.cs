using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombineDictionary : MonoBehaviour
{
    public Dictionary<string, Item> combineDic = new Dictionary<string, Item>();
    public EquipmentItem[] comEquipment = new EquipmentItem[4]; //���� ������ �迭

    private void Awake()
    {
        GameManager.instance.dic = this;
    }

    void Start()
    {
        combineDic.Add("WoodWood", comEquipment[0]); //���� ������ ���ջ��� �߰�
        combineDic.Add("WoodRock", comEquipment[1]); //���� ������ ���ջ��� �߰�
        combineDic.Add("WoodAxe", comEquipment[2]); //���� ��ȭ
        combineDic.Add("RockHammer", comEquipment[3]); //���� ��ȭ
    }
}
