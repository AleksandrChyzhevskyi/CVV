using UnityEngine;
using UnityEngine.UI;

public class CheckMaps : MonoBehaviour
{
    public Image[] meps;
    public Sprite selected, notselected;
    private BuyMepCoins _mepCoins;

    private void Start()
    {
        //PlayerPrefs.DeleteKey("City");
        //PlayerPrefs.DeleteKey("Megapolis");
        PlayerPrefs.SetInt("Coins", 2500);

        WhichMapSelected();
        _mepCoins = GetComponent<BuyMepCoins>();
        if(PlayerPrefs.GetString("City") == "Open")
        {
            
            _mepCoins.coins1000.SetActive(false);
            _mepCoins.money0_99.SetActive(false);
            _mepCoins.city_but.SetActive(true);
        }
        if (PlayerPrefs.GetString("Megapolis") == "Open")
        {
            
            _mepCoins.coins5000.SetActive(false);
            _mepCoins.money1_99.SetActive(false);
            _mepCoins.mega_but.SetActive(true);
        }
    }
    public void WhichMapSelected()
    {
        switch (PlayerPrefs.GetInt("NowMap"))
        {
            case 2:
                meps[0].sprite = notselected;
                meps[1].sprite = selected;
                meps[2].sprite = notselected;
                break;

            case 3:
                meps[0].sprite = notselected;
                meps[1].sprite = notselected;
                meps[2].sprite = selected;
                break;

            default:
                meps[0].sprite = selected;
                meps[1].sprite = notselected;
                meps[2].sprite = notselected;
                break;
        }
    }
}
