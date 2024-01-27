using UnityEngine;
using UnityEngine.UI;

public class HighscoreLoader : MonoBehaviour
{
    public string playerPrefsKey;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(playerPrefsKey))
        {
            gameObject.SetActive(false);
            return;
        }

        var highscore = PlayerPrefs.GetInt(playerPrefsKey);
        GetComponentInChildren<Text>().text = highscore.ToString();
    }
}
