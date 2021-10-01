using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField]
    private GameObject hitExplosion;
    // Start is called before the first frame update
    void Start()
    {
    }
    public override void Init(BulletType bulType)
    {
        Speed = new Vector3(15, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            collision.GetComponent<PlayerAvatar>().TakeDamage(Damage);
        }
        if (!collision.name.Contains("Enemy"))
        {
            Instantiate(hitExplosion, transform.position, Quaternion.identity);
            Factory.Instance.ReturnObjectToThePool(this.gameObject, PooledObjectName.EnemyBullet);
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position -= Speed * Time.deltaTime;
        if (!PlayZone.Contains(this.gameObject.transform.position))
        {
            Factory.Instance.ReturnObjectToThePool(this.gameObject, PooledObjectName.EnemyBullet);
        }
    }
}
