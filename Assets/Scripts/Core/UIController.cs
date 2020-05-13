using AI;
using Networking;
using Objects;
using UnityEngine;

namespace Core
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject alliesShop;
        [SerializeField] private GameObject alliesMenu;
        [SerializeField] private GameObject gameHUD;
        [SerializeField] private GameObject tutorial;
        [SerializeField] private GameObject classSelection;
        [SerializeField] private GameObject waitForPlayers;
        private AlliesShop _currShop;
        private AlliesCommander _currCommander;

        private bool _isMenuOpened;

        public void OnMainLoopStarted()
        {
            classSelection.SetActive(false);
            waitForPlayers.SetActive(false);
            tutorial.SetActive(true);
            gameHUD.SetActive(true);
        }

        public void OnClassSelectedText()
        {
            waitForPlayers.SetActive(true);
        }
        public void OpenAlliesShopMenu(AlliesShop shop)
        {
            if (_isMenuOpened) return;
            _isMenuOpened = true;
            _currShop = shop;
            alliesShop.SetActive(true);
        }

        public void OpenAlliesMenu(AlliesCommander commander)
        {
            if (_isMenuOpened) return;
            _isMenuOpened = true;
            _currCommander = commander;
            alliesMenu.SetActive(true);
        }

        public void CloseAlliesMenu()
        {
            if (_isMenuOpened)
            {
                alliesMenu.SetActive(false);
                _isMenuOpened = false;
            }
        }

        public void SpawnBoughtAlly(GameObject ally)
        {
            alliesShop.SetActive(false);
            _isMenuOpened = false;
            Instantiate(ally, _currShop.spawnPoint+_currShop.transform.position, Quaternion.identity);
        }

        public void ChangeAllyMode(int behavior)//enum нельзя передавать из кнопок, хотя эта фича была запрошена 6 лет назад
        {
            _currCommander.allyBehavior = (AllyBehaviors) behavior;
            alliesMenu.SetActive(false);
            _isMenuOpened = false;
        }

        public void ChangeAllyDestination()
        {
            _currCommander.UpdateDestination();
            alliesMenu.SetActive(false);
            _isMenuOpened = false;
        }

        public void ChangeAllyTarget()
        {
            _currCommander.UpdateTarget();
            alliesMenu.SetActive(false);
            _isMenuOpened = false;
        }
    }
}
