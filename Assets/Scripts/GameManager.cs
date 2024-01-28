using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public int level;
  public int contagionDistance = 0;

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

    if (happyChuckies == chuckies.Length)
    {
      Invoke(nameof(FinishLevel), 1);
    }
  }

  public void IncrementBumps()
  {
    bumps += 1;
    Bumps.instance.UpdateBumps();
  }

  private void FinishLevel()
  {
    var key = $"level{level}";
    print(PlayerPrefs.HasKey(key));
    print(PlayerPrefs.GetInt(key));
    if (!PlayerPrefs.HasKey(key) || bumps < PlayerPrefs.GetInt(key))
    {
      PlayerPrefs.SetInt(key, bumps);
      ScoreScreenController.instance.ShowHighScore(bumps);
    }
    else
    {
      ScoreScreenController.instance.ShowScore(bumps);
    }
  }
}
