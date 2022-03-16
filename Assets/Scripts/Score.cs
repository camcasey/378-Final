
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public DamageOther score;
    void Update() {
        scoreText.text = score.getScore().ToString();
    }
}
