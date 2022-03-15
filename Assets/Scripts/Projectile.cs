using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int x, y;
    public float speed = 4.5f;
    public Vector2 direction;
    public Vector3 tdirection;
    void Start()
    {
        tdirection = new Vector3(direction.x, direction.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(tdirection);

        transform.position += tdirection * speed * Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
