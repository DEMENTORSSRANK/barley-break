using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Control
{
    public class Sound : MonoBehaviour
    {
        public AudioSource source;

        public AudioClip win;

        public AudioClip click;

        public AudioClip record;

        public AudioClip place;

        public Button[] interactsSound;

        public Sprite activeSound;

        public Sprite inActiveSound;

        public Button[] buttons;
        
        public static Sound Instance;
        
        private float Volume
        {
            get => PlayerPrefs.GetFloat("volume");

            set
            {
                if (!PlayerPrefs.HasKey("volume"))
                    value = 1;
                
                source.volume = value;
                
                PlayerPrefs.SetFloat("volume", value);

                interactsSound.ToList().ForEach(x => x.image.sprite = value > 0 ? activeSound : inActiveSound);
            }
        }
        
        public void Win()
        {
            source.PlayOneShot(win);
        }

        public void Click()
        {
            source.PlayOneShot(click);
        }

        public void Record()
        {
            source.PlayOneShot(record);
        }

        public void Place()
        {
            source.PlayOneShot(place);
        }

        private void Interact()
        {
            Volume = Volume == 0 ? 1 : 0;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Volume = Volume;
            
            buttons.ToList().ForEach(x => x.onClick.AddListener(Click));
            
            interactsSound.ToList().ForEach(x => x.onClick.AddListener(Interact));
        }
    }
}