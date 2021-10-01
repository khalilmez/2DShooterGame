using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PooledObjectName { Enemy, PlayerBullet, EnemyBullet }
public class Factory : MonoBehaviour
{
    [SerializeField]
    GameObject prefabPlayerBullet;
    [SerializeField]
    GameObject prefabEnemyBullet;
    [SerializeField]
    GameObject prefabEnemy;

    Dictionary<PooledObjectName, List<GameObject>> factory;
    static Factory instance;
    public static Factory Instance
    {
        get => instance;
        private set => instance = value;
    }

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        factory = new Dictionary<PooledObjectName, List<GameObject>>();
        factory.Add(PooledObjectName.Enemy, new List<GameObject>(50));
        factory.Add(PooledObjectName.EnemyBullet, new List<GameObject>(150));
        factory.Add(PooledObjectName.PlayerBullet, new List<GameObject>(10));

        for (int i = 0; i < 10; i++)
        {
            factory[PooledObjectName.PlayerBullet].Add(GetNewObject(PooledObjectName.PlayerBullet));
        }
        for (int i = 0; i < 150; i++)
        {
            factory[PooledObjectName.EnemyBullet].Add(GetNewObject(PooledObjectName.EnemyBullet));
        }
        for (int i = 0; i < 50; i++)
        {
            factory[PooledObjectName.Enemy].Add(GetNewObject(PooledObjectName.Enemy));
        }
    }

    GameObject GetNewObject(PooledObjectName name)
    {
        GameObject obj;
        if (name == PooledObjectName.Enemy)
        {
            obj = GameObject.Instantiate(prefabEnemy);
        }
        else if (name == PooledObjectName.EnemyBullet)
        {
            obj = GameObject.Instantiate(prefabEnemyBullet);
        }
        else
        {
            obj = GameObject.Instantiate(prefabPlayerBullet);
        }
        obj.SetActive(false);
        GameObject.DontDestroyOnLoad(obj);
        return obj;
    }

    public GameObject GetObjectFromPool(PooledObjectName pooledObjectName)
    {
        if (pooledObjectName == PooledObjectName.PlayerBullet)
        {
            if (factory[PooledObjectName.PlayerBullet].Count > 0)
            {
                GameObject bullet = factory[PooledObjectName.PlayerBullet][0];
                factory[PooledObjectName.PlayerBullet].RemoveAt(0);
                return bullet;
            }
            else
            {
                factory[PooledObjectName.PlayerBullet].Capacity++;
                GameObject bullet = GetNewObject(PooledObjectName.PlayerBullet);
                return bullet;
            }
        }
        else if (pooledObjectName == PooledObjectName.EnemyBullet)
        {
            if (factory[PooledObjectName.EnemyBullet].Count > 0)
            {
                GameObject bullet = factory[PooledObjectName.EnemyBullet][0];
                factory[PooledObjectName.EnemyBullet].RemoveAt(0);
                return bullet;
            }
            else
            {
                factory[PooledObjectName.EnemyBullet].Capacity++;
                GameObject bullet = GetNewObject(PooledObjectName.EnemyBullet);
                return bullet;
            }
        }
        else
        {
            if (factory[PooledObjectName.Enemy].Count > 0)
            {
                GameObject enemy = factory[PooledObjectName.Enemy][0];
                factory[PooledObjectName.Enemy].RemoveAt(0);
                return enemy;
            }
            else
            {
                factory[PooledObjectName.Enemy].Capacity++;
                GameObject enemy = GetNewObject(PooledObjectName.Enemy);
                return enemy;
            }
        }
    }

    public void ReturnObjectToThePool(GameObject obj, PooledObjectName name)
    {
        if(name == PooledObjectName.PlayerBullet)
        {
            obj.GetComponent<PlayerBullet>().StopMoving();
            obj.SetActive(false);
            factory[PooledObjectName.PlayerBullet].Add(obj);
        }
        else if(name == PooledObjectName.EnemyBullet)
        {
            obj.GetComponent<EnemyBullet>().StopMoving();
            obj.SetActive(false);
            factory[PooledObjectName.EnemyBullet].Add(obj);
        }
        else
        {
            obj.GetComponent<EnemyAvatar>().StopMoving();
            obj.SetActive(false);
            factory[PooledObjectName.Enemy].Add(obj);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

}
