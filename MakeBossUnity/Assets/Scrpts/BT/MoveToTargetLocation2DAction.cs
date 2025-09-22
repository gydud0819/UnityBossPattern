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

    Rigidbody2D rigidbody2D;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    [SerializeReference] public BlackboardVariable<float> StoppingDistance;

    // Animator에 접근해서 Setbool로 이동하기 Self GameObject Animator를 가져와서 animator 변수에 저장하고 Update true, Successs, false 

    protected override Status OnStart()
    {
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
        if (Vector3.Distance(Self.Value.transform.position, TargetLocation.Value) < StoppingDistance.Value)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        else
        {
            rigidbody2D.linearVelocity = (TargetLocation.Value - Self.Value.transform.position).normalized * MoveSpeed.Value;          // 뒤에서 앞을 빼준다. 거리가 멀어지면 느려지고 가까우면 빨라진다.
            return Status.Running;
        }
    }

    // Success 이후에 실행되는 함수 
    //protected override void OnEnd()
    //{

    //}
}

