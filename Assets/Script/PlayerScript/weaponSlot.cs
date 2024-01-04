using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSlot : MonoBehaviour
{
    public List<Item>equipSlot = new List<Item>();
    public EquipSlot slot;

    public void Start()
    {
        slot.SetItem(equipSlot[0]);
    }

    public int ChangeWeapon(int num)
    {
        foreach(var equip in equipSlot) 
        {
            equip.gameObject.SetActive(false);
        }
        equipSlot[num-1].gameObject.SetActive(true);
        slot.SetItem(equipSlot[num - 1]);

        if (equipSlot[num-1].TryGetComponent(out Weapon weapon))
        {
            return weapon.addtionalAtk;
        }
        else
        {
            return 0;
        }
    }

    public void AddEquipItem(Item item)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Item/" + item.gameObject.name));
        if (item is CollectableItem) //인자로 받은 아이템이 수집 가능한 아이템이면
        {
            obj.transform.SetParent(transform, false); //위치 설정
            obj.SetActive(false);
            equipSlot[0].gameObject.SetActive(false); //원래 있던 아이템 꺼줌
            equipSlot[0].transform.SetParent(null); //부모 오브젝트에서 독립적으로 설정
            Destroy(equipSlot[0]); //원래 있던 도끼 아이템 파괴
            equipSlot[0] = obj.GetComponent<Item>(); //도끼 자리에 새롭게 만든 도끼를 넣어줌
            GetComponentInParent<Player>().weapon = equipSlot[0].gameObject;
            ChangeWeapon(1);
            return;
        }

        foreach (var equip in equipSlot) 
        {
            if(equip.name != item.name)
            {
                obj.transform.SetParent(transform, false);
                obj.SetActive(false);
            }
        }

        if (equipSlot.Count >= 2)
        {
            equipSlot.RemoveAt(1);
            equipSlot.Add(obj.GetComponent<Item>());
        }
        else
        {
            equipSlot.Add(obj.GetComponent<Item>());
        }
    }
}
