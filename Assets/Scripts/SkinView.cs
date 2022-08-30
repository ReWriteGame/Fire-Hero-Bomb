using System.Collections.Generic;
using UnityEngine;

public class SkinView : MonoBehaviour
{
    [SerializeField] private List<GameObject> skins;
        
    public void SetRandomSkin()
    {
        SetSkin(Random.Range(0, skins.Count));
    }
        
    public void SetSkin(int index)
    {
        SwitchOffAllSkins();
        skins[index].SetActive(true);
    }

    public void SwitchOffAllSkins()
    {
        skins.ForEach(x => x.SetActive(false));
    }
}