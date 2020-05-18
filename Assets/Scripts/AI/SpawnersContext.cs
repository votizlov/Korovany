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
    public int treesOnLevel = 30;
    public UtilityAIComponent componentAI;
    public AnimationCurve itemRarityCurve;
    public AnimationCurve enemyRarityCurve;
    public GameObject[] enemiesSorted;
    public GameObject[] itemsSorted;
    public GameObject[] treeVariants;
    public bool isItemsSpawned;
    public TerrainData terrainData;
}