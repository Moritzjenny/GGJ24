using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public int HappyChuckies { get; private set; }
  public int TotalChuckies { get; private set; }

  private Chucky[] chuckies;

  void Awake()
  {
    instance = this;
  }

  void Start()
  {
    chuckies = FindObjectsOfType<Chucky>();
    TotalChuckies = chuckies.Length;
    HappyChuckies = 0;
    Score.instance.SetScore(HappyChuckies, TotalChuckies);
  }

  public void IncreaseHappyChuckies()
  {
    HappyChuckies += 1;
    Score.instance.SetScore(HappyChuckies, TotalChuckies);
  }
}
