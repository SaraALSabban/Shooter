using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject[] powerUp;

    [SerializeField]
    private GameObject enemyContainer;

    private bool stopSpawning = false;



    // Start is called before the first frame update
    void Start()
    {

        

    }

    public void StartSpwoning()
    {
        StartCoroutine("SpawonEnemyRoutine");
        StartCoroutine("SpawonPowerupRoutine");

    }

    // Update is called once per frame
    void Update()
    {
        
    }





    IEnumerator SpawonPowerupRoutine()
    {

        yield return new WaitForSeconds(3.0f);

        while (stopSpawning == false)
        {

            Vector3 posSpwan = new Vector3(Random.Range(-8, 8), 7, 0);
            int randomPowerUp = Random.Range(0,3);
            Instantiate(powerUp[randomPowerUp], posSpwan, Quaternion.identity);
          

            yield return new WaitForSeconds(Random.Range(3.0f,7.0f));
        }

       
     
    }







    IEnumerator SpawonEnemyRoutine()
    {

        yield return new WaitForSeconds(3.0f);

        while (stopSpawning == false)
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
            GameObject newEnmey = Instantiate(enemyPrefab, posToSpawn,Quaternion.identity);
            newEnmey.transform.parent = enemyContainer.transform;

            yield return new  WaitForSeconds(4.0f);
        
        }

    }



    public void OnPlayerDeath()
    {
        stopSpawning = true;
       
    
    }













}
