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
    private int count = 0, count2 = 0;

    public HealthBarScript healthBar;
    public int curHealth = 100;
    public int maxHealth = 100;
    public bool iframe = false;
    public bool enemyPresent = false;
    public bool canMove = true;
    private void Start()
    {
        Debug.Log(this.gameObject.tag);
        hitbox = this.gameObject.transform.GetChild(0).gameObject;
        //CharacterController controller = this.gameObject.GetComponent<CharacterController>();
        //controller.detectCollisions = true;
        //curHealth = maxHealth;
        healthBar.setMaxHealth(curHealth);
    }


    // Update is called once per frame
    void Update()
    {
        
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
            projectile.direction = hitLocation;
            //play wizard charging up sound here
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
        if(count2 % 300 == 0 && canMove == false)
        {
            Instantiate(projectile, hitbox.transform.position, transform.rotation);
            canMove = true;
        }
        count++;
        count2++;
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
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //enemyPresent = true;
            TakeDamage(1);
        }
    }
}