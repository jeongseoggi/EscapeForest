using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinableItem : ConsumableItem
{
    public Dictionary<string, int> comDic = new Dictionary<string, int>();

    public void Start()
    {
        comDic.Add(name, itemCode);
    }
}
