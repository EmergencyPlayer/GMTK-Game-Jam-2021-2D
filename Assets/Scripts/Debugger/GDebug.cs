using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDebug : MonoBehaviour
{
    private bool begin;
    public GameObject[] list;
    public List<GameObject> l2;
    // Start is called before the first frame update
    void Start()
    {
        begin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("p"))
            begin = true;

        if (Input.GetKeyUp("p")) {
            l2.RemoveAt(1);
            begin = false;
        }
            

        if (begin)
        {
            l2[0].GetComponent<Gluable>().GlueTo(l2[1]);
            
            
            //Debug.Log("try");
        }
    }
}
