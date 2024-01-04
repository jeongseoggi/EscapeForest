using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> monsterPrefab = new List<GameObject>();
    public Transform spawnRange;
    public int size;
    public Dictionary<MONSTER_TYPE, Queue<GameObject>> spawner;


    public void Awake()
    {
        GameManager.instance.spawnManager = this;
    }
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        spawner = new Dictionary<MONSTER_TYPE, Queue<GameObject>>()
        {
            { MONSTER_TYPE.NORMAL_MON, new Queue<GameObject>() },
            { MONSTER_TYPE.BASIC_MON, new Queue<GameObject>() },
        };


        for(int i = 0; i < monsterPrefab.Count; i++)
        {
            MONSTER_TYPE type = monsterPrefab[i].GetComponent<Monster>().type;
            for(int j = 0; j < size; j++)
            {
                GameObject obj = Instantiate(monsterPrefab[i], transform);
                obj.SetActive(false);
                spawner[type].Enqueue(obj);
            }
        }

        for (int i = 0; i < monsterPrefab.Count; i++)
        {
            MONSTER_TYPE type = monsterPrefab[i].GetComponent<Monster>().type;
            Spawn(type, size);
        }
    }

    public void Spawn(MONSTER_TYPE type, int size = 1)
    {
        for(int j= 0; j < size; j++)
        {
            GameObject obj = spawner[type].Dequeue();
            obj.SetActive(true);
            obj.transform.position = SpawnPos(); 
        }
    }

    public void ReSpawn(Monster obj)
    {
        obj.gameObject.SetActive(false);
        MONSTER_TYPE type = obj.type;
        obj.Hp = obj.MaxHp;
        spawner[type].Enqueue(obj.gameObject);
        Spawn(type);
    }

    public Vector3 SpawnPos() //랜덤 스폰 함수
    {
        Vector3 pos = new Vector3();
        pos.x = Random.Range((spawnRange.GetComponent<BoxCollider>().bounds.size.x/2) * -1, spawnRange.GetComponent<BoxCollider>().bounds.size.x / 2);
        pos.y = -1;
        pos.z = Random.Range((spawnRange.GetComponent<BoxCollider>().bounds.size.z/2) * -1, spawnRange.GetComponent<BoxCollider>().bounds.size.z / 2);
        pos = pos + spawnRange.position;
        return pos;
    }
}
