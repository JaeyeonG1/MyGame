using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowTopPrefab;
    public GameObject arrowBottomPrefab;
    public GameObject arrowLeftPrefab;
    public GameObject arrowRightPrefab;

    private bool isin = true;

    float span = 1.0f;
    float delta = 0;

    void Awake()
    {
        arrowBottomPrefab = Resources.Load<GameObject>("arrowBottomPrefab");
        arrowTopPrefab = Resources.Load<GameObject>("arrowTopPrefab");
        arrowLeftPrefab = Resources.Load<GameObject>("arrowLeftPrefab");
        arrowRightPrefab = Resources.Load<GameObject>("arrowRightPrefab");
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (isin)
        {
            isin = false;
            StartCoroutine(SpawnCoroutine());
        }        
    }

    IEnumerator SpawnCoroutine()
    {
        float bottomRange = Random.Range(-5f, 5f);
        float topRange = Random.Range(-5f, 5f);
        int leftRange = Random.Range(-4, 4);
        int rightRange = Random.Range(-4, 4);

        Vector3 bottom = new Vector3(bottomRange, -1, 0);
        Vector3 top = new Vector3(topRange, 1, 0);
        Vector3 right = new Vector3(5, rightRange, 0);
        Vector3 left = new Vector3(-5, leftRange, 0);

        PhotonNetwork.Instantiate(this.arrowBottomPrefab.name, bottom, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(this.arrowTopPrefab.name, top, Quaternion.Euler(0, 0, 180), 0);
        PhotonNetwork.Instantiate(this.arrowRightPrefab.name, right, Quaternion.Euler(0, 0, 270), 0);
        PhotonNetwork.Instantiate(this.arrowLeftPrefab.name, left, Quaternion.Euler(0, 0, 90), 0);

        yield return new WaitForSeconds(span);

        if (this.span > 0.4f)
        {
            this.span *= 0.99f;
            Debug.Log(span);
        }

        isin = true;
    }
}
