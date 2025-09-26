using System.Collections;
using UnityEngine;

public class MushroomAttack1 : ActionBehavior
{
    Transform target;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] float waitTimeForCharging = 1f;            //  기 모으는 시간
    [SerializeField] GameObject projectilePrefab;               // 투사체
    [SerializeField] float projectileRange = 180f;              // 투사체가 발사하는 각도
    [SerializeField] int repeatCount = 2;                       // 패턴반복 횟수

    [SerializeField] float RightAngle = -60f;
    [SerializeField] float LeftAngle = 150f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void OnStart()
    {
        IsPatternEnd = false;
        StartCoroutine(ChargingPattern());

    }

    public override void OnUpdate()
    {
        // 플레이어의 현재 위치 방향으로 flip하는 코드 쓰기 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }

    }

    public override void OnStop()
    {
        // 패턴을 시작할 때 초기화 해야하는 패턴이 있다면 OnEnd에서 설정한다.
        StopCoroutine(ChargingPattern());
        base.OnStop();
    }

    IEnumerator ChargingPattern()
    {
        // 기를 모은다.
        animator.SetTrigger("A1");
        // 기 모으는 애니메이션 실행해야하지만 없기에 비슷하게 구현하기 
        yield return new WaitForSeconds(waitTimeForCharging);

        for (int i = 0; i < repeatCount; i++)
        {
            // 180도 각도로 투사체 발사하기

            Fire();
            yield return new WaitForSeconds(2f);
        }

        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(2f);

        IsPatternEnd = true;
    }

    private void Fire()
    {
        float currentAngle = SelectAnglePlayerPosition();             // 각도

        float deltaAngle = projectileRange / 10;

        for (int i = 0; i < 10; i++)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);       // cos 공식
            float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);       // sin 공식

            Vector2 Spawn = new Vector2(transform.position.x + dirX, transform.position.y + dirY);
            Vector2 dir = (Spawn - (Vector2)transform.position).normalized;

            if (projectileInstance.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = dir * 5;
            }
            currentAngle += deltaAngle;
        }

    }

    private float SelectAnglePlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (transform.position.x < player.transform.position.x)
        {
            return RightAngle;
        }
        else
        {
            return LeftAngle;
        }
    }

    public override void OnEnd()
    {
        
    }
}

