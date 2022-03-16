using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int score = 0;
    public int damage = 1;
    public bool playSound = false;
    public AudioSource hit;
    void Start()
    {
        hit = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(playSound)
        {
            hit.Play();
            playSound = false;
        }
    }
}
