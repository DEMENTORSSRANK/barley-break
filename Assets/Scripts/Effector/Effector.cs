using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effector
{
    public class Effector : MonoBehaviour
    {
        public Effect salutePrefab;
        
        public Vector2 firstPos;

        public Vector2 secondPos;

        [Space(5)] 
        
        public Effect puffPrefab;
        
        public static Effector Instance;

        public void CreateSalutes()
        {
            CreateSalute(firstPos);
            
            CreateSalute(secondPos);
        }

        public void CreatePuff(Vector2 position)
        {
            Instantiate(puffPrefab, position, Quaternion.identity);
        }
        
        private void CreateSalute(Vector2 forStartPos)
        {
            var position = forStartPos;

            position.x += Random.Range(-.4f, .4f);

            position.y += Random.Range(-.4f, .4f);

            Instantiate(salutePrefab, position, Quaternion.identity);
        }
        
        private void Awake()
        {
            Instance = this;
        }
    }
}