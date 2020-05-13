using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class isExecutingOrder : ContextualScorerBase
{
    public override float Score(IAIContext context)
    {
        var c = (MainContext) context;
        if (c.isGivenOrder)
        {
            return 3;
        }

        return 0;
    }
}