using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField]
    private GameObject hitExplosion;

    PlayerAvatar player;
    public override void Init(BulletType bulType)
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerAvatar>();
        if(player == null)
        {
            throw new Exception("Player not found");
        }
        Speed = new Vector3(15, 0, 0);
        this.transform.position = player.gameObject.transform.GetChild(0).position;
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyAvatar enemy = collision.GetComponent<EnemyAvatar>();
        if (enemy != null)
        {
            enemy.TakeDamage(Damage);
        }
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        Factory.Instance.ReturnObjectToThePool(this.gameObject,PooledObjectName.PlayerBullet);
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += Speed * Time.deltaTime;
        if (!PlayZone.Contains(this.gameObject.transform.position))
        {
            Factory.Instance.ReturnObjectToThePool(this.gameObject, PooledObjectName.PlayerBullet);
        }
    }
}
