using System;
using System.Collections.Generic;
using Apex.AI.Components;
using Core;
using Data;
using Objects;
using Objects.Items;
using PathCreation;
using PathCreation.Examples;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform playerStartPoint;

        [SerializeField] private float korovanSpawnTime;

        [SerializeField] private Transform korovanStartPoint;

        [SerializeField] private SettingsData settings;

        [SerializeField] private GameProxy gameProxy;

        [SerializeField] private ItemsController itemsController;

        [SerializeField] private AttackManager attackManager;

        [SerializeField] private UIController UI;

        [SerializeField] private GameObject classSelectionCam;

        [SerializeField] private UtilityAIComponent comp;

        [SerializeField] private PhotonView photonView;

        [SerializeField] private UtilityAIComponent utilityAiComponent;

        [SerializeField] private Timer timer;

        [SerializeField] private GameObject korovanPrefab;

        [SerializeField] private PathCreator roadPathCreator;

        private GameObject t;

        private int m_PlayersReady = 0;

        private bool isKorovanSpawned = false;

        void Awake()
        {
            timer.OnTimerTick += CheckTimer;
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
                return;
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

            utilityAiComponent.clients[0].Start();
            
            timer.StartTimer();

            UI.OnMainLoopStarted();
        }

        private void CheckTimer(float time)
        {
            if (time >= korovanSpawnTime && !isKorovanSpawned)
            {
                GameObject t;
                if (PhotonNetwork.IsConnected)
                {
                    t = PhotonNetwork.Instantiate(korovanPrefab.name, korovanStartPoint.position,
                        korovanStartPoint.rotation);
                }
                else
                    t = Instantiate(korovanPrefab, korovanStartPoint.position, korovanStartPoint.rotation);

                isKorovanSpawned = true;
                t.GetComponent<PathFollower>().pathCreator = roadPathCreator;
            }
        }

        private GameObject ChooseKorovan()
        {
            return korovanPrefab;
        }

        public void OnLeftRoom()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnKorovanLeft()
        {
            timer.ResetTimer();
        }

        public void OnKorovanDestroyed()
        {
            //todo go to next stage via portal
        }
    }
}