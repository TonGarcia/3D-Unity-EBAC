using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace FX
{
    public class FlashColor : MonoBehaviour
    {
        [FormerlySerializedAs("MeshRenderer")] public MeshRenderer meshRenderer;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        
        [Header("Setup")] 
        public Color color = Color.red;
        public float duration = .1f;
        public string shaderAttr = "_EmissionColor";

        // private Color defaultColor;
        private Tween _currentTween;

        #region Unity Events
        // private void Start()
        // {
        //     defaultColor = meshRenderer.material.GetColor(shaderAttr);
        // }

        private void OnValidate()
        {
            if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
            if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
        #endregion
        
        [NaughtyAttributes.Button]
        public void Flash()
        {
            if (meshRenderer != null && !_currentTween.IsActive())
            {
                // editing the Shader attr _EmissionColor with Tweening animation
                _currentTween = meshRenderer.material.DOColor(color, shaderAttr, duration).SetLoops(2, LoopType.Yoyo);    
            }

            if (skinnedMeshRenderer != null && !_currentTween.IsActive())
            {
                _currentTween = skinnedMeshRenderer.material.DOColor(color, shaderAttr, duration).SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}
