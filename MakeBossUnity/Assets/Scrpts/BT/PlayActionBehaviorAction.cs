using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayActionBehavior", story: "play [ActionBehavior] .", category: "Action/Pattern", id: "118a1ddf51d08269a0b3063127b39557")]
public partial class PlayActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<ActionBehavior> ActionBehavior;

    protected override Status OnStart()
    {
        ActionBehavior.Value.OnStart();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (ActionBehavior.Value.IsPatternEnd)
        {
            return Status.Success;
        }
        else
        {
            ActionBehavior.Value.OnUpdate();
            return Status.Running;

        }
    }

    protected override void OnEnd()
    {
        ActionBehavior.Value.OnEnd();
    }
}

