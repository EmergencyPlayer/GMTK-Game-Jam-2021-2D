using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gluable : MonoBehaviour
{
    public float glue_power = 0.1f;

    public int times_used;
    public int max_times;

    private Rigidbody2D rb;
    private bool isGlueing;
    private GameObject target;
    private bool isParent;

    [SerializeField] private GameObject line;
    private GlueLine gl;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        this.isGlueing = false;
        this.isParent = false;
        this.gl = line.GetComponent<GlueLine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isGlueing)
        {
            Vector2 dir = (target.transform.position - gameObject.transform.position);
            //rb.AddForce((dir/dir.magnitude) * glue_power);
        }
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
        if (g.tag != "Item")
            return;

        if (isGlueing)
            return;

        this.isParent = !flag;
        target = g;
        isGlueing = true;
        g.GetComponent<Gluable>().GlueTo(gameObject, !flag);
        
        if(this.isParent)
            gl.MakeLine(g);
      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Item")
        {
            return;
        }
        Debug.Log("touch");
        
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.angularVelocity = 0.0f;
        rb.gravityScale = 1f;
        isGlueing = false;

        if (!isParent)
        {
            foreach(Transform child in transform)
            {
                child.parent = target.transform;
            }
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
