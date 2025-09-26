using System.Collections;
using UnityEngine;

public class MushroomAttack1 : ActionBehavior
{
    Transform target;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] float waitTimeForCharging = 1f;            //  �� ������ �ð�
    [SerializeField] GameObject projectilePrefab;               // ����ü
    [SerializeField] float projectileRange = 180f;              // ����ü�� �߻��ϴ� ����
    [SerializeField] int repeatCount = 2;                       // ���Ϲݺ� Ƚ��

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
        // �÷��̾��� ���� ��ġ �������� flip�ϴ� �ڵ� ���� 
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
        // ������ ������ �� �ʱ�ȭ �ؾ��ϴ� ������ �ִٸ� OnEnd���� �����Ѵ�.
        StopCoroutine(ChargingPattern());
        base.OnStop();
    }

    IEnumerator ChargingPattern()
    {
        // �⸦ ������.
        animator.SetTrigger("A1");
        // �� ������ �ִϸ��̼� �����ؾ������� ���⿡ ����ϰ� �����ϱ� 
        yield return new WaitForSeconds(waitTimeForCharging);

        for (int i = 0; i < repeatCount; i++)
        {
            // 180�� ������ ����ü �߻��ϱ�

            Fire();
            yield return new WaitForSeconds(2f);
        }

        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(2f);

        IsPatternEnd = true;
    }

    private void Fire()
    {
        float currentAngle = SelectAnglePlayerPosition();             // ����

        float deltaAngle = projectileRange / 10;

        for (int i = 0; i < 10; i++)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);       // cos ����
            float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);       // sin ����

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

