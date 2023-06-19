using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using DG.Tweening;
using UI.View;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private UnitTooltip Tooltip;
        [SerializeField] private Image UnitImage;
        [SerializeField] private Image UnitBorder;
        [SerializeField] private UnitOrientation Orientation = UnitOrientation.Left;
    
        private UnitData Data;
    
        public void ShowTooltip()
        {
            Tooltip.Show(transform.position);
        }

        public void HideTooltip()
        {
            Tooltip.Hide();
        }

        public void SetSelected()
        {
            UnitBorder.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            UnitBorder.gameObject.SetActive(false);
        }
    
        public void SetUnitData(UnitData dataToSet)
        {
            Data = dataToSet;
            UpdateUnitDataView();
        }

        public void UpdateUnitDataView()
        {
            Tooltip.SetUnitData(Data);
            UnitImage.color = Data.UnitColor;
        }

        public void AnimateAttack(Action callback)
        {
            var OriginalPosition = transform.localPosition;
            transform.DOLocalMoveX(OriginalPosition.x + (Orientation == UnitOrientation.Left ? 100 : -100) , 0.5f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                transform.DOLocalMoveX(OriginalPosition.x, 0.3f).SetEase(Ease.Linear);
                callback();
            });
        }

        public void AnimateTakeDamage(Action callback)
        {
            var OriginalPosition = transform.localPosition;
            transform.DOLocalMoveX(OriginalPosition.x + (Orientation == UnitOrientation.Left ? -100 : 50) , 0.25f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                transform.DOLocalMoveX(OriginalPosition.x, 0.3f).SetEase(Ease.Linear);
                callback();
            });
        }

        public void AnimateDeath(Action callback)
        {
            transform.DOKill();
            transform.DOShakePosition(1,50);
            UnitImage.DOFade(0, 1.2f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                callback();
            });
        }
    }

    public enum UnitOrientation
    {
        Left,
        Right
    }
}