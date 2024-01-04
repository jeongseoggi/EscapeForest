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
        if (item is CollectableItem) //���ڷ� ���� �������� ���� ������ �������̸�
        {
            obj.transform.SetParent(transform, false); //��ġ ����
            obj.SetActive(false);
            equipSlot[0].gameObject.SetActive(false); //���� �ִ� ������ ����
            equipSlot[0].transform.SetParent(null); //�θ� ������Ʈ���� ���������� ����
            Destroy(equipSlot[0]); //���� �ִ� ���� ������ �ı�
            equipSlot[0] = obj.GetComponent<Item>(); //���� �ڸ��� ���Ӱ� ���� ������ �־���
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
