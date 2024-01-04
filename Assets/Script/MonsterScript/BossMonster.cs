using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonster : Monster
{
    public float updateTime = 5;
    [SerializeField]
    float currentTime;
    [SerializeField]
    Item escapeKey;


    protected new void Start()
    {
        base.Start();
        maxHp = 100;
        GameObject instance = Instantiate(Resources.Load<GameObject>("Item/FinalItem"));
        escapeKey = instance.GetComponent<Item>();
        escapeKey.gameObject.transform.SetParent(transform, false);
        escapeKey.gameObject.SetActive(false);
    }
    public new void Update()
    {
        lastTime += Time.deltaTime;
        if (dective.isRangeTarget)
        {
            currentTime += Time.deltaTime * 5;
            agent.SetDestination(dective.Pos.position - (Vector3.back * 1.5f));
            if(currentTime >= updateTime) 
            {
                if(Hp > 0)
                {
                    StartCoroutine(AttackMotion());
                }
                else
                {
                    animator.SetBool("AttackMotion1", false);
                    animator.SetBool("AttackMotion2", false);
                }
            }
            agent.transform.LookAt(dective.Pos);
        }
        else
        {
            //몬스터 랜덤이동 구현부
            if (lastTime >= time) //5보다 커지면
            {
                Vector3 randomPos = GetRandomPos(); //함수실행
                agent.SetDestination(randomPos);
                lastTime = 0;
            }
        }
    }

    IEnumerator AttackMotion()
    {
        int randomNum = Random.Range(0, 2);
        switch (randomNum)
        {
            case 0:
                animator.SetBool("AttackMotion2", false);
                animator.SetBool("AttackMotion1", true);
                currentTime = 0;
                yield return null;
                break;
            case 1:
                animator.SetBool("AttackMotion1", false);
                animator.SetBool("AttackMotion2", true);
                currentTime = 0;
                yield return null;
                break;
        }

    }

    protected override IEnumerator monsterDie()
    {
        escapeKey.gameObject.transform.SetParent(null);
        escapeKey.gameObject.SetActive(true);
        GameManager.instance.GameEnd();
        yield return StartCoroutine(base.monsterDie());
    }
}
