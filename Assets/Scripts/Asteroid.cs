using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 20.0f;

    [SerializeField]
    private GameObject expolsion;

    private SpwonManager _spwonmanager;


    void Start()
    {
        _spwonmanager = GameObject.Find("Spwon Manager").GetComponent<SpwonManager>();

        if (_spwonmanager == null)
        {
            Debug.Log("spwon manager is null");
         
        }
    }

    
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
           
            Instantiate(expolsion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spwonmanager.StartSpwoning();
            Destroy(this.gameObject,0.25f);

        }
    }
}
