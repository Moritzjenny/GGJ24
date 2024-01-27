using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
  public static Score instance;

  private Text text;

  void Awake()
  {
    instance = this;
    text = GetComponent<Text>();
  }

  public void UpdateScore()
  {
    text.text = $"{GameManager.instance.happyChuckies}/{GameManager.instance.chuckies.Length} happy";
  }
}
