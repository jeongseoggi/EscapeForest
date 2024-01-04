using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������̽�

public interface IHitable
{
    void Hit(int atk);
}

public interface IAttackable
{
    void Attack(IHitable target);
}

public interface ICollectableWeapon
{
    void Collect(Object obj);
}

public class InterfaceScript : MonoBehaviour
{

}
