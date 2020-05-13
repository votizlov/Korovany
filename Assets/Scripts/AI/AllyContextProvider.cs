using System;
using Apex.AI;
using Apex.AI.Components;
using Core;
using Objects.Gun;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AllyContextProvider : MonoBehaviour, IContextProvider
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private GameProxy gameProxy;
        [SerializeField] private Gun gun;

        private MainContext _context;

        private void Awake()
        {
            _context = new MainContext
            {
                agent = agent, possibleTargets = gameProxy.enemies, gun = gun, currentPos = transform,
                currPlayers = gameProxy.players,
                //commander = gameProxy.commander
            };
            gameProxy.allies.Add(gameObject);
        }

        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }

        private void OnDestroy()
        {
            gameProxy.allies.Remove(gameObject);
        }
    }
}