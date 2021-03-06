using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject ButtonManager;
    
    public GameObject ButtonCanvas;

  
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        ButtonManager.SetActive(true);
        ButtonCanvas.SetActive(true);
       
        Spawn();
    }


    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
       
    }

    //게임에서 나가지기를 만들기
    void Update() {
         if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
         {
            PhotonNetwork.Disconnect();
         } 
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //DisconnectPanel.SetActive(true);
        ButtonManager.SetActive(false);
        ButtonCanvas.SetActive(false);
       
    }
}




