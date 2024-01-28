using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreenController : MonoBehaviour
{
    public static ScoreScreenController instance;

    public Text title;
    public Text highscore;
    public GameObject background;

    private void Awake()
    {
        instance = this;
    }

    public void ShowScore(int bumps)
    {
        title.text = "Score";
        highscore.text = $"{bumps} {Pluralize(bumps)}";
        background.SetActive(true);
    }

    public void ShowHighScore(int bumps)
    {
        title.text = "New highscore !!!";
        highscore.text = $"{bumps} {Pluralize(bumps)}";
        background.SetActive(true);
    }

    private string Pluralize(int bumps)
    {
        return bumps == 1 ? "bump" : "bumps";
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
