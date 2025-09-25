using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class MushRoom : MonoBehaviour, IDamagable
{
    // 행동은 총 7개 
    // Idle, Run, Stun, Attack, Hit, Die, Attack2 
    // 각 상태를 코드로 정의하고 조립하는 방식으로 사용해보기
    // 유한 상태 머신 finate state machine, Behavior tree

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
            Debug.Log("죽음");
            behaviorAgent.SetVariableValue<BossState>("BossState", BossState.Die);
        }
    }

    
    private bool IsStun()
    {
        // 주사위 돌려서 4이상이면 스턴
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
