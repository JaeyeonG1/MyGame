using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkMove : MonoBehaviourPun, IPunObservable
{
    private Animator animator;
    Vector3 realPosition = Vector3.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(animator.GetBool("Jump"));
            stream.SendNext(animator.GetBool("Walk"));
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            animator.SetBool("Jump", (bool)stream.ReceiveNext());
            animator.SetBool("Walk", (bool)stream.ReceiveNext());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
        }
    }
}
