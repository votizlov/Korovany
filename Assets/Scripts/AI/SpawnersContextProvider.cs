using System;
using Apex.AI;
using Apex.AI.Components;
using UnityEngine;

namespace AI
{
    public class SpawnersContextProvider : MonoBehaviour, IContextProvider
    {
        [SerializeField] private AnimationCurve itemRarityCurve;

        public IAIContext GetContext(Guid aiId)
        {
            return new SpawnersContext();
        }
    }
}
