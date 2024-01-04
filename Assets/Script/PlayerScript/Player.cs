using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHitable
{
    private Animator animator;
    private float speed = 2; //ĳ���� �̵� ���ǵ�

    public Image interactionImage;
    public bool isInteracion;
    [SerializeField]
    private bool isWalk;
    private bool isRun;
    [SerializeField]
    private float hp; //ü��
    private float stamina; //���׹̳�
    private float maxHp = 100; //�ִ� ü��
    private float maxStamina = 100; //�ִ� ���׹̳�
    private static int basicAtk = 10; //���ݷ�
    [SerializeField]
    private int atk;
    Rigidbody rb;
    private int turnSpeed = 150; //ȸ�� ����Ʈ
    public GameObject weapon; //���� �ڱⰡ ���� �ִ� ���� ���ӿ�����Ʈ
    public GameObject inven; //�κ��丮UI ���� ������Ʈ
    public GameObject invenObj; //�÷��̾ �������� �Ծ��� �� �� ���ӿ�����Ʈ�� �ڽ����� ��
    public InventoryUI invenUI; //�κ��丮 UI ��ũ��Ʈ
    public GameObject weaponEquip; //���Ⱑ ���۵� �� ���ӿ�����Ʈ
    const int prevPitch = 1; // �޷��� �� audioSoure�� pitch�� �ٲ�����ϹǷ� �⺻ pitch�� ���� ��� ���
    public AudioClip walkClip; //�߼Ҹ� clip
    bool isPlay = false; 
    GameObject temp;

    public float Stamina
    {
        get
        { return stamina; }
        set
        {
            stamina = value;
            UIManager.instance.slider[1].value = stamina;
            if (stamina > maxStamina)
                stamina = maxStamina;
        }
    }

    public float MaxStamina => maxStamina;
    public float MaxHp => maxHp;
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            UIManager.instance.slider[0].value = hp;
            if (hp <= 0)
            {
                hp = 0;

                StartCoroutine(Die());
                GameManager.instance.Die();
            }
            else if (hp > maxHp)
                hp = maxHp;
        }
    } //hp ������Ƽ
    public bool IsWalk
    {
        get { return isWalk; }
        set
        {
            isWalk = value;
            if (isWalk)
            {
                animator.SetBool("IsWalking", true); 
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetFloat("Speed", 0.5f);
            }
        }
    } //walk ������Ƽ
    public bool IsRun
    {
        get { return isRun; }
        set
        {
            isRun = value;
            if (isRun)
            {
                animator.SetBool("IsRunning", true);
                transform.Translate(Vector3.forward * Time.deltaTime * (speed + 2));
            }
            else
            {
                animator.SetBool("IsRunning", false);
                if(isPlay)
                {
                    temp.GetComponent<AudioSource>().pitch = prevPitch;
                }
            }
        }
    } //run ������Ƽ
    public int Atk
    {
        get { return atk; }
        set
        {
            atk = value;
        }
    } //atk ������Ƽ

    void Awake()
    {
        GameManager.instance.player = this;
        UIManager.instance.SetSlider();
        GameManager.instance.GameStart();
    }


    void Start()
    {
        interactionImage.gameObject.SetActive(false);
        Hp = MaxHp;
        stamina = MaxStamina;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        atk = basicAtk;
        animator.SetBool("Axe", true);
        weapon = weaponEquip.GetComponent<weaponSlot>().equipSlot[0].gameObject; //���� ���� �� ������ ��� ����
    }
    void Update()
    {
        InputKey();
    }

    void InputKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if(!isPlay)
            {
                isPlay = true;
                temp = SoundManager.instance.Pop(walkClip, true);
            }
            IsWalk = true;
            animator.SetFloat("Speed", 1);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if(!isPlay)
            {
                isPlay = true;
                temp = SoundManager.instance.Pop(walkClip, true);
            }
            animator.SetFloat("Speed", 0);
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else
        {
            if(isPlay)
            {
                isPlay = false;
                SoundManager.instance.ReturnPool(temp);
            }
            IsWalk = false;
          
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -turnSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Stamina -= Time.deltaTime * 15; //�޸���ÿ� ���׹̳�
            if (IsWalk && Stamina > 0) //���׹̳ʰ� 0�� �ƴϰ� �ȱ� �����϶�
            {
                IsRun = true; //�޸�
                temp.GetComponent<AudioSource>().pitch = 2;
            }
            else if (IsWalk && Stamina <= 0) //���׹̳ʰ� 0�̸�
            {
                IsRun = false;
            }
        }
        else
        {
            Stamina += Time.deltaTime * 15; //���׹̳� ȸ��
            IsRun = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //���콺 ���� Ŭ�� ���� Ȱ��ȭ
            animator.SetTrigger("AttackTrigger");

        if (Input.GetKeyDown(KeyCode.Tab)) //�κ��丮 â Ȱ��ȭ/��Ȱ��ȭ
        {
            if (inven.activeSelf == false)
            {
                inven.SetActive(true);
            }
            else
            {
                inven.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) //Ű�е� 1���� ������ ��
        {
            animator.SetBool("Axe", true); //1���� ���� -> Axe�� bool���� true��
            animator.SetBool("weapon", false); //2���� ���� -> weapon�� bool���� false��
            weapon = weaponEquip.GetComponent<weaponSlot>().equipSlot[0].gameObject; //weapon�� ���� ������Ʈ �ٲٱ�
            //�Լ� ���� �� ���� �Ǵ� ���� 0�� ��� ���� �Ǵ� ���Ⱑ ���� �����̹Ƿ� �÷��̾��� ���ݷ��� �⺻ ���ݷ����� �������
            if (weaponEquip.GetComponent<weaponSlot>().ChangeWeapon(1) == 0)
            {
                atk = basicAtk;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) //Ű�е� 2���� ������ ��
        {
            if (weaponEquip.GetComponent<weaponSlot>().equipSlot.Count == 2)
            {
                animator.SetBool("weapon", true); //2���� ���� -> weapon�� bool���� true��
                animator.SetBool("Axe", false); //1���� ���� -> Axe�� bool���� false��
                weapon = weaponEquip.GetComponent<weaponSlot>().equipSlot[1].gameObject; //weapon�� ���� ������Ʈ �ٲٱ�
                atk += weaponEquip.GetComponent<weaponSlot>().ChangeWeapon(2); //���� �ٲٱ� �Լ�
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isInteracion = true;
        }
    } //ĳ���� �Է� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(invenObj.transform);
            invenUI.AddItem(item);
        }
    } //������ �ݱ� Ʈ����


    IEnumerator Die() //hp 0�϶� �ִϸ��̼� ���� �� player �ı�
    {
        animator.SetBool("IsDie", true);
        yield return new WaitForSeconds(2);
    }
    public void Hit(int atk) //�������̽� ���� �Լ�
    {
        Hp -= atk;
    }
    public void AttackStart() //�ִϸ��̼� �̺�Ʈ �Լ�
    {
        weapon.GetComponent<MeshCollider>().enabled = true;
    }
    public void AttackEnd() //�ִϸ��̼� �̺�Ʈ �Լ�
    {
        weapon.GetComponent<MeshCollider>().enabled = false;
    }
}
