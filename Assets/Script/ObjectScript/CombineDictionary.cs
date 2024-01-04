using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombineDictionary : MonoBehaviour
{
    public Dictionary<string, Item> combineDic = new Dictionary<string, Item>();
    public EquipmentItem[] comEquipment = new EquipmentItem[4]; //무기 아이템 배열

    private void Awake()
    {
        GameManager.instance.dic = this;
    }

    void Start()
    {
        combineDic.Add("WoodWood", comEquipment[0]); //도끼 아이템 조합사전 추가
        combineDic.Add("WoodRock", comEquipment[1]); //무기 아이템 조합사전 추가
        combineDic.Add("WoodAxe", comEquipment[2]); //도끼 강화
        combineDic.Add("RockHammer", comEquipment[3]); //무기 강화
    }
}
