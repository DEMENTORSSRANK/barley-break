using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Control
{
    public class Value : MonoBehaviour
    {
        public Text currentTime;

        public Text bestTimeText;

        private int _nowSeconds;

        private Coroutine _timing;
        
        private int NowSeconds
        {
            get => _nowSeconds;

            set
            {
                _nowSeconds = value;
                
                var minutes = Mathf.FloorToInt(value / 60f);

                var seconds = value - minutes * 60;

                var isAddSeconds = seconds > 9 ? "" : "0";
                var isAddMinutes = minutes > 9 ? "" : "0";

                currentTime.text = isAddMinutes + minutes + ":" + isAddSeconds + seconds;
            }
        }

        private int BestSeconds
        {
            get => PlayerPrefs.GetInt("best");

            set
            {
                PlayerPrefs.SetInt("best", value);
                
                var minutes = Mathf.FloorToInt(value / 60f);

                var seconds = value - minutes * 60;

                bestTimeText.text = $"{minutes} min {seconds} sec";
            }
        }

        public static Value Instance;
        
        public void CheckBest()
        {
            if (NowSeconds >= BestSeconds && BestSeconds != 0) 
                return;
            
            BestSeconds = NowSeconds;
                
            Sound.Instance.Record();
        }
        
        public void StartTimer()
        {
            StopTimer();

            NowSeconds = 0;
            
            _timing = StartCoroutine(Timing());
        }

        public void StopTimer()
        {
            if (_timing == null)
                return;
            
            StopCoroutine(_timing);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            BestSeconds = BestSeconds;
        }

        private IEnumerator Timing()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                NowSeconds++;
            }
        }
        
    }
}