using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Text score;
    
    public void mainMenuButton(){
        Destroy(GameObject.Find("manager"));
        SceneManager.LoadScene("StartMenu");
    
    }


}
