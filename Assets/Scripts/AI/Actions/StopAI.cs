using System.Collections;
using System.Collections.Generic;
using Apex.AI;
using UnityEngine;

public class StopAI : ActionBase
{
    public override void Execute(IAIContext context)
    {
        var c = (SpawnersContext) context;
        c.componentAI.clients[0].Stop();
    }
}