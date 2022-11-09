using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [NonSerialized] public static int countLoses;
    public AudioSource tyrnSignal;
    public Text nowScore, topScore, countsCount, nowScoreCF, topScoreCF;
    public GameObject canvasLose, canvasFreeze, horn, adsMenedjer;
    public bool isMainScene;
    public GameObject[] cars, maps;
    public float timeToSpawnFrom = 1f, timeToSpawnTo = 5f;
    private int cuntCars;
    private Coroutine bottomCars, leftCars, rightCars, upCars;
    private bool isLoseOnce;
    private static bool isAdd;    

    private void Start()
    {



        if (!isAdd && PlayerPrefs.GetString("NoAds") != "yes")
        {
            Instantiate(adsMenedjer, Vector3.zero, Quaternion.identity);
            isAdd = true;
        }

        if(PlayerPrefs.GetInt("NowMap") == 2)
        {
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[2]);
        }else if (PlayerPrefs.GetInt("NowMap") == 3)
        {
            Destroy(maps[0]);            
            Destroy(maps[1]);
            maps[2].SetActive(true);
        }
        else
        {
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
        }

        CarController.isLose = false;
        CarController.countCars = 0;

        if (isMainScene)
        {
            timeToSpawnFrom = 5f;
            timeToSpawnTo = 7f;
        }

        bottomCars = StartCoroutine(BottomCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        upCars = StartCoroutine(UpCars());

        StartCoroutine(CreateHorn());
    }

    private void Update()
    {
        if (nowScoreCF != null)
        {
            nowScoreCF.text = "<color=#FF0000>Score: </color>" + CarController.countCars.ToString();
        }
        if (PlayerPrefs.GetInt("Score") < CarController.countCars)
        {
            PlayerPrefs.SetInt("Score", CarController.countCars);
        }
        if (nowScoreCF != null)
        {
            topScoreCF.text = "<color=#FF0000>Top: </color>" + PlayerPrefs.GetInt("Score").ToString();
        }        

        if (CarController.isLose && !isLoseOnce)
        {
            countLoses++;
            StopCoroutine(bottomCars);
            StopCoroutine(leftCars);
            StopCoroutine(rightCars);
            StopCoroutine(upCars);
            nowScore.text = "<color=#FF0000>Score: </color>" + CarController.countCars.ToString();
            if(PlayerPrefs.GetInt("Score") < CarController.countCars)
            {
                PlayerPrefs.SetInt("Score", CarController.countCars);
            }
            
            topScore.text = "<color=#FF0000>Top: </color>" + PlayerPrefs.GetInt("Score").ToString();
            
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
            countsCount.text = PlayerPrefs.GetInt("Coins").ToString();
            
            canvasLose.SetActive(true);
            canvasFreeze.SetActive(false);
            isLoseOnce = true;
        }
    }
    IEnumerator BottomCars()
    {
        while (true)
        {
            SpwnCar(new Vector3 (-0.9f, -0.1319f, -27.1f), 180f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator LeftCars()
    {
        while (true)
        {
            SpwnCar(new Vector3(-80.5f, -0.1319084f, 2.7f), 270f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator RightCars()
    {
        while (true)
        {
            SpwnCar(new Vector3(27f, -0.1319084f, 10.9f), 90f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator UpCars()
    {
        while (true)
        {
            SpwnCar(new Vector3(-7.2f, -0.1319084f, 54.6f), 0f, true);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    void SpwnCar(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, rotationY, 0)) as GameObject;
        newObj.name = "Car - " + ++cuntCars;

        int random = isMainScene ? 1 : Random.Range(1, 4);
        if(isMainScene)
        {
            newObj.GetComponent<CarController>().speed = 10f;

        }
        switch (random)
        {
            case 1:
                newObj.GetComponent<CarController>().rihgtTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !tyrnSignal.isPlaying)
                {
                    tyrnSignal.Play();
                    Invoke("StopSound", 4f);
                }
                
                break;
            case 2:
                newObj.GetComponent<CarController>().leftTurn = true;
                if(PlayerPrefs.GetString("music") != "No" && !tyrnSignal.isPlaying)
                {
                    tyrnSignal.Play();
                    Invoke("StopSound", 4f);
                }
                if (isMoveFromUp) 
                    newObj.GetComponent<CarController>().moveFromUp = true;
                break;
        }
    }

    void StopSound()
    {
        tyrnSignal.Stop();
    }
    IEnumerator CreateHorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 9));
            if(PlayerPrefs.GetString("music") != "No")
                Instantiate(tyrnSignal, Vector3.zero, Quaternion.identity);
            
        }
    }
}
