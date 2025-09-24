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

    // Animator�� �����ؼ� Setbool�� �̵��ϱ� Self GameObject Animator�� �����ͼ� animator ������ �����ϰ� Update true, Successs, false 

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

        // ���Ϳ� rigidbody�� ������ Status�� Failure�� �����
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
        //�ѹ��� �ٲ�ٸ� Start�� �־ ������ ����ؼ� ��ġ�� �ٲ�ٸ� Update�� ���� �� ���� 
        if(Self.Value.transform.position.x < TargetLocation.Value.x)
        {
            spriteRenderer.flipX = true;           // �׻� ����
        }
        else
        {
            spriteRenderer.flipX = false;            // �׻� ������ 
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
            rigidbody2D.linearVelocity = (TargetLocation.Value - Self.Value.transform.position).normalized * MoveSpeed.Value;          // �ڿ��� ���� ���ش�. �Ÿ��� �־����� �������� ������ ��������.
            return Status.Running;
        }
    }

}

