using UnityEngine;
using UnityEngine.UI;
using System;

public class FreezeButton : MonoBehaviour
{
    [NonSerialized]
    public static int freezeScore = 3;
    public Text coinsCount;
    public Animation coinsText;
    public AudioClip success, fail;
    public Sprite btn, btnPrest, time1, time2;
    private Image image;


    private void Start()
    {
        FreezeScore();
    }

    public void FreezeBtn()
     {       
        foreach(var i in GameObject.FindObjectsOfType<CarController>())
        {
            i.isFreeze = true;
        }        
        if (freezeScore >= 0 && freezeScore <= 3)
            freezeScore++;
        else
            freezeScore = 4;
        Debug.Log(freezeScore);
        FreezeScore();

    }      
    private void PleyButtonSound()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
    }

    public void BuyFreeze(int needCoins)
    {
        int coins = PlayerPrefs.GetInt("Coins");

        if (coins < needCoins)
        {
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }

            coinsText.Play();
        }
        else if (coins > needCoins && freezeScore >= 1)
        {
            Debug.Log(freezeScore);
            
            switch (freezeScore)
            {
                case 4:
                  freezeScore--;
                    transform.GetChild(0).GetComponent<Image>().sprite = time1;
                    break;
                case 3:
                    freezeScore--;
                    transform.GetChild(0).GetComponent<Image>().sprite = btnPrest;                   
                    break;
                case 2:
                    freezeScore--;
                    transform.GetChild(0).GetComponent<Image>().sprite = btn;                    
                    break;
                case 1:
                    freezeScore--;
                    transform.GetChild(0).GetComponent<Image>().sprite = time2;
                    break;
                case 0:
                    transform.GetChild(0).GetComponent<Image>().sprite = time2;
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

    public void FreezeScore()
    {        
        switch (freezeScore)
        {
            case 0:
                transform.GetChild(0).GetComponent<Image>().sprite = time2;
                break;

            case 1:
                transform.GetChild(0).GetComponent<Image>().sprite = btn;
                PleyButtonSound();
                PlayerPrefs.SetInt("freeze", 3);
                break;

            case 2:
                transform.GetChild(0).GetComponent<Image>().sprite = btnPrest;
                PleyButtonSound();
                PlayerPrefs.SetInt("freeze", 2);
                break;

            case 3:
                transform.GetChild(0).GetComponent<Image>().sprite = time1;
                PleyButtonSound();
                PlayerPrefs.SetInt("freeze", 1);
                break;

            default:
                transform.GetChild(0).GetComponent<Image>().sprite = time1;                
                PlayerPrefs.SetInt("freeze", 0);
                break;
        }
    }    
}
