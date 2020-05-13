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
        public event Action<PlayerController> OnInteract;

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
            if (!photonView.IsMine) return;
            if (Input.GetKey(settings.forward))
            {
                applyAcceleration(gameObject.transform.forward);
            }

            if (Input.GetKey(settings.backward))
            {
                applyAcceleration(-gameObject.transform.forward);
            }

            if (Input.GetKey(settings.left))
            {
                applyAcceleration(Quaternion.Euler(0, -90, 0) * gameObject.transform.forward);
            }

            if (Input.GetKey(settings.right))
            {
                applyAcceleration(Quaternion.Euler(0, 90, 0) * gameObject.transform.forward);
            }

            if (Input.GetKey(settings.jump))
            {
                applyAcceleration(gameObject.transform.up);
            }

            if (Input.GetKey(settings.fire))
            {
                inventoryController.FireCurrentGun();
            }

            if (Input.GetKeyDown(settings.interaction))
            {
                OnInteract?.Invoke(this);
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

        private void applyAcceleration(Vector3 dir)
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