using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonster : Monster
{
    private new void Start()
    {
        base.Start();
        maxHp = 30;
    }
}
