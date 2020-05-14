using System.Collections.Generic;
using Apex.AI;
using Objects;
using Objects.Gun;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class MainContext : IAIContext
    {
        public List<GameObject> possibleTargets;
        public NavMeshAgent agent;
        public Gun gun;
        public GameObject target;
        public Transform currentPos;
        public Vector3 targetPosition;
        public List<PlayerController> currPlayers;
        public AlliesCommander commander;
        public bool isGivenOrder = false;
        public Vector3 lastOrderedPos;
    }
}