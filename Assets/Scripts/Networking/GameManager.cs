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

        private GameObject t;

        private PhotonView m_View;

        private int m_PlayersReady;

        void Awake()
        {
            gameProxy.attackManager = attackManager;
            gameProxy.players = new List<PlayerController>();
            gameProxy.UI = UI;
            gameProxy.allies = new List<GameObject>();
            gameProxy.enemies = new List<GameObject>();
            gameProxy.gameManager = this;
            gameProxy.itemsController = itemsController;
            m_View = GetComponent<PhotonView>();
        }

        public void SpawnPlayer(String selectedClass)
        {
            if (!m_View.IsMine && PhotonNetwork.IsConnected) return;
            if (settings.playerName == null)
            {
                t = Instantiate((GameObject) Resources.Load(selectedClass), playerStartPoint.position,
                    playerStartPoint.rotation);
            }
            else
            {
                t = PhotonNetwork.Instantiate(selectedClass, playerStartPoint.position, playerStartPoint.rotation);
            }

            gameProxy.allies.Add(t);
            gameProxy.players.Add(t.GetComponent<PlayerController>());

            classSelectionCam.SetActive(false);

            UI.OnClassSelectedText();
            m_PlayersReady++;
            if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom.PlayerCount == m_PlayersReady)
            {
                StartMainLoop();
            }
        }

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