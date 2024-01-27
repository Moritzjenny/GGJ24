using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
  public static Score instance;

  private Text text;

  void Awake()
  {
    instance = this;
  }

  void Start()
  {
    text = GetComponent<Text>();
  }

  public void SetScore(int happyChuckies, int totalChuckies)
  {
    text.text = $"{happyChuckies}/{totalChuckies} happy";
  }
}
