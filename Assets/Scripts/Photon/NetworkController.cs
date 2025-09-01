using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
    #region Variables

    [Header("Telas")]

    [Tooltip("GameObject da tela de Login")]
    [SerializeField] GameObject telaLogin;

    [Tooltip("GameObject da tela de Salas")]
    [SerializeField] GameObject telaSala;


    [Header("Player")]

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnPosition;
    
    [Tooltip("InputField que o jogador deve inserir seu nickname")]
    [SerializeField] InputField playerNameInput;

    [Tooltip("Nome temporário do jogador")]
    string playerNameTemp;


    [Header("Room")]

    [Tooltip("InputField do nome da sala")]
    [SerializeField] InputField roomName;

    [Tooltip("Texto do botão para se conectar à sala")]
    [SerializeField] Text connectionButtonText;

    #endregion


    #region Unity MonoBehaviour Methods

    void Start()
    {
        playerNameTemp = "Player " + Random.Range(1, 1000);
        playerNameInput.text = playerNameTemp;

        roomName.text = "Sala" + Random.Range(1, 1000);

        telaLogin.gameObject.SetActive(true);
        telaSala.gameObject.SetActive(false);
    }

    void Update()
    {
        //if (connect == false)
        //{
        //    PhotonNetwork.ConnectToRegion("eu");
        //}
    }

    #endregion


    #region Custom Methods

    public void StartServer()
    {
        // PhotonNetwork.ConnectUsingSettings();
    }

    public void Login()
    {
        if (playerNameInput.text == "")
        {
            PhotonNetwork.NickName = playerNameTemp;
        }
        else
        {
            PhotonNetwork.NickName = playerNameInput.text;
        }

        PhotonNetwork.ConnectUsingSettings();

        telaLogin.SetActive(false);
    }

    public void BuscarPartidaRapida()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CriarOuBuscarSala()
    {
        string roomTempName = roomName.text;
        RoomOptions roomOptions = new RoomOptions() {MaxPlayers = 4};
        PhotonNetwork.JoinOrCreateRoom(roomTempName, roomOptions, TypedLobby.Default);
    }

    #endregion


    #region Photon Callback Methods

    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConnected");

        //connect = true;
        //confirmButtonText.text = "CONNECTED";
    }

    public override void OnConnectedToMaster()
    {
        telaSala.SetActive(true);

        base.OnConnectedToMaster();
        print("OnConnectedToMaster");
        print("Server: " + PhotonNetwork.CloudRegion + " | Ping: " + PhotonNetwork.GetPing());

        //Login();
        //PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        print("OnJoinedRoom");
        print("Room Name: " + PhotonNetwork.CurrentRoom.Name);
        print("Current players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        telaLogin.gameObject.SetActive(false);
        telaSala.gameObject.SetActive(false);

        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition.position, playerSpawnPosition.rotation, 0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("A new player joined the room!");
        print("Current players in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        string roomTemp = "Room: " + Random.Range(1, 1000);
        PhotonNetwork.CreateRoom(roomTemp);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("OnDisconnected: " + cause);

        //PhotonNetwork.ConnectToRegion("eu");
        //connect = false;

        //confirmButtonText.text = "DISCONNECT";
    }

    #endregion
}