using UnityEngine;
using UnityEngine.UI;

public class CountCoints : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
