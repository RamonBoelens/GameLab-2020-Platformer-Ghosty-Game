using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LOBBY = WAITING
// ROOM = ACTUAL GAME

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;

    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 20;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = false;

    public void PhotonLogin()
    {
        // If the client is not connected to the photon network ..
        if (!PhotonNetwork.IsConnected)
        {
            // .. then connect to the photon network
            ConnectToMasterServer();
        }
    }

    private void ConnectToMasterServer()
    {
        // Setup Settings
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();

        if (!PhotonNetwork.IsConnected)
            Debug.LogError("Client wasn't able to connect to the Photon Network");            
    }

    public void CreateRoom()
    {
        // Get the lobby code to generate the room.
        string lobbyCode = GetComponent<GenerateLobbyCode>().GetLobbyCode();

        // Setup room options object.
        RoomOptions options = new Photon.Realtime.RoomOptions()
        {
            MaxPlayers = MaxPlayersPerRoom
        };

        // Create the room
        PhotonNetwork.CreateRoom(lobbyCode, options);
        Debug.Log("Custom Room created.");

        // Send host to the host scene
        PhotonNetwork.LoadLevel("01_GameHost");
    }

    public void JoinRoom()
    {
        Debug.Log("Joining a room");

        // Get lobby code from the input field
        string lobbyCode = FindObjectOfType<LobbyCodeInput>().GetLobbyCode();

        // Try and join the room
        PhotonNetwork.JoinRoom(lobbyCode);
    }

    // .. When connected to the Photon Servers
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master.");
    }

    // .. When disconnected from the Photon Servers
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected due to: {cause}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Client failed to join the room because: '" + message + "'");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client succesfully joined the " + PhotonNetwork.CurrentRoom.Name + " room.");

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("01_GameHost");
        //else
        //    PhotonNetwork.LoadLevel("Game_Player");
    }
}
