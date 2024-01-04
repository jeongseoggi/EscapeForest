using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<IHitable>() != null)
        {
            other.GetComponentInParent<IHitable>().Hit(GetComponentInParent<Monster>().Atk);
        }
    }
}
