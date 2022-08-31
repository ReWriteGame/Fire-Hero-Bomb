using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "LevelCounter", menuName = "ContainerSO/LevelCounter", order = 1)]
public class LevelCounter : ScriptableObject
{
  [SerializeField] [Min(0)] private int currentLevel;
  [SerializeField] private List<Level> levels;

  public float GetCurrentValue()
  {
    return levels[currentLevel].value;
  }
  
  public float GetNextLevelPrice()
  {
    return levels[currentLevel + 1].price;
  }

  public bool IsLastLevel()
  {
    return currentLevel >= levels.Count - 1;
  }

  public void UpLevel()
  {
    currentLevel++;
  }
  
  public void DownLevel()
  {
    currentLevel--;
  }
}

[Serializable]
public class Level
{
  public float value;
  public float price;
}
