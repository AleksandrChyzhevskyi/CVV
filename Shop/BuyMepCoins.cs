using UnityEngine;
using UnityEngine.UI;

public class BuyMepCoins : MonoBehaviour
{
    public GameObject coins1000, coins5000, money0_99, money1_99, city_but, mega_but, freeze_but;
    public Sprite btn, btnPrest, time1, time2;
    public Animation coinsText;
    public Text coinsCount;
    public AudioClip success, fail;
    
    public void BuyNewMep(int needCoins)
   {
        int coins = PlayerPrefs.GetInt("Coins");
            
        if(coins < needCoins)
        {
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }            

            coinsText.Play();
        }
        else
        {
            switch (needCoins)
            {
                case 1000:
                    PlayerPrefs.SetString("City", "Open");
                    PlayerPrefs.SetInt("NowMap", 2);
                    GetComponent<CheckMaps>().WhichMapSelected();
                    coins1000.SetActive(false);
                    money0_99.SetActive(false);
                    city_but.SetActive(true);
                    break;

                case 5000:
                    PlayerPrefs.SetString("Megapolis", "Open");
                    PlayerPrefs.SetInt("NowMap", 3);
                    GetComponent<CheckMaps>().WhichMapSelected();
                    coins5000.SetActive(false);
                    money1_99.SetActive(false);                    
                    mega_but.SetActive(true);
                    break;
            }

            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);

            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }
        }
            
    }    
}
