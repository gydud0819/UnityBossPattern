using UnityEngine;

public interface IDamagable 
{
    float CurrentHP {  get; }         // ������Ƽ
    public void TakeDamage(float damage);
}
