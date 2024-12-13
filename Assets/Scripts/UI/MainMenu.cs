using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utilities.Audio;

//This script is for Main Menu
namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public Text highScore;
        public Text coinns;
        public Animator crossFade;
        public void Start()
        {
            highScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt(Constants.HIGHSCORE).ToString(); //displaying HighScore
            coinns.text = "COINS: " + PlayerPrefs.GetInt(Constants.COINS).ToString(); //displaying the owned number of coins
        }

        //Play Button
        public void Play()
        {
            StartCoroutine(PlayScene());
        }

        //Quit Button
        public void Quit()
        {
            Application.Quit();
        }

        IEnumerator PlayScene()
        {
            SFXManager.instance.PlaySound("Click");
            crossFade.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("game");
        }
        
    }
}
