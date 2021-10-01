using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAvatar : MonoBehaviour
{
    [SerializeField]
    private float maximumHealthPoint;

    [SerializeField]
    private float health;

    [SerializeField]
    private float maximumSpeed;

    private float lastTimeFired;

    [SerializeField]
    private float cooldownFiring;

    [SerializeField]
    private bool invincible;

    public DamageTakenEvent tookDamageEvent;

    public float Health
    {
        get => health;
        set => health = value;
    }

    public float MaximumSpeed
    {
        get => maximumSpeed;
        set => maximumSpeed = value;
    }
    public float MaximumHealthPoint
    {
        get => maximumHealthPoint;
        set => maximumHealthPoint = value;
    }
    public float LastTimeFired
    {
        get => lastTimeFired;
        set => lastTimeFired = value;
    }
    public float CooldownFiring
    {
        get => cooldownFiring;
        set => cooldownFiring = value;
    }

    public Vector2 Position
    {
        get => this.gameObject.transform.position;
        set => this.gameObject.transform.position = value;
    }
    public bool Invincible
    {
        get => invincible;
        set => invincible = value;
    }
    public void TakeDamage(float damageTaken)
    {
        if (!Invincible)
        {
            Health -= damageTaken;
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public virtual void Die() { }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaximumHealthPoint;
        tookDamageEvent = new DamageTakenEvent();
        tookDamageEvent.AddListener(TakeDamage);
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
