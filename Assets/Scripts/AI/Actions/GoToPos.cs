using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class GoToPos : ActionBase
{

    public override void Execute(IAIContext context)
    {
        var c = (MainContext)context;
        c.agent.stoppingDistance = 0;
        c.agent.destination = c.targetPosition;
    }
}
