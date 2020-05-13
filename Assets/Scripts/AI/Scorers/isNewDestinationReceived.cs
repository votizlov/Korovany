using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class isNewDestinationReceived : ContextualScorerBase
{

    public override float Score(IAIContext context)
    {
        var c = (MainContext)context;
        if (c.commander.alliesDestination != c.lastOrderedPos)
        {
            c.targetPosition = c.commander.alliesDestination;
            c.lastOrderedPos = c.commander.alliesDestination;
            return 3;
        }

        return 0;
    }
}
