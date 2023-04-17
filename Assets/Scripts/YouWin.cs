using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWin : MonoBehaviour
{
    public Text gameText;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameWin();
        }
    }

    void GameWin()
    {
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
