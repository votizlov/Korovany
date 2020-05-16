using System;
using Apex.AI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.Actions
{
    public class SpawnEnemy : ActionBase
    {
        private int maxX = 200;
        private int maxZ = 200;
        private int offset = 10;
        private Vector3 v;
        private int x;
        private int z;
        private NavMeshHit hit;

        public override void Execute(IAIContext context)
        {
            var c = (SpawnersContext) context;
            x = Random.Range(-maxX, maxX);
            z = Random.Range(-maxZ, maxZ);
            v = new Vector3(x, c.terrainData.GetHeight(x, z), z);
            NavMesh.SamplePosition(v, out hit, offset, NavMesh.AllAreas);
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Instantiate(selectEnemyToSpawn(c), hit.position, Quaternion.identity);
            else
                GameObject.Instantiate((GameObject) Resources.Load(selectEnemyToSpawn(c)), v, Quaternion.identity);
        }

        private String selectEnemyToSpawn(SpawnersContext c)
        {
            return "Enemy";
        }
    }
}