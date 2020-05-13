using System;
using AI;
using Core;
using Data;
using Objects.Items;
using Photon.Pun;
using UnityEngine;

namespace Objects
{
    public class PlayerController : MonoBehaviour, IPunObservable
    {

        public bool isFreesed = true;
        
        public Camera camera;

        [SerializeField] private SettingsData settings;

        [SerializeField] private GameProxy gameProxy;

        [SerializeField] private AlliesCommander alliesCommander;

        [SerializeField] private InventoryController inventoryController;

        [SerializeField] private UIController UI;

        [SerializeField] private Rigidbody rigidbody;

        [SerializeField] private PhotonView photonView;

        [SerializeField] private GameObject gunPlace;

        [SerializeField] private float speed;

        [SerializeField] private float speedH = 2.0f;

        [SerializeField] private float speedV = 2.0f;

        private float _yaw = 0f;

        private float _pitch = 0f;

        private bool _toggleMouseFollow = true;

        private void Start()
        {
            UI = gameProxy.UI;
            gunPlace.transform.forward = camera.transform.forward;
        }

        void Update()
        {
            if (isFreesed) return;
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            if (Input.GetKey(settings.forward))
            {
                ApplyAcceleration(gameObject.transform.forward);
            }

            if (Input.GetKey(settings.backward))
            {
                ApplyAcceleration(-gameObject.transform.forward);
            }

            if (Input.GetKey(settings.left))
            {
                ApplyAcceleration(Quaternion.Euler(0, -90, 0) * gameObject.transform.forward);
            }

            if (Input.GetKey(settings.right))
            {
                ApplyAcceleration(Quaternion.Euler(0, 90, 0) * gameObject.transform.forward);
            }

            if (Input.GetKey(settings.jump))
            {
                ApplyAcceleration(gameObject.transform.up);
            }

            if (Input.GetKey(settings.fire))
            {
                inventoryController.FireCurrentGun();
            }

            if (Input.GetKeyDown(settings.interaction))
            {
                Interact();
            }

            if (Input.GetKeyDown(settings.openAlliesMenu))
            {
                _toggleMouseFollow = false;
                UI.OpenAlliesMenu(alliesCommander);
            }

            if (Input.GetKeyUp(settings.openAlliesMenu))
            {
                _toggleMouseFollow = true;
                UI.CloseAlliesMenu();
            }

            if (_toggleMouseFollow)
            {
                _yaw += speedH * Input.GetAxis("Mouse X");
                _pitch -= speedV * Input.GetAxis("Mouse Y");
                transform.eulerAngles = new Vector3(0f, _yaw, 0f);
                camera.transform.eulerAngles = new Vector3(_pitch, _yaw, 0f);
                gunPlace.transform.eulerAngles = new Vector3(_pitch, _yaw, 0f);
            }
        }

        private void Interact()
        {
            RaycastHit hit;

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit,
                2))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.CompareTag("Interactable"))
                {
                    hit.collider.GetComponent<Interactable>().Interact(this);
                }
            }
        }

        private void ApplyAcceleration(Vector3 dir)
        {
            rigidbody.AddForce(dir * speed);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                Vector3 pos = transform.localPosition;
                stream.Serialize(ref pos);
            }
            else
            {
                Vector3 pos = Vector3.zero;
                stream.Serialize(ref pos); // pos gets filled-in. must be used somewhere
            }
        }
    }
}