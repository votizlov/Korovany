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
           //todo remove buying bypass if (gameProxy.currency >= itemCosts[(int) type])
                switch (type)
                {
                    case ItemTypes.Speed:
                        player.speed *= 1.1f;
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