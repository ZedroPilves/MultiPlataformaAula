using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] bool connected = false;


    [SerializeField] TMP_Text btnTexto;
    [SerializeField] bool firstConnect = true;
    void Start()
    {
        
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
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        print($"OnDisconnected {cause}");
      //s  PhotonNetwork.ConnectToRegion("eu");
      firstConnect = false;

      connected = false;
        btnTexto.text = "reconectar";
    }
}
