using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gluable : MonoBehaviour
{
    public float glue_power = 2.0f;

    public int times_used;
    public int max_times;

    private Rigidbody2D rb;
    private bool isGlueing;
    private GameObject target;
    private bool isParent;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        this.isGlueing = false;
        this.isParent = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isGlueing)
            rb.AddForce((target.transform.position - gameObject.transform.position) * glue_power);
    }

    public void Init()
    {
        times_used = 0;
        max_times = 1;
    }

    public void Init(int times, int max)
    {
        times_used = times;
        max_times = max;
    }

    public void GlueTo(GameObject g, bool flag = false)
    {
        if (g.tag != "Gluable")
            return;

        try
        {
            if (isGlueing)
                return;

            this.isParent = !flag;
            target = g;
            g.GetComponent<Gluable>().GlueTo(gameObject, !flag);
            isGlueing = true;
        }
        catch (Exception e) { return; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("touch");
        
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.angularVelocity = 0.0f;
        isGlueing = false;

        if (!isParent)
        {
            gameObject.transform.GetChild(0).parent = target.transform;
            Destroy(gameObject);
        }
        
    }

    public void IncreaseTimesUsed(int t = 1)
    {
        times_used += t;
        CheckDestroy();
    }

    private void CheckDestroy()
    {
        if (times_used >= max_times)
            Destroy(gameObject);
    }
}
