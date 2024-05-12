using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGUFW.MonoGameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIPanel))]
    public class UIPanelEffectScale : UIPanelEffect
    {
        public AnimationCurve ShowCurve;
        public AnimationCurve HideCurve;
        private Coroutine _setCanvasScale;

        void Awake()
        {
            if(GetComponent<Canvas>().renderMode != RenderMode.WorldSpace)
            {
                Debug.LogError("UIPanelEffectScale需要Canvas渲染模式为WorldSpace!");
            }
        }

        public override void Hide(UIPanel uIPanel)
        {
            if(_setCanvasScale!=null)StopCoroutine(_setCanvasScale);
            _setCanvasScale = StartCoroutine(setCanvasScale(uIPanel,HideCurve));
        }

        public override void Show(UIPanel uIPanel)
        {
            if(_setCanvasScale!=null)StopCoroutine(_setCanvasScale);
            _setCanvasScale = StartCoroutine(setCanvasScale(uIPanel,ShowCurve));
        }

        IEnumerator setCanvasScale(UIPanel uIPanel,AnimationCurve alphaCurve)
        {
            while (uIPanel.Progress<=1)
            {
                uIPanel.Trans.localScale = alphaCurve.Evaluate(uIPanel.Progress)*Vector3.one;
                yield return null;
            }
            uIPanel.Trans.localScale = alphaCurve.Evaluate(1)*Vector3.one;
            _setCanvasScale = null;
        }

    }
}
