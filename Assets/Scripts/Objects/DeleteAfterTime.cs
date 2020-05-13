using System.Collections;
using UnityEngine;

namespace Objects
{
    public class DeleteAfterTime : MonoBehaviour
    {
        [SerializeField] private float time;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Delete());
        }

        private IEnumerator Delete()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}