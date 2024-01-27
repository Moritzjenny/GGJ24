using UnityEngine;
using UnityEngine.UI;

public class Bumps : MonoBehaviour
{
  public static Bumps instance;

  private Text text;

  void Awake()
  {
    instance = this;
    text = GetComponent<Text>();
  }

  public void UpdateBumps()
  {
    text.text = $"{GameManager.instance.bumps} Bumps";
  }
}
