using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;

public class FindClosestTarget : ActionBase
{
    public override void Execute(IAIContext context)
    {
        var c = (MainContext) context;
        if (c.possibleTargets.Count == 0)
        {
            c.target = null;
            return;
        }
        float range = Vector3.Distance(c.currentPos.position, c.possibleTargets[0].transform.position);
        GameObject closest = c.possibleTargets[0];
        foreach (var tgt in c.possibleTargets)
        {
            if (Vector3.Distance(c.currentPos.position, tgt.transform.position) < range)
            {
                closest = tgt;
                range = Vector3.Distance(c.currentPos.position, tgt.transform.position);
            }
        }

        c.target = closest;
    }
}
