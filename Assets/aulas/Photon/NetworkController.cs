using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] bool connected = false;

    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] TMP_Text btnTexto;
    [SerializeField] bool firstConnect = true;
    [SerializeField] GameObject telaLogin;
    [SerializeField] GameObject telaJoin;
    [SerializeField] TMP_Text textoNickPlayer;
    string playerNameTemp;

    void Start()
    {
        playerNameTemp = $"Player {Random.Range(1,10000)}";
        playerNameInput.text = playerNameTemp;

        telaLogin.SetActive(true);
        telaJoin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!connected)
       // {
       //     PhotonNetwork.ConnectToRegion("eu");
       // }
    }

    public override void OnConnected()
    {
        base.OnConnected();
        print("conectado");
        connected = true;   
        firstConnect = false;
     
    }

    public void ConnectToServerBTN()
    {
        if (!connected )
        {
            PhotonNetwork.ConnectUsingSettings();
            btnTexto.text = "conectando";
        }
        else { btnTexto.text = "Servidor ja conectado"; StartCoroutine(ChangeBtnName()); }
        }
      

    IEnumerator ChangeBtnName()
    {
        btnTexto.text = "Já Conectado";
        yield return new WaitForSeconds(1);
        if (firstConnect)
        {
            btnTexto.text = "reconectar";
        }
        else
        {
            btnTexto.text = "Conectar";
        }
    }


    

    public void backToConnecting()
    {

    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("Conectado ao master");
        print($"Server: {PhotonNetwork.CloudRegion} \n Ping:{PhotonNetwork.GetPing()}");
        btnTexto.text = "Conectado";
        Login();
        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        string roomTemp = $"Room{Random.Range(1, 1000)}";
        PhotonNetwork.CreateRoom(roomTemp);
    }








    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        print($"OnDisconnected {cause}");
      //s  PhotonNetwork.ConnectToRegion("eu");
      firstConnect = false;

      connected = false;
        btnTexto.text = "reconectar";
        telaLogin.SetActive(true);
        telaJoin.SetActive(false);
    }

    public void Login()
    {
        if (playerNameInput.text == "")
        {
            PhotonNetwork.NickName = playerNameTemp;
            textoNickPlayer.text = playerNameTemp;

        }
        else
        {
            
            PhotonNetwork.NickName = playerNameInput.text;
            textoNickPlayer.text = playerNameInput.text;
        }
        telaLogin.SetActive(false);
        telaJoin.SetActive(true);
    }



 
}
