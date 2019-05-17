using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowTopPrefab;
    public GameObject arrowBottomPrefab;
    public GameObject arrowLeftPrefab;
    public GameObject arrowRightPrefab;
    float span = 1.0f;
    float delta = 0;

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            // 하단 화면 게임오브젝트 및 생성 범위 지정
            GameObject goBottom = Instantiate(arrowBottomPrefab) as GameObject;
            GameObject goBottomLeft = Instantiate(arrowLeftPrefab) as GameObject;
            GameObject goBottomRight = Instantiate(arrowRightPrefab) as GameObject;

            float bottomPx = Random.Range(-5.0f, 5.0f);
            int leftBottomPx = Random.Range(-4, 0);
            int rightBottomPx = Random.Range(-4, 0);

            // 상단 화면 게임오브젝트 및 생성 범위 지정
            GameObject goTop = Instantiate(arrowTopPrefab) as GameObject;
            GameObject goTopLeft = Instantiate(arrowLeftPrefab) as GameObject;
            GameObject goTopRight = Instantiate(arrowRightPrefab) as GameObject;
            
            float topPx = Random.Range(-5.0f, 5.0f);
            int leftTopPx = Random.Range(4, 0);
            int rightTopPx = Random.Range(4, 0);
            
            // 하단 화면 화살 생성
            goBottom.transform.position = new Vector3(bottomPx, -1, 0);
            goBottomLeft.transform.position = new Vector3(-5, leftBottomPx, 0);
            goBottomRight.transform.position = new Vector3(5, rightBottomPx, 0);

            // 상단 화면 화살 생성
            goTop.transform.position = new Vector3(topPx, 1, 0);
            goTopLeft.transform.position = new Vector3(-5, leftTopPx, 0);
            goTopRight.transform.position = new Vector3(5, rightTopPx, 0);

            // 화살 생성 속도 점차 증가
            if (this.span > 0.4f)
            {
                this.span *= 0.99f;
                Debug.Log(span);
            }
        }
    }
}
