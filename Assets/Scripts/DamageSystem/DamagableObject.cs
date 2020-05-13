using UnityEngine;

namespace DamageSystem
{
    public class DamagableObject : MonoBehaviour
    {
        [SerializeField] private int hp;

        public void RemoveHP(int hp)
        {
            this.hp -= hp;
            if (this.hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}