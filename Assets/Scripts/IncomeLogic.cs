using Game.Logic.Score;
using TMPro;
using UnityEngine;

public class IncomeLogic : MonoBehaviour
{
  [SerializeField] private Money money;
  [SerializeField] private ScoreCounter score;
  [SerializeField] private LevelCounter income;
  [SerializeField] private TextMeshProUGUI labelIncome;
  [SerializeField] private TextMeshProUGUI labelIncome2;

  public void EndGame()
  {
    int addValue = (int)(score.Score * income.GetCurrentValue() / 100);
    money.Add(addValue);
    labelIncome.text = $"{addValue}";
    labelIncome2.text = $"{addValue}";
  }
}
