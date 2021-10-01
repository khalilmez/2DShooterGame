using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : BasicAvatar
{
    [SerializeField]
    private Rect playZone;
    [SerializeField]
    private GameObject explosionOnDeath;
    [SerializeField]
    private int scoreOnDeath;
    [SerializeField]
    private float damageOnCollision;
    [SerializeField]
    private GameObject BulletPrefab;

    public override void Die()
    {
        if(Health <= 0)
        {
            GameManager.Instance.PlayerKilledAnEnemy();
        }
        Instantiate(explosionOnDeath, transform.position, Quaternion.identity);
        Factory.Instance.ReturnObjectToThePool(this.gameObject, PooledObjectName.Enemy);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAvatar player = collision.gameObject.GetComponent<PlayerAvatar>();
        if ( player != null)
        {
            GameManager.Instance.PlayerKilledAnEnemy();
            player.TakeDamage(damageOnCollision);
            Die();
        }
    }
    private bool CanFire()
    {
        return (Time.time - LastTimeFired) >= CooldownFiring;
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = MaximumHealthPoint;
        LastTimeFired = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanFire())
        {
            LastTimeFired = Time.time;
            Fire();
        }
        this.gameObject.transform.position += new Vector3(-1, 0, 0)* MaximumSpeed * Time.deltaTime;
        if (!playZone.Contains(transform.position))
        {
            Die();
        }
    }

    private void Fire()
    {
        GameObject bullet = Factory.Instance.GetObjectFromPool(PooledObjectName.EnemyBullet);
        bullet.GetComponent<EnemyBullet>().Init(BulletType.EnemyBullet);
        bullet.transform.position = transform.GetChild(0).position;
        bullet.SetActive(true);
    }

    internal void StopMoving()
    {
        MaximumSpeed = 0;
    }
}
