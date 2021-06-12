using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public WinMenu win;
    public GameObject winG;

    void Start()
    {
        win = winG.GetComponent<WinMenu>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            win.winGame = true;
            FindObjectOfType<AudioManager>().Play("Win");
        }
    }
}
