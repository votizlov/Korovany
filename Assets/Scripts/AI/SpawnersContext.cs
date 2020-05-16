using System.Collections;
using System.Collections.Generic;
using Apex.AI;
using Apex.AI.Components;
using UnityEngine;

public class SpawnersContext : IAIContext
{
    public List<Vector3> itemPlaces;
    public Vector3 lastEnemyPlace;
    public int itemsOnLevel = 20;
    public UtilityAIComponent componentAI;
    public AnimationCurve itemRarityCurve;
    public AnimationCurve enemyRarityCurve;
    public GameObject[] enemiesSorted;
    public GameObject[] itemsSorted;
    public bool isItemsSpawned;
    public TerrainData terrainData;
}