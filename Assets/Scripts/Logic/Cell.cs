using System;
using System.Linq;
using UnityEngine;

namespace Logic
{
    public class Cell : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        private CellData _myData;

        private Sprite Avatar
        {
            get => spriteRenderer.sprite;

            set => spriteRenderer.sprite = value;
        }

        public CellData MyData
        {
            get => _myData;
            set
            {
                _myData = value;

                Avatar = value?.avatar;
            }
        }

        public bool IsSelected
        {
            get => spriteRenderer.color == Color.gray;

            set => spriteRenderer.color = value ? Color.gray : Color.white;
        }

        public bool IsRight()
        {
            if (MyData == null)
                return false;

            return MyData.index == FieldManage.Instance.allCells.ToList().IndexOf(this);
        }

        private void OnMouseDown()
        {
            FieldMove.Instance.OnDownCell(this);
        }

        private void OnMouseEnter()
        {
            FieldMove.Instance.OnEnterCell(this);
        }

        private void OnMouseUp()
        {
            FieldMove.Instance.OnUpCell(this);
        }
    }
}