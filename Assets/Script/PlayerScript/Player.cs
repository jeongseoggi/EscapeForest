using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHitable
{
    private Animator animator;
    private float speed = 2; //캐릭터 이동 스피드

    public Image interactionImage;
    public bool isInteracion;
    [SerializeField]
    private bool isWalk;
    private bool isRun;
    [SerializeField]
    private float hp; //체력
    private float stamina; //스테미너
    private float maxHp = 100; //최대 체력
    private float maxStamina = 100; //최대 스테미너
    private static int basicAtk = 10; //공격력
    [SerializeField]
    private int atk;
    Rigidbody rb;
    private int turnSpeed = 150; //회전 스피트
    public GameObject weapon; //현재 자기가 끼고 있는 무기 게임오브젝트
    public GameObject inven; //인벤토리UI 게임 오브젝트
    public GameObject invenObj; //플레이어가 아이템을 먹었을 때 이 게임오브젝트의 자식으로 들어감
    public InventoryUI invenUI; //인벤토리 UI 스크립트
    public GameObject weaponEquip; //무기가 장작될 손 게임오브젝트
    const int prevPitch = 1; // 달렸을 때 audioSoure의 pitch를 바꿔줘야하므로 기본 pitch의 값을 담는 상수
    public AudioClip walkClip; //발소리 clip
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
    } //hp 프로퍼티
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
    } //walk 프로퍼티
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
    } //run 프로퍼티
    public int Atk
    {
        get { return atk; }
        set
        {
            atk = value;
        }
    } //atk 프로퍼티

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
        weapon = weaponEquip.GetComponent<weaponSlot>().equipSlot[0].gameObject; //게임 시작 시 도끼를 들고 시작
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
            Stamina -= Time.deltaTime * 15; //달리기시에 스테미너
            if (IsWalk && Stamina > 0) //스테미너가 0이 아니고 걷기 상태일때
            {
                IsRun = true; //달림
                temp.GetComponent<AudioSource>().pitch = 2;
            }
            else if (IsWalk && Stamina <= 0) //스테미너가 0이면
            {
                IsRun = false;
            }
        }
        else
        {
            Stamina += Time.deltaTime * 15; //스테미너 회복
            IsRun = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //마우스 왼쪽 클릭 공격 활성화
            animator.SetTrigger("AttackTrigger");

        if (Input.GetKeyDown(KeyCode.Tab)) //인벤토리 창 활성화/비활성화
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) //키패드 1번을 눌렀을 때
        {
            animator.SetBool("Axe", true); //1번은 도끼 -> Axe의 bool값을 true로
            animator.SetBool("weapon", false); //2번은 무기 -> weapon의 bool값을 false로
            weapon = weaponEquip.GetComponent<weaponSlot>().equipSlot[0].gameObject; //weapon의 게임 오브젝트 바꾸기
            //함수 실행 후 리턴 되는 값이 0인 경우 도끼 또는 무기가 없는 상태이므로 플레이어의 공격력을 기본 공격력으로 만들어줌
            if (weaponEquip.GetComponent<weaponSlot>().ChangeWeapon(1) == 0)
            {
                atk = basicAtk;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) //키패드 2번을 눌렀을 때
        {
            if (weaponEquip.GetComponent<weaponSlot>().equipSlot.Count == 2)
            {
                animator.SetBool("weapon", true); //2번은 무기 -> weapon의 bool값을 true로
                animator.SetBool("Axe", false); //1번은 도끼 -> Axe의 bool값을 false로
                weapon = weaponEquip.GetComponent<weaponSlot>().equipSlot[1].gameObject; //weapon의 게임 오브젝트 바꾸기
                atk += weaponEquip.GetComponent<weaponSlot>().ChangeWeapon(2); //무기 바꾸기 함수
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isInteracion = true;
        }
    } //캐릭터 입력 제어

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(invenObj.transform);
            invenUI.AddItem(item);
        }
    } //아이템 줍기 트리거


    IEnumerator Die() //hp 0일때 애니메이션 실행 후 player 파괴
    {
        animator.SetBool("IsDie", true);
        yield return new WaitForSeconds(2);
    }
    public void Hit(int atk) //인터페이스 구현 함수
    {
        Hp -= atk;
    }
    public void AttackStart() //애니메이션 이벤트 함수
    {
        weapon.GetComponent<MeshCollider>().enabled = true;
    }
    public void AttackEnd() //애니메이션 이벤트 함수
    {
        weapon.GetComponent<MeshCollider>().enabled = false;
    }
}
