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
      StartCoroutine(InvokeAfterSeconds(1, FinishLevel));
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
    if (!PlayerPrefs.HasKey(key) || bumps < PlayerPrefs.GetInt(key))
    {
      PlayerPrefs.SetInt(key, bumps);
      HighscoreController.instance.Show(bumps);
    }
    else
    {
      SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
  }

  IEnumerator InvokeAfterSeconds(float seconds, Action callback)
  {
    yield return new WaitForSeconds(seconds);
    callback.Invoke();
  }
}
