using System;
using Data;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text logText;
        [SerializeField] private SettingsData settings;

        private bool isNewRoom;

        private void LoadSettings()
        {
            Log("Settings loaded");
            PhotonNetwork.NickName = settings.playerName;
            PhotonNetwork.GameVersion = settings.gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom()
        {
            if (settings.playerName == null)
            {
                Log("Specify player name");
                return;
            }

            isNewRoom = true;

            LoadSettings();
        }

        public void JoinRoom()
        {
            if (settings.playerName == null)
            {
                Log("Specify player name");
                return;
            }

            LoadSettings();
        }

        public override void OnJoinedRoom()
        {
            Log("Joined the room");
            PhotonNetwork.LoadLevel("Arena");
        }

        public override void OnConnectedToMaster()
        {
            Log("Connected to master");
            if (isNewRoom)
                PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions {MaxPlayers = 4});
            else
                PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
        }

        private void Log(String message)
        {
            Debug.Log(message);
            logText.text += "\n";
            logText.text += message;
        }
    }
}