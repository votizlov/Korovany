using Core;
using UnityEngine;

namespace Objects.Items
{
    public class ItemComponent : Interactable
    {
        [SerializeField] private GameProxy gameProxy;
        [SerializeField] private ItemTypes type;

        public override void Interact(PlayerController playerController)
        {
            Debug.Log("a");
            gameProxy.itemsController.PickupItem(playerController, type);
            //Destroy(gameObject);
        }
    }
}