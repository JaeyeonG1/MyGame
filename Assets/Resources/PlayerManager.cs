using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public float Health = 1f;
    public int myCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount % 2 == 1)
        {
            myCount = 1;
        }
        Debug.Log(myCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            if (transform.position.y < -5)
            {
                Health -= 0.1f;
                transform.position = new Vector3(0, -3, 0);
                Debug.Log(Health);
            }

            if (transform.position.y > 5)
            {
                Health -= 0.1f;
                transform.position = new Vector3(0, 3, 0);
                Debug.Log(Health);
            }
        }
        

        if (Health <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!other.name.Contains("Arrow"))
        {
            return;
        }
        Health -= 0.1f;
        Debug.Log(Health);
    }
}
