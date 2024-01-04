using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField]
    private int hp;
    Rigidbody rb;
    public GameObject obj;
    GameObject instance;
    GameObject instance2;
    public GameObject dropItem;
    List<GameObject> prefabList = new List<GameObject>();
    public Player player;
    public AudioClip brokenClip;


    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                StartCoroutine(CutTime());
            }
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        listPrefab();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ICollectableWeapon weapon))
        {
            player = other.GetComponentInParent<Player>();
            weapon.Collect(this);
        }
    }

    void listPrefab() // 오브젝트 아이템 드랍 초기 설정
    {
        //instance = PrefabUtility.InstantiatePrefab(obj) as GameObject;
        instance = Instantiate(Resources.Load<GameObject>("Item/" + obj.name));
        //instance2 = PrefabUtility.InstantiatePrefab(dropItem) as GameObject;
        instance2 = Instantiate(Resources.Load<GameObject>("Item/" + dropItem.name));

        prefabList.Add(instance);
        prefabList.Add(instance2);

        foreach(GameObject pre in prefabList)
        {
            pre.transform.SetParent(transform, false);
            pre.SetActive(false);
        }
    }

    IEnumerator CutTime()
    {
        player.GetComponent<Rigidbody>().isKinematic = true; //오브젝트가 부셔질 때 점프되는 것을 방지
        rb.isKinematic = false;
        if(brokenClip != null)
        {
            SoundManager.instance.Pop(brokenClip);
        }
        player.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(4);
        foreach(GameObject instan in prefabList)
        {
            instan.transform.SetParent(null);
            instan.SetActive(true);
        }
        Destroy(gameObject);
    }
}
