using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameState { Playing, WonGame, GameOver }
public class GameManager : MonoBehaviour
{
    private PlayerAvatar player;
    private GameState state;
    [SerializeField]
    private Slider sliderHealth;
    [SerializeField]
    private Slider slideMana;
    [SerializeField]
    private int killsLeftToWin;
    private static GameManager instance;
    private float cooldownRandomSpawn = 1;
    private float lastTimeSpawned;
    public GameState State
    {
        get => state;
        private set => state = value;
    }
    public int EnemiesLeft
    {
        get => killsLeftToWin;
        private set => killsLeftToWin = value;
    }
    public static GameManager Instance
    {
        get => instance;
        private set => Instance = value;
    }
    public void PlayerKilledAnEnemy()
    {
        killsLeftToWin = killsLeftToWin -1;
        if(killsLeftToWin == 0)
        {
            state = GameState.WonGame;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyAvatar>();
        foreach (EnemyAvatar enemie in enemies)
        {
            Factory.Instance.ReturnObjectToThePool(enemie.gameObject, PooledObjectName.Enemy);
        }
        var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
        foreach (EnemyBullet bullet in bullets)
        {
            Factory.Instance.ReturnObjectToThePool(bullet.gameObject, PooledObjectName.EnemyBullet);
        }
        instance = this;
        state = GameState.Playing;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderHealth.value = player.Health / player.MaximumHealthPoint;
        slideMana.value = player.Energy / player.MaximumEnergy;
        if (player.Health == 0)
        {
            state = GameState.GameOver;
        }
        if ((Time.time - lastTimeSpawned) >= cooldownRandomSpawn)
        {
            SpawnEnemy();
            lastTimeSpawned = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        float height = UnityEngine.Random.Range(-4f,4f);
        GameObject enemy = Factory.Instance.GetObjectFromPool(PooledObjectName.Enemy);
        enemy.GetComponent<EnemyAvatar>().MaximumSpeed = 2;
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(10, height,0);
    }

    internal void WonGame()
    {
        player.Invincible = true;
    }
}
