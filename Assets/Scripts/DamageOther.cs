using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOther : MonoBehaviour
{
    // Start is called before the first frame update
    public Manager manager;
    public AudioSource hit;
    void Start()
    {
        hit = this.GetComponent<AudioSource>();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        //Debug.Log("Hit");
        if (other.CompareTag("Projectile") && gameObject.CompareTag("Enemy"))
        {
            //t.Play();
            manager.playSound = true;
            Destroy(gameObject.transform.parent.gameObject);
            manager.score += 10;

        }
        else if(other.tag == "Enemy")
        {
            hit.Play();
            manager.score += 10;
            Destroy(other.transform.parent.gameObject);
            
        }
    }

}
