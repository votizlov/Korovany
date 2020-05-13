using System;
using System.Collections.Generic;
using AI;
using Core;
using Data;
using Objects;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerPrefab;

        [SerializeField] private Transform playerStartPoint;

        [SerializeField] private SettingsData settings;

        [SerializeField] private GameProxy gameProxy;
        
        [SerializeField] private AttackManager attackManager;

        [SerializeField] private UIController UI;

        [SerializeField] private GameObject classSelectionCam;

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
            m_View = GetComponent<PhotonView>();
        }

        public void SpawnPlayer(String selectedClass)
        {
            if (!m_View.IsMine) return;
            if (settings.playerName == null)
            {
                t = Instantiate(playerPrefab, playerStartPoint.position, playerStartPoint.rotation);
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
            if (PhotonNetwork.CurrentRoom.PlayerCount == m_PlayersReady)
            {
                StartMainLoop();
            }
        }

        private void StartMainLoop()
        {
            UI.OnMainLoopStarted();
        }

        public void OnLeftRoom()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}