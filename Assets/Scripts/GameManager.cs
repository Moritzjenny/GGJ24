using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public int contagionDistance = 0;

  [HideInInspector] public Chucky[] chuckies;
  [HideInInspector] public int happyChuckies;

  void Awake()
  {
    instance = this;
  }

  void Start()
  {
    InitChuckies();
  }

  public void InitChuckies()
  {
    chuckies = FindObjectsOfType<Chucky>();
    Score.instance.UpdateScore();
  }

  public void IncrementHappyChuckies()
  {
    happyChuckies += 1;
    Score.instance.UpdateScore();
  }
}
