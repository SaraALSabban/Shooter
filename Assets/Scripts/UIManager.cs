using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Image livesImage;


    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text gameOver;

    [SerializeField]
    private Text _resetText;

   private GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score :" + 0;
        gameOver.gameObject.SetActive(false);
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gamemanager == null)
        {
            Debug.Log("Gamemananger is null");
        }
    }

    public void UpdateScore(int playerScore)
    {

        scoreText.text = "Score :" + playerScore.ToString();
    
    }


    public void UpdateLives(int currentlives)
    {

        livesImage.sprite = _liveSprites[currentlives];

        if (currentlives == 0)
        {
            GameOverSequence();


        }
    
    
    }

    void GameOverSequence()
    {
        gamemanager.GameOver();
        gameOver.gameObject.SetActive(true);
        _resetText.gameObject.SetActive(true);
        StartCoroutine(GameOverFilckerRoutine());

    }


    IEnumerator GameOverFilckerRoutine()
    {
        while (true)
        {
            gameOver.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    
    
    }
}
