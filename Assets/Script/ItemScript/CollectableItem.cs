using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : EquipmentItem, ICollectableWeapon
{
    public int collectAtk = 10;
    public void Collect(Object obj)
    {
        obj.Hp -= collectAtk;
    }
}
