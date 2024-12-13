using System;
using UnityEngine;
using UnityEngine.UI;

//This script is for keeping score
namespace UI
{
    public class Scoring : MonoBehaviour
    {
        public static int scoreValue;
        public Text score;
        public static int highScore;
       
        private void Start()
        {
            score = GetComponent<Text>();
            scoreValue = 0;
            highScore = PlayerPrefs.GetInt(Constants.HIGHSCORE, 0);
        }

        private void Update()
        {
            if (scoreValue > PlayerPrefs.GetInt(Constants.HIGHSCORE, 0))
            {
                PlayerPrefs.SetInt(Constants.HIGHSCORE, scoreValue);
            }

            score.text = scoreValue.ToString();
        }
    }
}
