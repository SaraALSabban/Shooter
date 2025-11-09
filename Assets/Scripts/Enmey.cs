using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;

    private Player _player;

    Animator anim;


    private AudioSource audioSource;

    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.Log("the player is null"); 
        }

        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.Log("the anim is null");
        }


        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {

            Debug.Log("audio source on player is null!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed* Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomx = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomx, 7,0);
        
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player= other.transform.GetComponent<Player>();

            if (player != null)
            {

                player.Damage();            
            }


            anim.SetTrigger("OnEnemyDeath");
             speed = 0;
            audioSource.Play();
            Destroy(this.gameObject,2.9f);
           
        
        }



        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {

                _player.AddScore(10);
            }

            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            audioSource.Play();
            Destroy(this.gameObject,2.9f);
           
        }

    }






}
