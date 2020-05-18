using System;
using System.Collections;
using System.Collections.Generic;
using Apex.AI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlaceTrees : ActionBase
{
    private float m_OffsetX;
    private float m_OffsetY;
    private int m_SpawnedItems = 0;
    private int width = 250;
    private int height = 250;
    private float scale = 5f;
    private float m_TrashHold = 0.9f;
    private NavMeshHit hit;
    private float offset = 10;
    private int count = 0;

    public override void Execute(IAIContext context)
    {
        var c = (SpawnersContext) context;
        m_OffsetX = Random.Range(0f, 9999f);
        m_OffsetY = Random.Range(0f, 9999f);
        for (int x = -250; x < width; x += 10)
        {
            for (int z = -250; z < height; z += 10)
            {
                if (m_SpawnedItems < c.treesOnLevel && CalculateHeight(x, z) >= m_TrashHold)
                {
                    m_SpawnedItems++;
                    NavMesh.SamplePosition(new Vector3(x, c.terrainData.GetHeight(x, z), z), out hit, offset,
                        NavMesh.AllAreas);
                    if (PhotonNetwork.IsConnected)
                        PhotonNetwork.Instantiate(selectItemToSpawn(c), hit.position, Quaternion.identity);
                    else
                        GameObject.Instantiate((GameObject) Resources.Load(selectItemToSpawn(c)), hit.position,
                            Quaternion.identity); //todo сделать, чтобы предметы 'лежали' на меше с помощью нормалей
                }
            }
        }
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale + m_OffsetX;
        float yCoord = (float) y / height * scale + m_OffsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private String selectItemToSpawn(SpawnersContext c)
    {
        if (count == c.treeVariants.Length) count = 0;
        return c.treeVariants[count++].name;
    }
}