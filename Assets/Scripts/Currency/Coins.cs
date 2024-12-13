using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Currency
{
    public class Coins : MonoBehaviour
    {
        public static int coinValue;
        public Text coinText;

        private void Start()
        {
            coinText = GetComponent<Text>();
            coinValue = PlayerPrefs.GetInt(Constants.COINS, 0);
        }

        private void Update()
        {
            coinText.text ="COINS: " + coinValue.ToString();
        }
    }
}
