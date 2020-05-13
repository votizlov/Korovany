using System.Collections.Generic;
using Networking;
using Objects;
using Objects.Items;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameProxy", order = 2)]
    public class GameProxy : ScriptableObject
    {
        public List<GameObject> allies;
        public List<GameObject> enemies;
        public AttackManager attackManager;
        public List<PlayerController> players;
        public GameManager gameManager;
        public UIController UI;
        public ItemsController itemsController;
        public int currency;
    }
}