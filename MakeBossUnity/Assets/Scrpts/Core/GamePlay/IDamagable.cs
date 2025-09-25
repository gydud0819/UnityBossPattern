using UnityEngine;

public interface IDamagable 
{
    float CurrentHP {  get; }         // 프로퍼티
    public void TakeDamage(float damage);
}
