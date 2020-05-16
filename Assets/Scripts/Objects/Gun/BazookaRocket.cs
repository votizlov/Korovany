using System;
using DamageSystem;
using Photon.Pun;
using UnityEngine;

namespace Objects.Gun
{
    public class BazookaRocket : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private GameObject prefab;

        private void OnCollisionEnter(Collision other)
        {
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Instantiate(prefab.name, gameObject.transform.position, Quaternion.identity);
            else
                Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            if (other.gameObject.CompareTag("Damagable"))
                other.gameObject.GetComponent<DamagableObject>().RemoveHP(damage);
            Destroy(gameObject);
        }
    }
}