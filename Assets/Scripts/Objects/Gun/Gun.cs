using DamageSystem;
using UnityEngine;

namespace Objects.Gun
{
    public class Gun : MonoBehaviour
    {
        public AttackingObject attackingObject;
        [SerializeField] private int magCapacity;
        [SerializeField] private int maxInventoryRounds;

        public void Reload()
        {
        
        }

        public void Fire()
        {
            attackingObject.Attack();
        }
    }
}
