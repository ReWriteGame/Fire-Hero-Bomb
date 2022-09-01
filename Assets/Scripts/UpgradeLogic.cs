using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLogic : MonoBehaviour
{
    [SerializeField] private Money money;
    [SerializeField] private LevelCounter level;
    [SerializeField] private TextMeshProUGUI priceLabel;
    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private Button buttonBay;


    private void Start()
    {
        buttonBay.onClick.AddListener(BuyLogicLevel);
        ShowPrice();
    }

    private void OnDestroy()
    {
        buttonBay.onClick.RemoveListener(BuyLogicLevel);
    }

    private void BuyLogicLevel()
    {
        if (!level.IsLastLevel() && money.HaveMoney((int)level.GetNextLevelPrice()) )
        {
            money.TakeAway((int) level.GetNextLevelPrice());
            level.UpLevel();
        }

        ShowPrice();
    }

    private void ShowPrice()
    {
        priceLabel.text = $"{level.GetNextLevelPrice()}";
        levelLabel.text = $"{level.CurrentLevel}";
    }
}
