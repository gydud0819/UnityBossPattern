using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    Player player;
    [Header("Fire 속성")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePos;
    [SerializeField] int fireRate;

    bool shouldFire;                // .
    float previousFireTime;         // 내가 직전에 발사한 시간 

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.OnFire += HandleFire;
    }

    private void OnDisable()
    {
        player.OnFire -= HandleFire;
        
    }

    private void HandleFire(bool enabled)
    {
        shouldFire = enabled;
    }

    private void Update()
    {
        if(!shouldFire)
        {
            return;
        }

        if (Time.time < previousFireTime + (1 / fireRate)) return;

        GameObject PJinstance = Instantiate(projectilePrefab, firePos.position, Quaternion.identity);

        float tmepValue = player.controls.Player.Move.ReadValue<float>();

        int playerForwarddir = 1;

        if(tmepValue < 0)
        {
            playerForwarddir = -1;
        }
        else if(tmepValue > 0) 
        {
            playerForwarddir = 1;
        }

            PJinstance.GetComponent<Rigidbody2D>().linearVelocity = Vector3.right * playerForwarddir * 10f;
        previousFireTime = Time.time;
    }
}
