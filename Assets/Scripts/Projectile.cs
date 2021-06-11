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
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Item"))
            {
                Debug.Log("Item has been hit");
            }
            DestroyProjectile();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //void GlueTogether()

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject item = coll.collider.gameObject;
        glueList.glueableList.Add(item);
        Debug.Log("Item has been added");
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
