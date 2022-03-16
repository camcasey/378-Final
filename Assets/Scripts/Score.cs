
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Manager manager;
    public Text scoreText;
    public DamageOther score;
    void Update() {
        scoreText.text = manager.score.ToString();
    }
}
