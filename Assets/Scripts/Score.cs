
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Manager manager;
    public Text scoreText;
    public DamageOther score;
    void Start() {
        manager = GameObject.Find("manager").GetComponent<Manager>();
    }
    void Update() {
        scoreText.text = "Score: " + manager.score.ToString();
    }
}
