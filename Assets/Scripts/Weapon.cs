using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public List<GameObject> glueableList;

    private void Start()
    {
        glueableList = new List<GameObject>();
    }

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void addToList(GameObject item)
    {
        if (!glueableList.Contains(item))
        {
            glueableList.Add(item);
        }
        if (glueableList.Count >= 2)
        {
            Debug.Log("2 objects are in list");
            glueableList[0].GetComponent<Gluable>().GlueTo(glueableList[1]);
            glueableList.Clear();
        }
    }
}
    
