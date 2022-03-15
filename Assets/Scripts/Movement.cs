using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 75;
    public Rigidbody2D rb;
    float x,y;
    private Vector2 MoveSpeed;
    public GameObject hitbox;
    private int count = 0;
    private bool hit = false;
    //private float cx, cy;

    //new stuff
    public Projectile projectile;
    public Transform offset;

    //end new stuff
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hitbox = this.gameObject.transform.GetChild(0).gameObject;
        count = 0;
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        // if(x > 1 || y > 1)
        // {
        //     x = 0;
        //     y = 0;
        // }
        Vector2 input = new Vector2(x,y);
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 hitLocation = getHitboxLocation(mouse, transform.transform);
        MoveSpeed = input * speed * Time.deltaTime;
        //fixes hold down key at start error
        if(MoveSpeed.x <= 1 && MoveSpeed.y <= 1 && MoveSpeed.x >= -1 && MoveSpeed.y >= -1)
        {
            rb.MovePosition(rb.position + MoveSpeed);
        }
        if (hit == false)
        {
            
            hitbox.transform.localPosition = new Vector3(hitLocation.x, hitLocation.y, 0);
        }

        //dontRender(hitbox,hitbox1,hitbox2,hitbox3);
        if(Input.GetMouseButtonDown(0))
        {
        //    GameObject hitbox0 = this.gameObject.transform.GetChild(getQuadrant(mouse,transform)).gameObject;
        //    dontCollide(hitbox,hitbox1,hitbox2,hitbox3);
        //    hitbox0.GetComponent<BoxCollider2D>().enabled = true;
        //    dontRender(hitbox,hitbox1,hitbox2,hitbox3);
            hitbox.GetComponent<SpriteRenderer>().enabled = true;
            hitbox.GetComponent<CircleCollider2D>().enabled = true;
            print("Hit");
            count = 1;
            hit = true;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            projectile.direction = hitLocation;
            //Vector3 dir = (this.transform.position - hitbox.transform.position).normalized;
            //Quaternion quaternion = Quaternion.Euler(dir.x, dir.y, dir.z);
            Instantiate(projectile,hitbox.transform.position, transform.rotation);
        }
        if(count % 100 == 0 && hit)
        {
            hitbox.GetComponent<CircleCollider2D>().enabled = false;
            hitbox.GetComponent<SpriteRenderer>().enabled = false;
            hit = false;
        }
        if(count >= 100000)
        {
            count = 0;
        }
        count++;
    }

    Vector2 getHitboxLocation(Vector2 mouse, Transform t)
    {
        float cx, cy;
        cx = mouse.x - t.position.x;
        cy = mouse.y - t.position.y;
        float magnitude = Mathf.Sqrt(cx*cx + cy*cy);
        return new Vector2(cx/magnitude, cy / magnitude);
    }
    void dontRender(GameObject one, GameObject two, GameObject three, GameObject four)
    {
        one.GetComponent<SpriteRenderer>().enabled = false;
        two.GetComponent<SpriteRenderer>().enabled = false;
        three.GetComponent<SpriteRenderer>().enabled = false;
        four.GetComponent<SpriteRenderer>().enabled = false;
    }
    void dontCollide(GameObject one, GameObject two, GameObject three, GameObject four)
    {
        one.GetComponent<BoxCollider2D>().enabled = false;
        two.GetComponent<BoxCollider2D>().enabled = false;
        three.GetComponent<BoxCollider2D>().enabled = false;
        four.GetComponent<BoxCollider2D>().enabled = false;
        
    }
}
