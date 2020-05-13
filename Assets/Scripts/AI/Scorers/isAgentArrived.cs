using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class isAgentArrived : ContextualScorerBase
{
    public override float Score(IAIContext context)
    {
        var c = (MainContext) context;
        if (c.agent.remainingDistance < 1)
        {
            return 2;
        }

        return 0;
    }
}