using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDscript : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public static int lives;

    public GameObject gameOverText;
    public GameObject playAgainButton;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        lives = 1;
        SetLivesText();
        gameOverText.SetActive(false);
        playAgainButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //this could be better. would call SetLivesText() after a cat dies but not sure how to call inside the CatAI script
        SetLivesText();
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
