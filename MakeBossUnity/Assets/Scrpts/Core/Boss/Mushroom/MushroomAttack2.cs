using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack2 : ActionBehavior
{

    public override void OnEnd()
    {

    }

    public override void OnStart()
    {
        Debug.Log("���� ����2 ����");
      IsPatternEnd = true;
    }

    public override void OnUpdate()
    {

    }

   
}
