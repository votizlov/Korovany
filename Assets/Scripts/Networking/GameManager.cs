using System;
using System.Collections.Generic;
using Apex.AI.Components;
using Core;
using Data;
using Objects;
using Objects.Items;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform playerStartPoint;

        [SerializeField] private SettingsData settings;

        [SerializeField] private GameProxy gameProxy;

        [SerializeField] private ItemsController itemsController;

        [SerializeField] private AttackManager attackManager;

        [SerializeField] private UIController UI;

        [SerializeField] private GameObject classSelectionCam;

        [SerializeField] private UtilityAIComponent comp;

        [SerializeField] private PhotonView photonView;

        private GameObject t;

        private int m_PlayersReady = 0;

        void Awake()
        {
            gameProxy.attackManager = attackManager;
            gameProxy.players = new List<PlayerController>();
            gameProxy.UI = UI;
            gameProxy.allies = new List<GameObject>();
            gameProxy.enemies = new List<GameObject>();
            gameProxy.gameManager = this;
            gameProxy.itemsController = itemsController;
        }

        public void SpawnPlayer(String selectedClass)
        {
            if (!PhotonNetwork.IsConnected)
            {
                t = Instantiate((GameObject) Resources.Load(selectedClass), playerStartPoint.position,
                    playerStartPoint.rotation);
            }
            else
            {
                t = PhotonNetwork.Instantiate(selectedClass, playerStartPoint.position, playerStartPoint.rotation);
                photonView.RPC("PlayerReady", RpcTarget.All);
            }

            gameProxy.allies.Add(t);
            gameProxy.players.Add(t.GetComponent<PlayerController>());

            classSelectionCam.SetActive(false);

            UI.OnClassSelectedText();

            Debug.Log(m_PlayersReady);
            if (!PhotonNetwork.IsConnected)
            {
                StartMainLoop();
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == m_PlayersReady)
            {
                photonView.RPC("StartMainLoop", RpcTarget.All);
            }
        }

        [PunRPC]
        private void PlayerReady()
        {
            m_PlayersReady++;
        }

        [PunRPC]
        private void StartMainLoop()
        {
            foreach (var player in gameProxy.players)
            {
                player.isFreesed = false;
            }

            UI.OnMainLoopStarted();
        }

        public void OnLeftRoom()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnKorovanDestroyed()
        {
        }
    }
}