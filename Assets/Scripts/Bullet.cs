using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType { PlayerBulletContinue, PlayerBulletDiagonalUp, PlayerBulletDiagonalDown, PlayerBulletSpriral, EnemyBullet }

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rect playZone;
    [SerializeField]
    private float damage;
    [SerializeField]
    private Vector3 speed;
    private Vector2 initialPosition;

    public float Damage
    {
        get =>damage;
        private set => damage = value;
    }
    public Rect PlayZone
    {
        get => playZone;
        set => playZone = value;
    }
    public Vector3 Speed
    {
        get => speed;
        set => speed = value;
    }
    public Vector2 InitialPosition
    {
        get => initialPosition;
        set => initialPosition = value;
    }
    public void StopMoving()
    {
        speed = Vector3.zero;
    }
    virtual public void Init(BulletType bulType) { }
    virtual public void UpdatePosition() { }
}
