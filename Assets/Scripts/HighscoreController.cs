using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    public static HighscoreController instance;

    public Text highscore;
    public GameObject background;

    private void Awake()
    {
        instance = this;
    }

    public void Show(int bumps)
    {
        highscore.text = $"{bumps} bumps";
        background.SetActive(true);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
