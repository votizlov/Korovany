using System.Collections;
using System.Collections.Generic;
using Core;
using Objects;
using UnityEngine;

namespace Objects.Items
{
    public class ItemsController : MonoBehaviour
    {
        [SerializeField] private int[] itemCosts = {10, 10, 0};
        [SerializeField] private GameProxy gameProxy;

        public void PickupItem(PlayerController player, ItemTypes type)
        {
            if (gameProxy.currency >= itemCosts[(int) type])
                switch (type)
                {
                    case ItemTypes.Speed:

                        break;
                    case ItemTypes.ExplosiveRounds:
                        break;
                    case ItemTypes.ChallengeAltar:
                        break;
                }
        }
    }

    public enum ItemTypes
    {
        Speed,
        ExplosiveRounds,
        ChallengeAltar
    }
}