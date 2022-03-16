using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemy : MonoBehaviour
{
    CharacterController _controller;
    Transform target;
    GameObject Player;
    [SerializeField] float _moveSpeed = 5.0f;
    public SpriteRenderer renderer;
    public Sprite[] sprites;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        target = Player.transform;
        _controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        if(direction.x > 0)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
        direction = direction.normalized;
        Vector3 velocity = direction * _moveSpeed;
        if(velocity.x > 0)
        {
            //renderer.sprite = sprites[1];
        }
        if(velocity.x < 0)
        {
            //renderer.sprite = sprites[0];
        }
        _controller.Move(velocity * Time.deltaTime);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("hit");
    //}
    
}