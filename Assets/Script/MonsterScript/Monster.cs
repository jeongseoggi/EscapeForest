using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
public enum MONSTER_TYPE
{
    BASIC_MON,
    NORMAL_MON
}
public class Monster : MonoBehaviour, IHitable
{
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int atk;
    protected int maxHp;
    public GameObject weapon;
    protected DectectiveComponent dective;
    protected NavMeshAgent agent;
    protected Animator animator;

    public MONSTER_TYPE type;

    public int Atk => atk;
    public int MaxHp => maxHp;
    public int Hp
    {
        get { return hp; }
        set 
        {
            hp = value;
            if(hp <= 0)
            {
                hp = 0;
                animator.SetBool("IsDie", true);
                agent.ResetPath();
                agent.transform.LookAt(null);
                StartCoroutine(monsterDie());
            }
        }
    }

    //몬스터 랜덤이동 변수
    public float time = 5; //목표 위치 갱신 시간
    protected float lastTime; //마지막 목표 위치 갱신 시간

    protected void Start()
    {
        dective = GetComponent<DectectiveComponent>();
        weapon.GetComponent<BoxCollider>().enabled = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if(Hp > 0)
        {
            lastTime += Time.deltaTime; //시간 값 넣어줌
            if (dective.isRangeTarget)
            {
                agent.SetDestination(dective.Pos.position - (Vector3.back * 0.7f));
                agent.transform.LookAt(dective.Pos);
            }
            else
            {
                //몬스터 랜덤이동 구현부
                if (lastTime >= time) //3보다 커지면
                {
                    Vector3 randomPos = GetRandomPos(); //함수실행
                    agent.SetDestination(randomPos);
                    lastTime = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IAttackable attack))
        {
            attack.Attack(this);
        }
    }

    public void AttackStart()
    {
        weapon.GetComponent<BoxCollider>().enabled = true; //이벤트 발생 시 무기에 달려있는 콜라이더 활성화
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }
    public void AttackEnd()
    {
        weapon.GetComponent<BoxCollider>().enabled = false; //이벤트 종료 시 무기에 달려있는 콜라이더 비활성화
        gameObject.GetComponent<BoxCollider>().enabled = true; 
    }

    public void Attack(GameObject target)
    {

    }

    public void Hit(int atk)
    {
        Hp -= atk;
    }

    public Vector3 GetRandomPos() // 랜덤이동 포지션 구현 함수
    {
        Vector3 dir = Random.insideUnitSphere * 20; //방향 벡터 생성
        dir += transform.position;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(dir, out hit, 20f, NavMesh.AllAreas)) //랜덤으로 정해진 위치가 NavMesh위에 있는지 확인
        {
            return hit.position;
        }
        else
        {
            return transform.position;
        }

    }

    protected virtual IEnumerator monsterDie()
    {
        yield return new WaitForSeconds(3.5f);
        if(!this.GetComponent<BossMonster>())
        {
            GameManager.instance.spawnManager.ReSpawn(this);
        }
        else
        {
            Destroy(gameObject);
        }
        yield return null;
    }
}
