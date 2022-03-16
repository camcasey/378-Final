using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public GameObject hitbox;
    public GameObject remainingEnemy;
    public Projectile projectile;
    private int count = 0, count2 = 0, count3 = 0;
    public Manager manager;
    public int curHealth = 100;

    public HealthBarScript healthBar;
    public bool hurt = false;
    public bool enemyPresent = false;
    public bool canMove = true;
    public AudioSource charge;
    public AudioSource shoot;
    public AudioSource ouch;
    private void Start()
    {
        charge = this.GetComponent<AudioSource>();
        //shoot = this.GetComponent<AudioSource>();

        
        hitbox = this.gameObject.transform.GetChild(0).gameObject;
        curHealth = manager.maxHealth;
        Debug.Log(curHealth);
        //CharacterController controller = this.gameObject.GetComponent<CharacterController>();
        //controller.detectCollisions = true;
        //curHealth = maxHealth;
        healthBar.setMaxHealth(curHealth);
    }


    // Update is called once per frame
    void Update()
    {
        if(hurt)
        {
            spriteRenderer.color = Color.red;
        }
        if(hurt && count3 % 100 == 0)
        {
            spriteRenderer.color = Color.white;
            hurt = false;
        }
        /*if(enemyPresent && !iframe)
        {
            other.gameObject.SendMessage("ApplyDamage", 5);
            TakeDamage(1);
            iframe = true;
            count2 = 0;
        }
        if(iframe && count2 % 100 == 0)
        {
            iframe = false;
        }*/
        
        remainingEnemy = GameObject.FindWithTag("Enemy");
        //Debug.Log("remaining enemy = null?: " + remainingEnemy);
        if(remainingEnemy == null)
        {
            manager.maxHealth += 10;
            manager.damage += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 hitLocation = getHitboxLocation(mouse, transform.transform);
        hitbox.transform.localPosition = new Vector3(hitLocation.x, hitLocation.y+.6f, 0);

        if (hitbox.GetComponent<CircleCollider2D>().enabled == true && count % 10 == 0)
        {
            hitbox.GetComponent<CircleCollider2D>().enabled = false;
            hitbox.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //    GameObject hitbox0 = this.gameObject.transform.GetChild(getQuadrant(mouse,transform)).gameObject;
            //    dontCollide(hitbox,hitbox1,hitbox2,hitbox3);
            //    hitbox0.GetComponent<BoxCollider2D>().enabled = true;
            //    dontRender(hitbox,hitbox1,hitbox2,hitbox3);
            hitbox.GetComponent<SpriteRenderer>().enabled = true;
            hitbox.GetComponent<CircleCollider2D>().enabled = true;
            //print("Hit");
            count = 0;
        }
        else if (Input.GetMouseButtonDown(1) && canMove)
        {

            //play wizard charging up sound here
            charge.Play();
            canMove = false;
            count2 = 1;
        }

        if (canMove)
        {
            if (Input.GetKey(KeyCode.D))
            {

                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                spriteRenderer.sprite = sprites[3];

            }
            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                spriteRenderer.sprite = sprites[2];
            }

            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveSpeed);
                spriteRenderer.sprite = sprites[0];
            }
            if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -moveSpeed);
                spriteRenderer.sprite = sprites[1];
            }
        }
        if(curHealth <= 0){

            SceneManager.LoadScene("LoseMenu");
        }
        if(count > 10000)
        {
            count = 0;
        }
        if (count2 > 10000)
        {
            count2 = 1;
        }
        if(count3 > 10000)
        {
            count3 = 1;
        }
        if(count2 % 300 == 0 && canMove == false)
        {
            shoot.Play();
            projectile.direction = hitLocation;
            Instantiate(projectile, hitbox.transform.position, transform.rotation);
            canMove = true;
        }
        count++;
        count2++;
        count3++;
        enemyPresent = false;
    }
    Vector2 getHitboxLocation(Vector2 mouse, Transform t)
    {
        float cx, cy;
        cx = mouse.x - t.position.x;
        cy = mouse.y - t.position.y;
        float magnitude = Mathf.Sqrt(cx * cx + cy * cy);
        return new Vector2(cx / magnitude, cy / magnitude);
    }

    void TakeDamage(int damage){
        curHealth -= damage;
        healthBar.setHealth(curHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ouch.Play();
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //enemyPresent = true;
            TakeDamage(manager.damage);
            hurt = true;
            count3 = 1;
        }
    }

}