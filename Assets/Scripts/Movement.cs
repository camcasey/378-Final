using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.01f;
    public Rigidbody2D rb;
    float x,y;
    private Vector2 MoveSpeed;
    public GameObject hitbox;
    private int count = 0;
    private bool hit = false;
    //private float cx, cy;

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
        MoveSpeed = input * speed * Time.deltaTime;
        //fixes hold down key at start error
        if(MoveSpeed.x <= 1 && MoveSpeed.y <= 1 && MoveSpeed.x >= -1 && MoveSpeed.y >= -1)
        {
            rb.MovePosition(rb.position + MoveSpeed);
        }
        if (hit == false)
        {
            Vector2 hitLocation = getHitboxLocation(mouse, transform.transform);
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
    int getQuadrant(Vector2 mouse, Transform t)
    {
        if(mouse.x > t.position.x && mouse.y > t.position.y)
            return 0;
        if(mouse.x > t.position.x && mouse.y < t.position.y)
            return 1;
        if(mouse.x < t.position.x && mouse.y < t.position.y)
            return 2;
        if(mouse.x < t.position.x && mouse.y > t.position.y)
            return 3;
        return -1;
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
