using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class RotateToTarget : ActionBase
{

    public override void Execute(IAIContext context)
    {
        var c = (MainContext)context;
        if (c.target == null) return;
        c.gun.transform.LookAt(c.target.transform.position);
    }
}
