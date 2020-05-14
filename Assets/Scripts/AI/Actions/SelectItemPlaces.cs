using System;
using Apex.AI;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI.Actions
{
    public class SelectItemPlaces : ActionBase
    {
        private float m_OffsetX;
        private float m_OffsetY;
        private int width = 250;
        private int height = 250;
        private float scale = 5f;
        private float m_TrashHold = 0.9f;
        private int m_SpawnedItems = 0;
        private float offset = 10;
        private int layermask = -1;
        private Vector3 t;

        public override void Execute(IAIContext context)
        {
            var c = (SpawnersContext) context;
            m_OffsetX = Random.Range(0f, 9999f);
            m_OffsetY = Random.Range(0f, 9999f);
            for (int x = -250; x < width; x += 10)
            {
                for (int z = -250; z < height; z += 10)
                {
                    if (m_SpawnedItems < c.itemsOnLevel && CalculateHeight(x, z) >= m_TrashHold)
                    {
                        m_SpawnedItems++;
                        t = getPosOnNavMesh(x, z);
                            //if (Math.Abs(t.x) < width && Math.Abs(t.z) < width & Math.Abs(t.y) < 250)
                            c.itemPlaces.Add(getPosOnNavMesh(x,z));
                    }
                }
            }
        }

        private Vector3 getPosOnNavMesh(int x, int z)
        {
            Vector3 randomDirection = Vector3.up * offset;

            randomDirection += new Vector3(x, -10, z);

            NavMeshHit navHit;

            NavMesh.SamplePosition(randomDirection, out navHit, offset, layermask);
            return navHit.position;
        }

        private float CalculateHeight(int x, int y)
        {
            float xCoord = (float) x / width * scale + m_OffsetX;
            float yCoord = (float) y / height * scale + m_OffsetY;

            return Mathf.PerlinNoise(xCoord, yCoord);
        }
    }
}