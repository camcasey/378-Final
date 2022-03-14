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

    public HealthBarScript healthBar;
    public int curHealth;
    public int maxHealth = 100;
    private void Start()
    {
        Debug.Log(this.gameObject.tag);
        hitbox = this.gameObject.transform.GetChild(0).gameObject;
        CharacterController controller = this.gameObject.GetComponent<CharacterController>();
        controller.detectCollisions = true;
        curHealth = maxHealth;
        healthBar.setMaxHealth(curHealth);
    }


    // Update is called once per frame
    void Update()
    {
        remainingEnemy = GameObject.FindWithTag("Enemy");
        Debug.Log("remaining enemy = null?: " + remainingEnemy);
        if(remainingEnemy == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 hitLocation = getHitboxLocation(mouse, transform.transform);
        hitbox.transform.localPosition = new Vector3(hitLocation.x, hitLocation.y, 0);
        if(hitbox.GetComponent<SpriteRenderer>().enabled == true){
            hitbox.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (hitbox.GetComponent<CircleCollider2D>().enabled == true)
        {
            hitbox.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //    GameObject hitbox0 = this.gameObject.transform.GetChild(getQuadrant(mouse,transform)).gameObject;
            //    dontCollide(hitbox,hitbox1,hitbox2,hitbox3);
            //    hitbox0.GetComponent<BoxCollider2D>().enabled = true;
            //    dontRender(hitbox,hitbox1,hitbox2,hitbox3);
            hitbox.GetComponent<SpriteRenderer>().enabled = true;
            hitbox.GetComponent<CircleCollider2D>().enabled = true;
            print("Hit");
        }


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
    public void enemyCollision(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            TakeDamage(5);
        }
    }
}