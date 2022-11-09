using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]

public class CarController : MonoBehaviour
{
    public AudioClip cresh;
    public AudioClip[] acselereit;
    public GameObject turnLeft, turnRight, explosion, exhaust;
    public LayerMask carsLayer;
    public bool rihgtTurn, leftTurn, moveFromUp;
    public float speed = 15f, force = 50f;
    private Rigidbody carRb;
    private float originRoteitionY, rotateMultRight = 6f, rotateMultLeft = 4.5f;
    private Camera mainCam;
    private bool isMovingFast, carCrashed;
    [NonSerialized] public bool carPast, isFreeze;
    [NonSerialized] public static bool isLose;
    [NonSerialized] public static int countCars;

    private void Start()
    {
        originRoteitionY = transform.eulerAngles.y;
        carRb = GetComponent<Rigidbody>();
        mainCam = Camera.main;

        if (rihgtTurn)
            StartCoroutine(TurnSignals(turnRight));
        else if (leftTurn)
            StartCoroutine(TurnSignals(turnLeft));
    }

    IEnumerator TurnSignals(GameObject turnSignal)
    {
        while (!carPast)
        {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        carRb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }
    private void Update()
    {
        //#if UNITY_EDITOR
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        /*#else
            if (Input.touchCount == 0) return; - Если нужно упровление на планшете или телефоне (Андроид).

            Ray ray = mainCam.ScreenPointToRay(Input.GetTouch(0).position); - Если нужно упровление на планшете или телефоне (Андроид).
         #endif*/
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, carsLayer))
        {
            string carName = hit.transform.gameObject.name;
            //#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !isMovingFast && gameObject.name == carName)
            {
                /*#else
                   if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName) { - Если нужно упровление на планшете или телефоне (Андроид).
                 #endif*/
                GameObject vfx = Instantiate(exhaust, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
                Destroy(vfx, 2f);

                speed *= 2f;
                isMovingFast = true;

                if (PlayerPrefs.GetString("music") != "No")
                {
                    GetComponent<AudioSource>().clip = acselereit[Random.Range(0, acselereit.Length)];
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car") && !carCrashed)
        {
            carCrashed = true;
            isLose = true;
            speed = 0f;
            other.gameObject.GetComponent<CarController>().speed = 0f;

            GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfx, 5f);
            
            if (isMovingFast)
                force *= 1.5f;

            carRb.AddRelativeForce(Vector3.back * force);
            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = cresh;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (carCrashed) return;

        if (other.transform.CompareTag("TBR") && rihgtTurn)
        {
            RotateCar(rotateMultRight);
        }
        else if (other.transform.CompareTag("TBL") && leftTurn)
        {
            RotateCar(rotateMultLeft, -1);
        }
        else if (other.transform.CompareTag("DeleteCar"))
        {
            Destroy(gameObject);
        }
        if (other.transform.CompareTag("Freeze") && isFreeze && FreezeButton.freezeScore <= 3)
        {
            foreach (var i in GameObject.FindObjectsOfType<CarController>())
            {
                i.speed = 5f;
            }           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPast)
        {
            other.GetComponent<CarController>().speed = speed + 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (carCrashed) return;

        if (other.transform.CompareTag("TriggerPass"))
        {
            if (carPast) return;
            
            carPast = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
                col.enabled = true;

            countCars++;
        }
        if (other.transform.CompareTag("TBR") && rihgtTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRoteitionY
            + 90, 0);
        }

        if (other.transform.CompareTag("TBL") && leftTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRoteitionY
            - 90, 0);
        }

    }
    private void RotateCar(float speedRotate, int dir = 1)
    {
        if (carCrashed) return;

        if (dir == -1 && transform.localRotation.eulerAngles.y < originRoteitionY - 90f)
            return;
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return; //Может пригодится если y < 0 (c отрецательным значением) 

        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        carRb.MoveRotation(carRb.rotation * deltaRotation);
    }
}