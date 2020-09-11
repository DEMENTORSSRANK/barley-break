using System.Collections;
using UnityEngine;

namespace Effector
{
    public class Effect : MonoBehaviour
    {
        public Sprite[] samples;

        public SpriteRenderer spriteRenderer;

        public float offset = .01f;

        private IEnumerator Start()
        {
            foreach (var sample in samples)
            {
                spriteRenderer.sprite = sample;
                
                yield return new WaitForSeconds(offset);
            }
            
            Destroy(gameObject);
        }
    }
}