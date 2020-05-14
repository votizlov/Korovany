using System;
using Apex.AI;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.Actions
{
    public class SpawnEnemy : ActionBase
    {
        private int maxX = 250;
        private int maxZ = 250;
        private int offset = 5;
        private Vector3 v;
        public override void Execute(IAIContext context)
        {
            var c = (SpawnersContext) context;
            v = new Vector3(Random.Range(0, maxX), offset, Random.Range(0, maxZ));
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Instantiate(selectEnemyToSpawn(c), v, Quaternion.identity);
            else
                GameObject.Instantiate((GameObject) Resources.Load(selectEnemyToSpawn(c)), v, Quaternion.identity);
        }

        private String selectEnemyToSpawn(SpawnersContext c)
        {
            return "Enemy";
        }
    }
}