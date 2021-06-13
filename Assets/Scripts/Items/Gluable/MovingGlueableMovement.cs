using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGlueableMovement : MonoBehaviour
{

    public GameObject v1;
    public GameObject v2;

    private Vector3 v1Pos;
    private Vector3 v2Pos;

    private bool isMoving = true;
    private Vector3 pos;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        v1Pos = v1.transform.position;
        v2Pos = v2.transform.position;
        gameObject.transform.position = v1Pos;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(v1Pos, v2Pos, Mathf.Abs(0.5f + 0.5f * Mathf.Cos(time)));
        //Debug.Log(Mathf.Abs(Mathf.Sin(time)));
        time += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Item" && collision.collider.gameObject.tag != "Projectile")
            return;
        Destroy(this);
    }
}