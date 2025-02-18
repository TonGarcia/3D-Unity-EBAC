using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CombatSystem.Gun
{
    public class UIUpdater : MonoBehaviour
    {
        [Header("Animation")] 
        public float duration = .1f;
        public Ease ease = Ease.OutBack;
        private Tween _currentTween;
        
        public Image uiImage;
        
        private void OnValidate()
        {
            if (uiImage == null) uiImage = GetComponent<Image>();
        }

        public void UpdateValue(float f)
        {
            uiImage.fillAmount = f;
        }

        public void UpdateValue(float max, float current)
        {
            if(_currentTween != null) _currentTween.Kill();
            
            // 10 / 100 => 0.1 (10%)
            float fillAmount = 1 - (current / max);
            _currentTween = uiImage.DOFillAmount(fillAmount, duration).SetEase(ease);
        }
    }
}