using System.Collections;
using System.Collections.Generic;
using Data;
using Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //todo name input [SerializeField] private Text playerNameText;
    [SerializeField] private LobbyManager lobbyManager;
    [SerializeField] private SettingsData settings;

    public void StartSolo()
    {
        settings.playerName = null;
        SceneManager.LoadScene("Level1");
    }

    public void CreateRoom()
    {
        lobbyManager.CreateRoom();
    }

    public void JoinRoom()
    {
        lobbyManager.JoinRoom();
    }

    public void OpenNamePrompt()
    {
        
    }
}
