using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Objects.Items
{
    public class ItemsController : MonoBehaviour
    {
        public void PickupItem(PlayerController player, ItemTypes type)
        {
            
        }
    }

    public enum ItemTypes
    {
        Speed,
        ExplosiveRounds,
        ChallengeAltar
    }
}