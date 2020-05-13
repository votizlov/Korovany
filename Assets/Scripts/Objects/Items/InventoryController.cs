using System.Collections.Generic;
using UnityEngine;

namespace Objects.Items
{
    public class InventoryController : MonoBehaviour
    {
        public Gun.Gun currentGun;
        [SerializeField] private List<Gun.Gun> guns;
        [SerializeField] private Transform gunPlace;

        public void Awake()
        {
            //todo инстанцирование пушек
            currentGun = Instantiate(currentGun, gunPlace.position, gunPlace.rotation);
            currentGun.transform.parent = gunPlace;
        }

        public void FireCurrentGun()
        {
            currentGun.Fire();
        }

        public void ReloadCurrentGun()
        {
        
        }

        public void AddGun()
        {
        
        }

        public void AddRounds()
        {
        
        }
    }
}
