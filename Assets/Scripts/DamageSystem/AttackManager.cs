using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject pistolShotEffect;
    private Transform t;

    public void Attack(AttackingObject attackingObject)
    {
        switch (attackingObject.type)
        {
            case AttackTypes.PistolShot:
                t = attackingObject.transform;
                GameObject.Instantiate(pistolShotEffect, t.position, t.rotation);
                RaycastHit hit;

                if (Physics.Raycast(attackingObject.transform.position, attackingObject.transform.forward, out hit,
                    attackingObject.range))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    if (hit.collider.gameObject.CompareTag("Damagable"))
                    {
                        hit.collider.GetComponent<DamagableObject>().RemoveHP(attackingObject.damage);
                    }
                }


                break;
            default:
                Debug.LogError("Undefined attack");
                break;
        }
    }
}