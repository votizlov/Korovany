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

        [SerializeField] private float jumpVelocity = 10;

        [SerializeField] private float fallMultipler = 2.5f;

        [SerializeField] private float lowJumpMultipler = 2f;

        private bool isHoldingJump;

        private float _yaw = 0f;

        private float _pitch = 0f;

        private bool _toggleMouseFollow = true;

        private void Start()
        {
            UI = gameProxy.UI;
            gunPlace.transform.forward = camera.transform.forward;
            if(!photonView.IsMine && PhotonNetwork.IsConnected)
                Destroy(camera);
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
                isHoldingJump = true;
            }

            if (Input.GetKey(settings.fire))
            {
                inventoryController.FireCurrentGun();
            }

            if (Input.GetKeyDown(settings.jump))
            {
                ApplyJumpVelocity();
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

        private void FixedUpdate()
        {
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity += Vector3.up * (Physics.gravity.y * (fallMultipler - 1) * Time.deltaTime);
            }
            else if (rigidbody.velocity.y > 0 && !isHoldingJump)
            {
                rigidbody.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime);
            }

            isHoldingJump = false;
        }

        private void Interact()
        {
            RaycastHit hit;

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit,
                2))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
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

        private void ApplyJumpVelocity()
        {
            rigidbody.velocity += Vector3.up * jumpVelocity;
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