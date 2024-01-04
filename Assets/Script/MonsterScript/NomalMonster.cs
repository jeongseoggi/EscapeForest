using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalMonster : Monster
{
    private new void Start()
    {
        base.Start();
        maxHp = 50;
    }
}
