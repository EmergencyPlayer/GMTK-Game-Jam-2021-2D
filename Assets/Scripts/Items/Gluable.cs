using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluable : MonoBehaviour
{
    public int times_used;
    public int max_times;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
