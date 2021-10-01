using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Rect allowedZone;

    private float hMove;

    private float vMov;

    private Vector2 newPosition;

    private PlayerAvatar player;

    private float doubleClickTime = 0.2f;

    private float LastClickTimeUp = -1f;

    private float LastClickTimeDown = -1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxis("Horizontal");
        vMov = Input.GetAxis("Vertical"); 
        if (Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.Z))
        {
            float TimeDiff = Time.time - LastClickTimeUp;
            LastClickTimeUp = Time.time;
            if (TimeDiff <= doubleClickTime)
            {
                player.DashUp();
            }
        }
        if (Input.GetKeyDown("down") || Input.GetKeyDown(KeyCode.S))
        {
            float TimeDiff = Time.time - LastClickTimeDown;
            LastClickTimeDown = Time.time;
            if (TimeDiff <= doubleClickTime)
            {
                player.DashDown();
            }
        }
        if (player != null && Input.GetButton("Fire1"))
        {
            player.Fire();
        }
        if(player != null && Input.GetKeyDown(KeyCode.Tab))
        {
            player.SwitchWeapon();
        }
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            newPosition = player.Position + new Vector2(hMove, vMov) * player.MaximumSpeed * Time.deltaTime;
            if (allowedZone.Contains(newPosition))
            {
                player.Position = newPosition;
            }
        }
    }
}
