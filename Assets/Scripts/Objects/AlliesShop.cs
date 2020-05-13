using Core;
using UnityEngine;

namespace Objects
{
    public class AlliesShop : MonoBehaviour
    {
        [SerializeField] private GameProxy gameProxy;
        public Vector3 spawnPoint;
        private bool _isPlayerInRange;


        private void Start()
        {
            //gameProxy.player.OnInteract += OpenMenu;
        }


        private void OnDisable()
        {
            //gameProxy.player.OnInteract -= OpenMenu;
        }

        private void OpenMenu(PlayerController player)
        {
            if (_isPlayerInRange)
            {
                gameProxy.UI.OpenAlliesShopMenu(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 9)
                _isPlayerInRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 9)
                _isPlayerInRange = false;
        }
    }
}