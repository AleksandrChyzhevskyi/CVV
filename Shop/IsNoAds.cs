using UnityEngine;

public class IsNoAds : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefs.DeleteKey("NoAds");
        //PlayerPrefs.DeleteKey("City");

        if (PlayerPrefs.GetString("NoAds") == "yes")
            Destroy(gameObject);
    }
}
