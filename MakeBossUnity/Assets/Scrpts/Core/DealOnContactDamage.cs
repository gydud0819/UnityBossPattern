using UnityEngine;

public class DealOnContactDamage : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [SerializeField] private int applyDamage;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagable>(out var damagable))
        {
            SetApplyDamage();
            damagable.TakeDamage(applyDamage);

            Destroy(gameObject);
        }
    }

    private void SetApplyDamage()
    {

    }
}
