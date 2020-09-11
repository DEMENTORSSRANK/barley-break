using System;
using System.Collections;
using Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Control
{
    public class GameControl : MonoBehaviour
    {
        public GameObject menu;

        public GameObject game;

        public GameObject pause;

        public static GameControl Instance;


        public void Pause(bool isActive)
        {
            Time.timeScale = isActive ? 0 : 1;
            
            pause.SetActive(isActive);
        }

        public void WinGame()
        {
            print("win");
            
            Value.Instance.CheckBest();

            Sound.Instance.Win();
            
            StartCoroutine(ShowingWin());
        }
        
        public void StartGame()
        {
            game.SetActive(true);
            
            menu.SetActive(false);
            
            Value.Instance.StartTimer();
            
            FieldManage.Instance.InitField();
        }

        public void RestartGame()
        {
            Pause(false);
            
            StartGame();
        }
        
        public void LoadMenu()
        {
            Time.timeScale = 1;

            SceneManager.LoadScene("game");
        }

        private void Start()
        {
            game.SetActive(false);
            
            menu.SetActive(true);
        }

        private void Awake()
        {
            Instance = this;

            Screen.orientation = ScreenOrientation.AutoRotation;

            Screen.autorotateToPortrait = false;

            Screen.autorotateToPortraitUpsideDown = false;
        }

        private IEnumerator ShowingWin()
        {
            Effector.Effector.Instance.CreateSalutes();
            
            yield return new WaitForSeconds(3f);
            
            LoadMenu();
        }
    }
}
