using System;
using Control;
using UnityEngine;

namespace Logic
{
    public class FieldMove : MonoBehaviour
    {
        public float toGetDistance = .5f;
        
        public Cell SelectedCell { get; set; }

        public bool CanMove { get; set; } = true;
        
        public static FieldMove Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void OnUpCell(Cell cell)
        {
            if (SelectedCell != cell || !CanMove)
                return;

            SelectedCell.IsSelected = false;
            
            SelectedCell = null;
        }
        
        public void OnDownCell(Cell cell)
        {
            if (SelectedCell != null || cell.MyData == null || Time.timeScale == 0 || !CanMove)
                return;

            cell.IsSelected = true;
            
            SelectedCell = cell;
        }

        public void OnEnterCell(Cell cell)
        {
            if (SelectedCell == null || SelectedCell == cell || !CanMove)
                return;

            var distanceSelected = Vector2.Distance(cell.transform.position, 
                SelectedCell.transform.position);

            var isComplete = distanceSelected <= toGetDistance && cell.MyData == null;

            if (!isComplete)
                return;
            
            FieldManage.Instance.ChangeCells(SelectedCell, cell);

            Sound.Instance.Click();
            
            SelectedCell.IsSelected = false;

            SelectedCell = null;
        }
    }
}