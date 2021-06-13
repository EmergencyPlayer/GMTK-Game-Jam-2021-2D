using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Player")
            return;

        FindObjectOfType<AudioManager>().Play("Reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
