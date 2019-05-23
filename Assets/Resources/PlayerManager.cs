using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public const float maxHealth = 1f;
    public float health = 1f;
    public Image hpGauge;


    void FixedUpdate()
    {
        OnChangeHealth(health);

        if (!photonView.IsMine) return;
        if (health <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }
    }

    void OnChangeHealth(float currentHealth)
    {
        hpGauge.fillAmount = health;
    }    

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
                TakeDamage(0.1f);
                transform.position = new Vector3(0, -3, 0);
                Debug.Log(health);
            }

            if (transform.position.y > 5)
            {
                TakeDamage(0.1f);
                transform.position = new Vector3(0, 3, 0);
                Debug.Log(health);
            }
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
