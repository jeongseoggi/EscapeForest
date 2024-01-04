using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverableItem : ConsumableItem
{
    public override void Use(Player player)
    {
        player.Hp += 10;
    }
}
