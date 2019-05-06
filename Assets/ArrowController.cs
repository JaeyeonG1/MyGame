using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject playerBottom;
    GameObject playerTop;
    
    void Start()
    {
        this.playerBottom = GameObject.Find("playerBottom"); // 추가
        this.playerTop = GameObject.Find("playerTop"); // 추가
    }
    
    void Update()
    {
        transform.Translate(0, -0.1f, 0); // 프레임마다 등속 낙하

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject); // 화면 밖으로 나오면 오브젝트 소멸
        }

        // 충돌 판정(추가)
        Vector2 arrow = transform.position;
        Vector2 pB = this.playerBottom.transform.position;
        Vector2 pT = this.playerTop.transform.position;
        Vector2 dirB = arrow - pB;
        Vector2 dirT = arrow - pT;
        float dB = dirB.magnitude;
        float dT = dirT.magnitude;
        float r1 = 0.25f; // 화살 반경
        float r2 = 0.25f; // 플레이어 반경

        if(dB < r1 + r2)
        {
            // 감독 스크립트에 플레이어와 화살이 충돌했다고 전달
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseBottomHp();
            // 충돌하면 화살 소멸
            Destroy(gameObject);
        }else if (dT < r1 + r2)
        {
            // 감독 스크립트에 플레이어와 화살이 충돌했다고 전달
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseTopHp();
            // 충돌하면 화살 소멸
            Destroy(gameObject);
        }
    }
}
