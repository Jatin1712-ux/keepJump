using System;
using Currency;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

//This script is for Spawning Boxes
namespace Box
{
    public class BoxSpawner : MonoBehaviour
    {
        //Public Variables
        public static bool isRight;
        public Transform[] spawnpoints;
        public GameObject[] boxprefabs;
        public GameObject bonus;
        
        //Private Variables
        private bool _isDoneOnce;
        private bool _isBonus;

        private void Start()
        {
            bonus = transform.Find("Bonus").gameObject;
            bonus.SetActive(false);
            _isBonus = false;
        }

        static BoxSpawner()

        {
            isRight = false;
        }

        

        private void Spawn()
        {
            int randomSpawnpoint = Random.Range(0, spawnpoints.Length ) ;
            int randomColor = Random.Range(0, boxprefabs.Length);
            var newbox =Instantiate(boxprefabs[randomColor], spawnpoints[randomSpawnpoint].position, Quaternion.identity);
            var player = GameObject.FindWithTag("Player");
            player.transform.LookAt(newbox.transform);
            var playerangels = player.transform.eulerAngles;
            playerangels.x = 0;
            player.transform.eulerAngles = playerangels ;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && _isDoneOnce == false )
            {
                Spawn();
                _isDoneOnce = true;
                Scoring.scoreValue += 1;   //Incrementing the Score
                Coins.coinValue += 1;      //Incrementing the number of coins
                PlayerPrefs.SetInt(Constants.COINS, Coins.coinValue);
            }

            if (collision.gameObject.CompareTag("Player") && _isBonus == true)
            {
                Coins.coinValue += 5;
                
            }
        }
    }
}
