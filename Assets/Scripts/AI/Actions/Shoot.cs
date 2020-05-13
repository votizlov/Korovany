using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class Shoot : ActionBase
{
    public override void Execute(IAIContext context)
    {
        var c = (MainContext) context;
        c.gun.Fire();
    }
}
