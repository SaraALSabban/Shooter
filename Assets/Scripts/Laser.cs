using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    float speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime *speed);

        if (transform.position.y > 8.90f)
        {
            //chack if has a parnet becuse it has a 3 laser in parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            
            }
            Destroy(gameObject);
        }
    }



}
