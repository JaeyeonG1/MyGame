using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class PlayerAnimationManager : MonoBehaviourPun
{
    protected Joystick joystick;
    protected Joybutton joybutton;

    protected bool jump;

    Animator animator;
    Rigidbody2D rigid2D;

    float jumpForce = 580.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float direction;

    void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 해당 클라이언트의 인스턴스인지 확인
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        // 점프
        if (!jump && joybutton.Pressed)
        {
            jump = true; 
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        if (jump && !joybutton.Pressed)
        {
            jump = false;
        }

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if (joystick.Horizontal > 0)
        {
            direction = 1;
        }
        else if (joystick.Horizontal < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }

        if (this.tag == "PlayerTop") direction *= -1;

        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * direction * this.walkForce);
        }

        // 플레이어 애니메이션 및 방향
        AnimationUpdate();
    }

    void AnimationUpdate()
    {
        if (direction == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            transform.localScale = new Vector3(direction, 1, 1);
            animator.SetBool("isWalking", true);
        }
    }
}