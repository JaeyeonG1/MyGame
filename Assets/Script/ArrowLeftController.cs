using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ArrowLeftController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.x > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBottom")
        {
            other.GetComponent<PlayerManager>().photonView.RPC("TakeDamage", RpcTarget.All, 0.1f);

            Destroy(gameObject);
        }
        else if (other.tag == "PlayerTop")
        {
            other.GetComponent<PlayerManager>().photonView.RPC("TakeDamage", RpcTarget.All, 0.1f);

            Destroy(gameObject);
        }
    }
}
