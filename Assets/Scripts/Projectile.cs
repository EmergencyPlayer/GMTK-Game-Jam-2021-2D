using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    public Weapon glueList;

    private void Start()
    {
        Debug.Log(GameObject.Find("Weapon").name);
        glueList = GameObject.Find("Weapon").GetComponent<Weapon>();
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Item"))
            {
                //GameObject item = hitInfo.collider.gameObject;
                //glueList.glueableList.Add(item);
                Debug.Log("Item has been hit");
            }
            DestroyProjectile();
        }*/

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag == "Environment")
        {
            DestroyProjectile();
        }
        
        if (coll.collider.gameObject.tag != "Item")
        {
            return;
        }
        GameObject item = coll.collider.gameObject;
        glueList.addToList(item.transform.parent.transform.gameObject);
        Debug.Log("Item has been added");
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
