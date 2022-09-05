using System.Collections.Generic;
using Game.Logic.Timer;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockController : MonoBehaviour
{
    [SerializeField] private List<Block> blocks;
    [SerializeField] private List<Block> simplifiedBlocks;

    private void Start()
    {
        Timer timer = FindObjectOfType<Timer>();
        SetСomplexityBlocks((int)(timer.CurrentTime / 10) + 1, (int)timer.CurrentTime);
        SetSimplifiedBlocks((int)(timer.CurrentTime / 10) + 1, (int)(timer.CurrentTime / 10) * 2);
    }

    public void SetСomplexityBlocks(int min, int max)
    {
        //if (max > 20) max -= 7;
        blocks.ForEach(x => x.SetStartHealth(Random.Range(min, max)));
    }
    
    public void SetSimplifiedBlocks(int min, int max)
    {
        simplifiedBlocks.ForEach(x => x.SetStartHealth(Random.Range(min, max)));
    }
}
