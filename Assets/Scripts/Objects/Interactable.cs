using UnityEngine;

namespace Objects
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact(PlayerController playerController);
    }
}
