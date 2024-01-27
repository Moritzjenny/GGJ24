using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreLoader : MonoBehaviour
{
    public GameObject background;
    public Text text;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var playerPrefsKey = $"level{transform.parent.GetSiblingIndex() + 1}";
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            background.SetActive(true);

            var highscore = PlayerPrefs.GetInt(playerPrefsKey);
            text.text = highscore.ToString();
        }
        else
        {
            background.SetActive(false);
        }

    }
}
