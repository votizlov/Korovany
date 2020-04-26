using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SettingsData settings;

    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private Camera camera;

    [SerializeField] private float speed;

    [SerializeField] private float speedH = 2.0f;

    [SerializeField] private float speedV = 2.0f;

    private float yaw = 0f;

    private float pitch = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(settings.forward))
        {
            applyAcceleration(gameObject.transform.forward);
        } else if (Input.GetKey(settings.backward))
        {
            applyAcceleration(-gameObject.transform.forward);
        } else if (Input.GetKey(settings.left))
        {
            applyAcceleration(Quaternion.Euler(0,-90,0)*gameObject.transform.forward);
        } else if (Input.GetKey(settings.right))
        {
            applyAcceleration(Quaternion.Euler(0,90,0)*gameObject.transform.forward);
        } 
        if (Input.GetKey(settings.jump))
        {
            applyAcceleration(gameObject.transform.up);
        }

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        camera.transform.eulerAngles = new Vector3(pitch,yaw,0f);
        transform.eulerAngles = new Vector3(0f,yaw,0f);
    }

    private void applyAcceleration(Vector3 dir)
    {
        rigidbody.AddForce(dir*speed);
    }
}
