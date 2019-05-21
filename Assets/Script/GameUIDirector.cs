using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameUIDirector : MonoBehaviourPunCallbacks, IPunObservable
{
    // IPunObservable 구현
    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {// We own this player: send the others our data
            stream.SendNext(hpGaugeBottom);
        }
        else
        {// Network player, receive data
            this.hpGaugeBottom = (GameObject)stream.ReceiveNext();
        }
    }

    #endregion
    public static GameObject LocalPlayerInstance;
    GameObject hpGaugeBottom;
    GameObject hpGaugeTop;

    void Awake()
    {
        if (photonView.IsMine)
        {
            GameUIDirector.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        this.hpGaugeBottom = GameObject.Find("hpGaugeBottom");
        this.hpGaugeTop = GameObject.Find("hpGaugeTop");
    }

    void Update()
    {
        if ((this.hpGaugeBottom.GetComponent<Image>().fillAmount <= 0) 
            || (this.hpGaugeTop.GetComponent<Image>().fillAmount <= 0))
        {
            GameManager.Instance.LeaveRoom();
        }
    }

    public void DecreaseHpBottom()
    {
        this.hpGaugeBottom.GetComponent<Image>().fillAmount -= 0.05f;
    }

    public void DecreaseHpTop()
    {
        this.hpGaugeTop.GetComponent<Image>().fillAmount -= 0.05f;
    }
}
