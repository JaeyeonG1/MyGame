using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject hpGaugeBottom;
    GameObject hpGaugeTop;

    // Start is called before the first frame update
    void Start()
    {
        this.hpGaugeBottom = GameObject.Find("hpGaugeBottom");
        this.hpGaugeTop = GameObject.Find("hpGaugeTop");
    }

    public void DecreaseBottomHp()
    {
        this.hpGaugeBottom.GetComponent<Image>().fillAmount -= 0.1f;
    }

    public void DecreaseTopHp()
    {
        this.hpGaugeTop.GetComponent<Image>().fillAmount -= 0.1f;
    }
}
