using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float speed = 3.0f;

    //id for each one
    [SerializeField]
    private int powerUpID; // 0 tripleshot 1 speed 2 shild


    

    [SerializeField]
    private AudioClip powerAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -5.0f)
        {


            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Player player = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(powerAudio, transform.position);

            if (player != null)
            {

                switch (powerUpID)
                {
                    case 0:

                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedBoostActive();
                        break;

                    case 2:
                        player.ShiledActive();

                        break;

                }






            }
            
            Destroy(this.gameObject);

        }
    }



}
