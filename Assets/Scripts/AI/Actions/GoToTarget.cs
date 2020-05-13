using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class GoToTarget : ActionBase
{

    public override void Execute(IAIContext context)
    {
        var c = (MainContext)context;
        if(c.target==null) return;
        c.agent.destination = c.target.transform.position;
    }
}
