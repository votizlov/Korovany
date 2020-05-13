using System.Collections;
using System.Collections.Generic;
using AI;
using Apex.AI;
using UnityEngine;
using UnityEngine.AI;

public class SelectRandomDestination : ActionBase
{
    private float distance = 10;
    private int layermask = -1;

    public override void Execute(IAIContext context)
    {
        var c = (MainContext) context;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
           
        randomDirection += c.currentPos.position;
           
        NavMeshHit navHit;
           
        NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);

        c.targetPosition = navHit.position;
    }
}
