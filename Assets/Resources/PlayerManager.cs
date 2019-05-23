using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public const float maxHealth = 100f;
    public float health = 100f;
    public Image hpGauge;
    public int myCount = 0;


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
        hpGauge.fillAmount = health / maxHealth;
    }

    [PunRPC]
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
        }
    }

    [PunRPC]
    public void AddHealth(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    /*
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
    */
}
