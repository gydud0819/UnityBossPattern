using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetPlayerToTarget", story: "Find player to [TargetLocation] .", category: "Action/Find", id: "a12db2f6d6191c86b8217ebd86eea844")]
public partial class SetPlayerToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;


    protected override Status OnStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("태그가 플레이어인 객체가 없음");
            return Status.Failure;
            
        }
        TargetLocation.Value = player.transform.position;
        return Status.Success;
    }


}

