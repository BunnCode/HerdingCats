using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDscript : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public static int lives;

    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        lives = 1;
        SetLivesText();
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetLivesText();
        if (lives == 0)
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
}
