using System.Collections;
using UnityEngine;

public class SizeAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform scaleObject;
    [SerializeField] private Vector3 scaleValue;
    
    [SerializeField][Min(0)] private float delayTime;
    [SerializeField][Min(0)] private float timeDuration;
    
    private Vector3 startScaleValue;
    private Coroutine corutine;

    public void Play()
    {
        if(corutine != null)scaleObject.localScale = startScaleValue;
        startScaleValue = scaleObject.localScale;
        StartRoutine();
    }

    private void StartRoutine()
    {
        EndRoutine();
        corutine = StartCoroutine(ScaleRoutine());
    }
 
    private void EndRoutine()
    {
        if(corutine == null) return;
        StopCoroutine(corutine);
    }

    private IEnumerator ScaleRoutine()
    {
        yield return new WaitForSeconds(delayTime);
        
        for (float time = timeDuration / 2; time > 0; time -= Time.deltaTime)
        {
            float value = 1 - time / timeDuration;
            scaleObject.localScale = Vector3.Lerp(startScaleValue,scaleValue, value);
            yield return null;
        }

        for (float time = timeDuration / 2; time > 0; time -= Time.deltaTime)
        {
            float value = 1 - time / timeDuration;
            scaleObject.localScale = Vector3.Lerp(scaleValue,startScaleValue, value);
            yield return null;
        }
    }

}
