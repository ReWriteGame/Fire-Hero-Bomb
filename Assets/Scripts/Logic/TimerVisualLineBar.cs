using Game.Logic.Timer;
using UnityEngine;
using UnityEngine.UI;

public class TimerVisualLineBar : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Slider visualBar;
        
    private void Update()
    {
        UpdateValue();
    }
        
    public void UpdateValue()
    {
        visualBar.minValue = timer.MinTime;
        visualBar.maxValue = timer.MaxTime;
        visualBar.value = timer.CurrentTime;
    }
}
