using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Control;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic
{
    public class FieldManage : MonoBehaviour
    {
        public Sprite[] cellSprites;

        public Cell[] allCells;

        public List<CellData> poolCells;

        public SpriteRenderer characterRenderer;

        public Sprite idleCharacter;
        
        public Sprite winCharacter;

        public Sprite gotSomeCharacter;

        private Coroutine _gotSome;
        
        public static FieldManage Instance;

        public void InitField()
        {
            poolCells.Clear();
            
            for (var i = 0; i < cellSprites.Length; i++)
            {
                var i1 = i;
                
                poolCells.Add(new CellData()
                {
                    avatar = cellSprites[i],
                    index = i1
                });
            }
            
            MixList(poolCells);

            for (var i = 0; i < poolCells.Count; i++)
            {
                allCells[i].MyData = poolCells[i];
            }

            var last = allCells.ToList().Last(); 
            
            last.MyData = null;

            var setRandomToEmpty = allCells[Random.Range(0, allCells.Length - 1)];
            
            ChangeCells(last, setRandomToEmpty);
        }

        public void ChangeCells(Cell firstCell, Cell secondCell)
        {
            var firstData = firstCell.MyData;

            firstCell.MyData = secondCell.MyData;

            secondCell.MyData = firstData;

            var gotSome = firstCell.IsRight() || secondCell.IsRight();
            
            CheckToCreatePuff(firstCell);
            
            CheckToCreatePuff(secondCell);
            
            if (gotSome)
                ShowGotSome();
            
            CheckWin();
        }

        private void CheckToCreatePuff(Cell cell)
        {
            if (!cell.IsRight())
                return;
                
            Effector.Effector.Instance.CreatePuff(cell.transform.position);
            
            Sound.Instance.Place();
        }

        private void ShowGotSome()
        {
            _gotSome = StartCoroutine(ShowingGot());
        }
        
        private void CheckWin()
        {
            for (var i = 0; i < allCells.Length - 1; i++)
            {
                if (allCells[i].MyData == null)
                    return;
                
                if (allCells[i].MyData.index != i)
                    return;
            }
            
            GameControl.Instance.WinGame();

            characterRenderer.sprite = winCharacter;

            characterRenderer.flipX = true;
        }
        
        private void Awake()
        {
            Instance = this;
        }

        private static void MixList<TT>(ICollection<TT> list)
        {
            var r = new System.Random();

            var mixedList = new SortedList<int, TT>();

            foreach (var item in list)
                mixedList.Add(r.Next(), item);

            list.Clear();

            for (var i = 0; i < mixedList.Count; i++)
            {
                list.Add(mixedList.Values[i]);
            }
        }

        private IEnumerator ShowingGot()
        {
            if (_gotSome != null)
                yield break;

            characterRenderer.sprite = gotSomeCharacter;
            
            yield return new WaitForSeconds(1.2f);

            characterRenderer.sprite = idleCharacter;
            
            _gotSome = null;
        }
    }

    [Serializable]
    public class CellData
    {
        public Sprite avatar;

        public int index;
    }
}