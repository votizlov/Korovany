using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using Photon.Pun;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject pistolShotEffect;
    public GameObject bazookaShotEffect;
    public GameObject swordSwingEffect;
    private Transform t;

    public void Attack(AttackingObject attackingObject)
    {
        switch (attackingObject.type)
        {
            case AttackTypes.PistolShot:
                t = attackingObject.transform;
                if (PhotonNetwork.IsConnected)
                    PhotonNetwork.Instantiate(pistolShotEffect.name, t.position, t.rotation);
                else
                    GameObject.Instantiate(pistolShotEffect, t.position, t.rotation);
                RaycastHit hit;

                if (Physics.Raycast(attackingObject.transform.position, attackingObject.transform.forward, out hit,
                    attackingObject.range))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                        Color.yellow);
                    if (hit.collider.gameObject.CompareTag("Damagable"))
                    {
                        hit.collider.GetComponent<DamagableObject>().RemoveHP(attackingObject.damage);
                    }
                }


                break;
            case AttackTypes.BazookaShot:
                t = attackingObject.transform;
                if (PhotonNetwork.IsConnected)
                    PhotonNetwork.Instantiate(bazookaShotEffect.name, t.position, t.rotation);
                else
                    GameObject.Instantiate(bazookaShotEffect, t.position, t.rotation);
                break;
            
            case AttackTypes.SwordSwing:
                t = attackingObject.transform;
                if (PhotonNetwork.IsConnected)
                    PhotonNetwork.Instantiate(swordSwingEffect.name, t.position, t.rotation);
                else
                    GameObject.Instantiate(swordSwingEffect, t.position, t.rotation);
                break;
            default:
                Debug.LogError("Undefined attack");
                break;
        }
    }
}