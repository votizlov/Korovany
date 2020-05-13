using System.Collections;
using System.Collections.Generic;
using Apex.AI;
using UnityEngine;

public class randomGenerator : ContextualScorerBase
{
    public override float Score(IAIContext context)
    {
        return Random.value;
    }
}
