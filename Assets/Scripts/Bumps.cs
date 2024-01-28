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
    text.text = $"{GameManager.instance.bumps} {Pluralize(GameManager.instance.bumps)}";
  }

  private string Pluralize(int bumps)
  {
    return bumps == 1 ? "bump" : "pumps";
  }
}
