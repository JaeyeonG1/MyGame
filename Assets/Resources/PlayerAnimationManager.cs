using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class PlayerAnimationManager : MonoBehaviourPun
{
    #region MonoBehaviour Callbacks

    Animator animator;
    Rigidbody2D rigid2D;

    float jumpForce = 580.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float direction;

    // Use this for initialization
    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (!animator)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 해당 클라이언트의 인스턴스인지 확인
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        // 애니메이터 존재 확인
        if (!animator)
        {
            return;
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.UpArrow) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        direction = Input.GetAxisRaw("Horizontal");

        // 스피드 제한
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * direction * this.walkForce);
        }

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

    #endregion
}