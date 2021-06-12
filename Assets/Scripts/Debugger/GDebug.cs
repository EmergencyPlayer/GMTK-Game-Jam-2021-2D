using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDebug : MonoBehaviour
{
    private bool begin;
    public List<GameObject> glueableList;
    // Start is called before the first frame update
    void Start()
    {
        begin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (glueableList.Count >= 2)
        {
            Debug.Log("Glued");
            glueableList[0].GetComponent<Gluable>().GlueTo(glueableList[1]);
        }
    }
}
