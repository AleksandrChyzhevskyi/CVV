using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasButtons : MonoBehaviour
{
    public Sprite btn, btnPrest,musicOn, musicOff, freeze1, freeze2, freeze3;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        if (gameObject.name == "Music Button")
        {
            if (PlayerPrefs.GetString("music") == "No") // Решить вопрос с устоновкай Нет.
            {
                transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
            }
        }        
    }
    public void MusicButton()
    {
        if(PlayerPrefs.GetString("music") == "No")
        {
            PlayerPrefs.SetString("music", "Yes");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            PlayerPrefs.SetString("music", "No");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }
        PleyButtonSound();
    }

    public void Freeze()
    {
        if (PlayerPrefs.GetInt("freeze") == 3)
        {
            PlayerPrefs.SetInt("freeze", 1);
            transform.GetChild(0).GetComponent<Image>().sprite = freeze1;
        }
        else if(PlayerPrefs.GetInt("freeze") == 2)
        {
            PlayerPrefs.SetInt("freeze", 2);
            transform.GetChild(0).GetComponent<Image>().sprite = freeze2;
        }
        else
        {
            PlayerPrefs.SetInt("freeze", 1);
            transform.GetChild(0).GetComponent<Image>().sprite = freeze3;
        }
        PleyButtonSound();       
    }
    public void ShopScene()
    {
        PleyButtonSound();
        StartCoroutine(LoadScene("Shop"));        
    }
    public void ExitShopScene()
    {
        PleyButtonSound();
        StartCoroutine(LoadScene("Main"));        
    }

    public void PlayGame()
    {
        PleyButtonSound();

        if (PlayerPrefs.GetString("First Game") == "No")
        {
            StartCoroutine(LoadScene("Game"));
        }
        else
        {
            StartCoroutine(LoadScene("Study"));
        }
    }
    public void RestartGame()
    {
        PleyButtonSound();
        StartCoroutine(LoadScene("Game"));        
    }    
    public void SetPressedButten()
    {
        PleyButtonSound();
        image.sprite = btnPrest;        
    }
    public void SetDefaultButten()
    {
        PleyButtonSound();
        image.sprite = btn;        
    }

    IEnumerator LoadScene (string name)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

    private void PleyButtonSound()
    {
        if (PlayerPrefs.GetString("music") != "No")
            GetComponent<AudioSource>().Play();
    }
}
