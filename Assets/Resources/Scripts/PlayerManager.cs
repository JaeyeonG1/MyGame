using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public const float maxHealth = 1f;
    public float health = 1f;
    public Image hpGauge;
    
    void OnChangeHealth(float currentHealth)
    {
        hpGauge.fillAmount = health;
    }

    void Update()
    {
        OnChangeHealth(health);

        if (health <= 0f)
        {
            if (photonView.IsMine)
            {
                SceneManager.LoadScene(3);
                GameManager.Instance.LeaveRoom();
            }
            else if (!photonView.IsMine)
            {
                SceneManager.LoadScene(2);
                GameManager.Instance.LeaveRoom();
            }
        }

        if (this.tag == "PlayerBottom" && transform.position.y < -5)
        {
            this.GetComponent<PlayerManager>().photonView.RPC("TakeDamage", RpcTarget.All, 0.1f);
            this.transform.position = new Vector3(0, -3, 0);
        }
        if (this.tag == "PlayerTop" && transform.position.y > 5)
        {
            this.GetComponent<PlayerManager>().photonView.RPC("TakeDamage", RpcTarget.All, 0.1f);
            this.transform.position = new Vector3(0, 3, 0);
        }
    }

    [PunRPC]
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
        }
    }

    [PunRPC]
    public void AddHealth(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
