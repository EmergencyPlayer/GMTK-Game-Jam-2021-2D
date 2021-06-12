using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gluable : MonoBehaviour
{
    public float glue_power = 100f;

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
            rb.AddForce((dir/dir.magnitude) * glue_power);
        }
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

        if (this.isParent)
        {
            line.SetActive(true);
            gl.MakeLine(g);
        }
      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Item" && collision.collider.gameObject.name != target.name)
            return;
        
        
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.angularVelocity = 0.0f;
        rb.gravityScale = 1f;
        isGlueing = false;
        gl.StopLine();
        line.SetActive(false);

        if (!isParent)
        {
            List<GameObject> temp = new List<GameObject>();
            Debug.Log(gameObject.name);

            foreach(Transform child in transform)
            {
                if(child.tag == "Item")
                    temp.Add(child.transform.gameObject);

                Debug.Log(child.name);
            }

            for(int i = 0; i < temp.Count; i++)
            {
                temp[i].transform.parent = target.transform;           
            }
            Destroy(gameObject);
        }
        
    }


}
