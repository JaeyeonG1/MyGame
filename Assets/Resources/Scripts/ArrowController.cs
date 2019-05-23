using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ArrowController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (this.tag == "arrowLeft" && transform.position.x > 5.0f)
        {
            Destroy(gameObject);
        }
        else if (this.tag == "arrowRight" && transform.position.x < -5.0f)
        {
            Destroy(gameObject);
        }
        else if (this.tag == "arrowTop" && transform.position.y > 5.0f)
        {
            Destroy(gameObject);
        }
        else if (this.tag == "arrowBottom" && transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBottom")
        {
            other.GetComponent<PlayerManager>().photonView.RPC("TakeDamage", RpcTarget.All, 0.05f);

            Destroy(gameObject);
        }
        else if (other.tag == "PlayerTop")
        {
            other.GetComponent<PlayerManager>().photonView.RPC("TakeDamage", RpcTarget.All, 0.05f);

            Destroy(gameObject);
        }
    }
}
