using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities.Audio;

// This script is for the GameOver Menu
namespace UI
{
    public class GameOverMenu : MonoBehaviour
    {
        public Text currentScore;
        public Text highScore;
        public Animator crossfade;
        
        public void Awake()
        {
            currentScore.text = Scoring.scoreValue.ToString();
            highScore.text = PlayerPrefs.GetInt(Constants.HIGHSCORE, 0).ToString();
        }

        //Restart Button
        public void Restart()
        {
            SceneManager.LoadScene("game");
            SFXManager.instance.PlaySound("Click");
        }

        //Home Button
        public void HomeMenu()
        {
            StartCoroutine(LoadScene());
        }
        
        IEnumerator LoadScene()
        {
            SFXManager.instance.PlaySound("Click");
            crossfade.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
