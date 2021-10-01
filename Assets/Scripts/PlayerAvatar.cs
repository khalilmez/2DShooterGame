using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootingStyle { Continue, Diagonal, Spriral }
public class PlayerAvatar : BasicAvatar
{
    [SerializeField]
    private float maximumEnergy;

    [SerializeField]
    private float energy;

    [SerializeField]
    private GameObject bulletPrefab;

    private ShootingStyle firingTypePlayer;

    public DamageTakenEvent tookDamageEvent;

    private SpriteRenderer playerSprite;

    public float Energy
    {
        get => energy;
        private set => energy = value;
    }
    public float MaximumEnergy
    {
        get => maximumEnergy;
        private set => maximumEnergy = value;
    }

    public ShootingStyle FiringTypePlayer
    {
        get => firingTypePlayer;
        private set => firingTypePlayer = value;
    }
    public bool CanFire()
    {
        return (Time.time - LastTimeFired) >= CooldownFiring;
    }
    public void Fire()
    {
        if (CanFire() && energy>=10)
        {
            Energy -= 10;
            GameObject bullet = Factory.Instance.GetObjectFromPool(PooledObjectName.PlayerBullet);
            bullet.GetComponent<PlayerBullet>().Init(BulletType.PlayerBulletContinue);
            bullet.SetActive(true);
            LastTimeFired = Time.time;
        }
    }

    public void SwitchWeapon()
    {
        firingTypePlayer = (ShootingStyle)((int)(firingTypePlayer + 1) % Enum.GetNames(typeof(ShootingStyle)).Length);
    }
    public override void Die()
    {
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = MaximumHealthPoint;
        Energy = MaximumEnergy;
        Invincible = false;
        tookDamageEvent = new DamageTakenEvent();
        tookDamageEvent.AddListener(TakeDamage);
        LastTimeFired = -CooldownFiring;
        FiringTypePlayer = ShootingStyle.Continue;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(addEnergy());
    }

    public IEnumerator addEnergy()
    {
        if (CanFire() && !Invincible && Time.timeScale!=0)
        {
            Energy = Mathf.Min(100f, Energy + 0.3f);
        }
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return WaitForSeconds(seconds);
    }

    //AfterDashing
    public IEnumerator BecomeInvincible()
    {
        Invincible = true;
        for (int i = 0; i < 15; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        Invincible = false;
    }
    public void DashUp()
    {
        if (Energy >= 50)
        {
            StartCoroutine(BecomeInvincible());
            Energy -= 50; 
            this.gameObject.transform.position += new Vector3(0, Mathf.Min(4, 4f - this.gameObject.transform.position.y), 0);
        }

    }
    public void DashDown()
    {
        if (Energy >= 50)
        {
            StartCoroutine(BecomeInvincible());
            Energy -= 50;
            this.gameObject.transform.position += new Vector3(0, Mathf.Max(-4, -4f - this.gameObject.transform.position.y), 0);
        }

    }
}
