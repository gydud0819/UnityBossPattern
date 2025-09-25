using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class MushRoom : MonoBehaviour, IDamagable
{
    // �ൿ�� �� 7�� 
    // Idle, Run, Stun, Attack, Hit, Die, Attack2 
    // �� ���¸� �ڵ�� �����ϰ� �����ϴ� ������� ����غ���
    // ���� ���� �ӽ� finate state machine, Behavior tree

    BehaviorGraphAgent behaviorAgent;
    [SerializeField] float MaxHP = 100;
   [field : SerializeField] public float CurrentHP { get; private set; }

    

    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<BossState>("BossState", BossState.Idle);
        CurrentHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;

        if(IsStun())
        {
            WhenStun();
        }

        if (CurrentHP <= 0)
        {
            Debug.Log("����");
            behaviorAgent.SetVariableValue<BossState>("BossState", BossState.Die);
        }
    }

    
    private bool IsStun()
    {
        // �ֻ��� ������ 4�̻��̸� ����
        int rand = Random.Range(0, 7);

        if (rand <= 4) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void WhenStun()
    {
        behaviorAgent.SetVariableValue<BossState>("BossState", BossState.Stun);

    }

    public void Update()
    {
        if(Keyboard.current.tKey.IsPressed())
        {
          
            TakeDamage(5.0f);
        }
    }
}
