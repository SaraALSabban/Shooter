using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.9f;
    private float speedMultiplier = 2.0f;

    [SerializeField]
    private GameObject laserPrefabs , triblePrefabs;

    [SerializeField]
    private GameObject fireRight,fireLeft;



    [SerializeField]
    private GameObject shiledVisualizer;

    [SerializeField]
    private float firerate = 0.15f;

    private float canFire = -1f;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private int score;


    private SpwonManager spwonManager;

    [SerializeField]
    private bool isTribleShotActive = false ;
    [SerializeField]
    private bool isSpeedBoostActive = false;
    [SerializeField]
    private bool isShiledActive = false;


    [SerializeField]
    private AudioClip laserShoot ;

    
    private AudioSource  audioSorce;



    private UIManager _uIManager;

    // Start is called before the first frame update
    void Start()
    {
        //take the crueent pos = new pos (0,0,0)

        transform.position = new Vector3(0, 0, 0);
        spwonManager = GameObject.Find("Spwon Manager").GetComponent<SpwonManager>();

        if (spwonManager == null)
        {
            Debug.LogError("not find");
        }


        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uIManager == null)
        {
            Debug.LogError("not find ui manager");
        }

        audioSorce = GetComponent<AudioSource>();

        if (audioSorce == null)
        {

            Debug.Log("audio source on player is null!");
        }
        else
        {
            audioSorce.clip = laserShoot;
        
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();

        }

           
    }


    void CalculateMovement()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        //vector (1,0,0)* 5 speed * real time
        //transform.Translate(Vector3.right *inputHorizontal*speed* Time.deltaTime);
        // transform.Translate(Vector3.up * inputVertical * speed * Time.deltaTime);

        // or other way clean code
        Vector3 Direction = new Vector3(inputHorizontal, inputVertical, 0);



           transform.Translate(Direction * speed * Time.deltaTime);

        
       



       /* if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);

        } */


       //clarn code

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //يتحرك من اليمن ويخرج من اليسار والعكس عشان كده ماحصره مثل اللي فوق

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }

        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }

    }



    void FireLaser()
    {
        canFire = Time.time + firerate;


        if (isTribleShotActive == true)
        {
            Instantiate(triblePrefabs, transform.position, Quaternion.identity);

        }

        else
        {
            Instantiate(laserPrefabs, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

        }


        audioSorce.Play();


    }





    public void Damage()
    {
        if (isShiledActive == true)
        {
            isShiledActive = false;
            shiledVisualizer.SetActive(false);
            return;
        
        }


        lives--;


        if (lives == 2)
        {
            fireLeft.SetActive(true);

        }

        else if (lives == 1)
        {
           fireRight.SetActive(true);
        }

        _uIManager.UpdateLives(lives);

        if (lives < 1 )
        {
            spwonManager.OnPlayerDeath();
            Destroy(this.gameObject);

           
        }
    
    }


    public void TripleShotActive()
    {
        isTribleShotActive = true;
        StartCoroutine("TripleShotPowerDownRoutine");
    
    }



    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        isTribleShotActive = false;
    
    }




    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }


    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;

    }



    public void ShiledActive()
    {
        isShiledActive = true;
        shiledVisualizer.SetActive(true);
    
    }



    public void AddScore(int points)
    {
        score += points;
        _uIManager.UpdateScore(score);
    }



}
