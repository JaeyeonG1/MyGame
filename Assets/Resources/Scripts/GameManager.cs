﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public GameObject playerMaster;
    public GameObject playerClient;
    public GameObject MainMaster;
    public GameObject MainClient;

    private void Start()
    {        Instance = this;

        if (playerMaster == null || playerClient == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            if (PhotonNetwork.CurrentRoom.PlayerCount % 2 == 1)
            {
                PhotonNetwork.Instantiate(this.playerMaster.name, new Vector3(0f, -3.96f, 0f), Quaternion.identity, 0);
                Instantiate(MainMaster);
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount % 2);
            }
            else if(PhotonNetwork.CurrentRoom.PlayerCount % 2 == 0)
            {
                PhotonNetwork.Instantiate(this.playerClient.name, new Vector3(0f, 3.96f, 0f), Quaternion.Euler(180, 0, 0), 0);
                Instantiate(MainClient);
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount % 2);
            }
        }   
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
    }


    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            LoadArena();
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }

}