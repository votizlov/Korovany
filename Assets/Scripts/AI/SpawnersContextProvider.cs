using System;
using System.Collections.Generic;
using Apex.AI;
using Apex.AI.Components;
using Core;
using UnityEngine;

namespace AI
{
    public class SpawnersContextProvider : MonoBehaviour, IContextProvider
    {
        [SerializeField] private AnimationCurve itemRarityCurve;
        [SerializeField] private AnimationCurve enemyRarityCurve;
        [SerializeField] private GameObject[] enemiesSorted;
        [SerializeField] private GameObject[] itemsSorted;
        [SerializeField] private UtilityAIComponent componentAI;

        public IAIContext GetContext(Guid aiId)
        {
            SpawnersContext c = new SpawnersContext
            {
                itemPlaces = new List<Vector3>(), componentAI = componentAI, itemRarityCurve = itemRarityCurve,
                enemyRarityCurve = enemyRarityCurve, itemsSorted = itemsSorted, enemiesSorted = enemiesSorted
            };
            return c;
        }
    }
}