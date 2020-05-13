using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

namespace Objects.Items
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]private Gun.Gun primaryGun;
        //[SerializeField] private List<Gun.Gun> guns; todo different guns for class
        [SerializeField] private Transform gunPlace;
        //[SerializeField] private todo items saving

        public void Awake()
        {
            primaryGun = Instantiate(primaryGun, gunPlace.position, gunPlace.rotation);
            primaryGun.transform.parent = gunPlace;
        }

        public void FireCurrentGun()
        {
            primaryGun.Fire();
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
