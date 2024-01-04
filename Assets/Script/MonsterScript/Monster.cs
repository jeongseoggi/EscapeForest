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

    //���� �����̵� ����
    public float time = 5; //��ǥ ��ġ ���� �ð�
    protected float lastTime; //������ ��ǥ ��ġ ���� �ð�

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
            lastTime += Time.deltaTime; //�ð� �� �־���
            if (dective.isRangeTarget)
            {
                agent.SetDestination(dective.Pos.position - (Vector3.back * 0.7f));
                agent.transform.LookAt(dective.Pos);
            }
            else
            {
                //���� �����̵� ������
                if (lastTime >= time) //3���� Ŀ����
                {
                    Vector3 randomPos = GetRandomPos(); //�Լ�����
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
        weapon.GetComponent<BoxCollider>().enabled = true; //�̺�Ʈ �߻� �� ���⿡ �޷��ִ� �ݶ��̴� Ȱ��ȭ
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }
    public void AttackEnd()
    {
        weapon.GetComponent<BoxCollider>().enabled = false; //�̺�Ʈ ���� �� ���⿡ �޷��ִ� �ݶ��̴� ��Ȱ��ȭ
        gameObject.GetComponent<BoxCollider>().enabled = true; 
    }

    public void Attack(GameObject target)
    {

    }

    public void Hit(int atk)
    {
        Hp -= atk;
    }

    public Vector3 GetRandomPos() // �����̵� ������ ���� �Լ�
    {
        Vector3 dir = Random.insideUnitSphere * 20; //���� ���� ����
        dir += transform.position;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(dir, out hit, 20f, NavMesh.AllAreas)) //�������� ������ ��ġ�� NavMesh���� �ִ��� Ȯ��
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
