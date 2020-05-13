using System.Collections;
using UnityEngine;

namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float offset = 2;

        [SerializeField] private float maxX = 300;

        [SerializeField] private float maxZ = 300;

        [SerializeField] private GameObject objectToSpawn;

        [SerializeField] private float spawnFreq = 5;
        private Vector3 v;

        void Start()
        {
            StartCoroutine(spawn());
        }

        private IEnumerator spawn()
        {
            while (true)
            {
                v = new Vector3(Random.Range(0, maxX), offset, Random.Range(0, maxZ));
                Instantiate(objectToSpawn, v, Quaternion.identity);
                yield return new WaitForSeconds(spawnFreq);
            }
        }
    }
}