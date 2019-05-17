using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace Com.MyCompany.MyGame
{
    public class GM : MonoBehaviourPunCallbacks
    {

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}