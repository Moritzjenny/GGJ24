using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public int contagionDistance = 10;

  [HideInInspector] public Chucky[] chuckies;
  [HideInInspector] public int happyChuckies;
  [HideInInspector] public int bumps;

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

    public void IncrementBumps()
  {
    bumps += 1;
    Bumps.instance.UpdateBumps();
  }
}
