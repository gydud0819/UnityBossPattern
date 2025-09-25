using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopAllActionBehavior", story: "[Self] stop all [ActionBehavior] .", category: "Action/Pattern", id: "e401f6818530d6309639c19dfc4ee518")]
public partial class StopAllActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> ActionBehavior;

    List<ActionBehavior> stopActions = new();

    protected override Status OnStart()
    {
        // �ڵ尡 �ٲ� �� ���� �����͸� �߰��� 
        // stopActions�� �����Ͱ� ���ٸ� ã�Ƽ� ���� �͵��� �����ض� 

        if (stopActions.Count <= 0)
        {
            foreach (var action in ActionBehavior.Value)
            {
                

                if (action.TryGetComponent<ActionBehavior>(out var ab))
                {
                    stopActions = action.GetComponents<ActionBehavior>().ToList();
                }
            }
        }

        foreach (var action in stopActions)
        {
            action.OnStop();
        }

        Self.Value.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        return Status.Success;
    }


}

