using UnityEngine;
using UnityEngine.UI;

public class UpgradeLogic : MonoBehaviour
{
    [SerializeField] private Money money;
    [SerializeField] private LevelCounter level;
    [SerializeField] private Button buttonBay;


    private void Start()
    {
        buttonBay.onClick.AddListener(BuyLogicLevel);
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
    }
}
