using System.Collections;
using Core;
using UnityEngine;

namespace DamageSystem
{
    public class AttackingObject : MonoBehaviour
    {
        public float range;
        public int damage;
        public float speed, reload;
        [SerializeField] private GameProxy gameProxy;

        private bool _isReloading = false;
        public AttackTypes type; 

//todo reload logic + attack properties
        public void Attack()
        {
            if (_isReloading) return;
            gameProxy.attackManager.Attack(this);
            StartCoroutine(Reloading());
        }
    
        private IEnumerator Reloading()
        {
            _isReloading = true;
            yield return new WaitForSeconds(reload);
            _isReloading = false;
        }

    }
}