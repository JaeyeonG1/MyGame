using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRightController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if (transform.position.x < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBottom")
        {
            GameObject director = GameObject.Find("GameUIDirector");
            director.GetComponent<GameUIDirector>().DecreaseHpBottom();

            Destroy(gameObject);
        }
        else if (other.tag == "PlayerTop")
        {
            GameObject director = GameObject.Find("GameUIDirector");
            director.GetComponent<GameUIDirector>().DecreaseHpTop();

            Destroy(gameObject);
        }
    }
}
