using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public new string name;
    public string effect;
    public int itemCode;
    public Sprite sprite;
    public virtual void Use(Player player)
    {

    }
}
