using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class ExecutedOrderState : ActionBase
{

    public override void Execute(IAIContext context)
    {
        var c = (MainContext) context;
        c.isGivenOrder = false;
    }
}
