using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTargetLocation2D", story: "[Self] move to [TargetLocation] .", category: "Action/Navigation", id: "cfbdc072c0b4331664852f76c6415fae")]
public partial class MoveToTargetLocation2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    [SerializeReference] public BlackboardVariable<float> StoppingDistance;
    Rigidbody2D rigidbody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Animator에 접근해서 Setbool로 이동하기 Self GameObject Animator를 가져와서 animator 변수에 저장하고 Update true, Successs, false 

    protected override Status OnStart()
    {
        if(Self.Value.TryGetComponent<SpriteRenderer>(out var sr))
        {
            spriteRenderer = sr;
        }

        if (Self.Value.TryGetComponent<Animator>(out Animator anim))
        {
            animator = anim;
          
        }

        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < 0.1f)
        {
            return Status.Success;
        }

        // 몬스터에 rigidbody가 없으면 Status를 Failure로 만들기
        if (Self.Value.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid))
        {
            rigidbody2D = rigid;
            return Status.Running;
        }
        else
        {
            return Status.Failure;
        }



    }

    protected override Status OnUpdate()
    {
        //한번만 바뀐다면 Start에 넣어도 되지만 계속해서 위치가 바뀐다면 Update에 들어가는 게 맞음 
        if(Self.Value.transform.position.x < TargetLocation.Value.x)
        {
            spriteRenderer.flipX = true;           // 항상 왼쪽
        }
        else
        {
            spriteRenderer.flipX = false;            // 항상 오른쪽 
        }

        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < StoppingDistance.Value)     // StoppingDistance
        {
            animator.SetBool("IsRun", false);
            rigidbody2D.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        else
        {
            animator.SetBool("IsRun", true);
            rigidbody2D.linearVelocity = (TargetLocation.Value - Self.Value.transform.position).normalized * MoveSpeed.Value;          // 뒤에서 앞을 빼준다. 거리가 멀어지면 느려지고 가까우면 빨라진다.
            return Status.Running;
        }
    }

}

