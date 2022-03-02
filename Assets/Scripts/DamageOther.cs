using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOther : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     GameObject other = collision.gameObject;
    //     if(other.CompareTag("Enemy") && this.gameObject.GetComponent<SpriteRenderer>().isVisible)
    //     {
    //         Destroy(other);
    //     }
    // }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.CompareTag("Enemy"))
        {
            Destroy(other);
        }
    }
}
