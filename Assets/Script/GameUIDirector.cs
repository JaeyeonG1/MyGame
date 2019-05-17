using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIDirector : MonoBehaviour
{
    GameObject hpGaugeBottom;
    GameObject hpGaugeTop;

    void Start()
    {
        this.hpGaugeBottom = GameObject.Find("hpGaugeBottom");
        this.hpGaugeTop = GameObject.Find("hpGaugeTop");
    }

    void Update()
    {
        if ((this.hpGaugeBottom.GetComponent<Image>().fillAmount <= 0) 
            || (this.hpGaugeTop.GetComponent<Image>().fillAmount <= 0))
        {
            SceneManager.LoadScene("EndScene");
        }
            
    }

    public void DecreaseHpBottom()
    {
        this.hpGaugeBottom.GetComponent<Image>().fillAmount -= 0.05f;
    }

    public void DecreaseHpTop()
    {
        this.hpGaugeTop.GetComponent<Image>().fillAmount -= 0.05f;
    }
}
