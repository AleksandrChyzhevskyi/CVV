using UnityEngine;

public class ChooseMep : MonoBehaviour
{
   public void ChooseNewMep(int numberMap)
    {
        PlayerPrefs.SetInt("NowMap", numberMap);
        GetComponent<CheckMaps>().WhichMapSelected();
    }
}
