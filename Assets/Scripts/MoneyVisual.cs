using TMPro;
using UnityEngine;

public class MoneyVisual : MonoBehaviour
{
    [SerializeField] private Money money;
    [SerializeField] private TextMeshProUGUI label;

    private void Update()
    {
        label.text = $"{money.value}";
    }
}
