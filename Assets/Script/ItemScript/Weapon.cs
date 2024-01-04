using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipmentItem, IAttackable
{
    public int addtionalAtk;
    bool isEquip = false;

    public void Attack(IHitable target)
    {
        target.Hit(GetComponentInParent<Player>().Atk);
    }

    public override void Use(Player player)
    {
        if (!isEquip)
        {
            isEquip = true;
        }
        else
        {
            isEquip = false;
        }
    }
}
