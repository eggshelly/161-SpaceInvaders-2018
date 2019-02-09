using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public static LevelController levelController;

    [SerializeField] private int maxLife = 3;
    [SerializeField] private GameObject pausePannel;
    [SerializeField] private GameObject winPannel;
    [SerializeField] private GameObject gameOverPannel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    private int life;// = maxLife;

    private int points = 0;

    private void Awake()
    {
        levelController = this;
        life = maxLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pausePannel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pausePannel.SetActive(true);
        }
       
    }

    public void onWin()
    {
        Time.timeScale = 0;
        winPannel.SetActive(true);
    }

    public void checkGameOver()
    {
        if(life <= 0)
        {
            gameOver();
        }
    }
    public void gameOver()
    {
        Time.timeScale = 0;
        gameOverPannel.SetActive(true);
    }
    public void addPoints(int x)
    {
        points += x;
        scoreText.text = string.Format("Score: {0}", points);
    }
    public void decreaseLife()
    {
        life--;
        lifeText.text = string.Format("Life remaining: {0}", life);
        checkGameOver();
    }
}
