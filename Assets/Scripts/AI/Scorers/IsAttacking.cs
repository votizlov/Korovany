using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class IsAttacking : ContextualScorerBase
{
    public override float Score(IAIContext context)
    {
        var c = (MainContext) context;
        if (c.commander.allyBehavior == AllyBehaviors.Attacking)
            return 2;
        else
            return 0;
    }
}
