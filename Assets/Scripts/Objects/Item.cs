using UnityEngine;

namespace Objects
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void ApplyBonus(PlayerController playerController);
        public abstract void RemoveBonus(PlayerController playerController);
    }
}
