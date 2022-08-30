using System.Collections.Generic;
using Game.Logic.Timer;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockController : MonoBehaviour
{
    [SerializeField] private List<Block> blocks;

    private void Start()
    {
        Timer timer = FindObjectOfType<Timer>();
        SetСomplexityBlocks(1, (int)timer.CurrentTime);
    }

    public void SetСomplexityBlocks(int min, int max)
    {
        blocks.ForEach(x => x.SetStartHealth(Random.Range(min, max)));
    }
}
