using System;
using Apex.AI;
using Apex.AI.Components;
using Core;
using Objects.Gun;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class EnemyContextProvider : MonoBehaviour, IContextProvider
    {
        [SerializeField] private GameProxy gameProxy;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Gun gun;
        private MainContext _context;

        private void Awake()
        {
            _context = new MainContext {agent = agent, possibleTargets = gameProxy.allies, gun = gun,currentPos = transform};
            gameProxy.enemies.Add(gameObject);
        }

        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }

        private void OnDestroy()
        {
            gameProxy.enemies.Remove(gameObject);
        }
    }
}
