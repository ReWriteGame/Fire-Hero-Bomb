using UnityEngine;


[CreateAssetMenu(fileName = "LevelCounter", menuName = "ContainerSO/Money", order = 1)]
public class Money : ScriptableObject
{
  public int value;

  public bool HaveMoney(int money)
  {
    return value - money >= 0;
  }

  public void TakeAway(int money)
  {
    value -= money;
  }
  
  public void Add(int money)
  {
    value += money;
  }
}
