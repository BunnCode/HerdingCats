using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDscript : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public static int lives;

    public TextMeshProUGUI freeMeterText;
    public static int freeMeter;

    public GameObject gameOverText;
    public GameObject playAgainButton;


    // Start is called before the first frame update
    void Start()
    {
        freeMeterText.alpha = 0.0F;
        Time.timeScale = 1;
        lives = 3;
        freeMeter = 0;
        SetLivesText();
        SetFreeMeterText();
        gameOverText.SetActive(false);
        playAgainButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //this could be better. would call SetLivesText() after a cat dies but not sure how to call inside the CatAI script
        SetLivesText();
        if (FreeAction.freeTime == true)
        {
            freeMeterText.alpha = 1.0F;
        }
        else
        {
            freeMeterText.alpha = 0.0F;
        }
        SetFreeMeterText();

        if (lives == 0)
        {
            gameOverText.SetActive(true);
            playAgainButton.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void SetFreeMeterText()
    {
        freeMeterText.text = freeMeter.ToString() + "/3";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
