using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BoardManager boardScript;
    private int level =1;

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }
    void InitGame(){
        boardScript.SetupScene(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}