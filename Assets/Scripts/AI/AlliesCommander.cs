using UnityEngine;

namespace AI
{
    public class AlliesCommander : MonoBehaviour
    {
        public AllyBehaviors allyBehavior = AllyBehaviors.Attacking;

        [HideInInspector] public Vector3 alliesDestination;

        //todo target selection public GameObject AlliesTarget;
        public Camera cam;

        private RaycastHit _hit;
        private int layerMask = 1 << 9;

        public void UpdateDestination()
        {
            if (Physics.Raycast(transform.position, cam.transform.forward, out _hit, 100,
                ~layerMask))
            {
                alliesDestination = _hit.point;
            }
        }

        public void UpdateTarget()
        {
            //todo change target
        }
    }
}