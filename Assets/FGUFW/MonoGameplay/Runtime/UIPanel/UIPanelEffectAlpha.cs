using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGUFW.MonoGameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIPanel))]
    public class UIPanelEffectAlpha : UIPanelEffect
    {
        public AnimationCurve ShowCurve;
        public AnimationCurve HideCurve;
        private Coroutine _setCanvasAlpha;

        public override void Hide(UIPanel uIPanel)
        {
            if(_setCanvasAlpha!=null)StopCoroutine(_setCanvasAlpha);
            _setCanvasAlpha = StartCoroutine(setCanvasAlpha(uIPanel,HideCurve));
        }

        public override void Show(UIPanel uIPanel)
        {
            if(_setCanvasAlpha!=null)StopCoroutine(_setCanvasAlpha);
            _setCanvasAlpha = StartCoroutine(setCanvasAlpha(uIPanel,ShowCurve));
        }

        IEnumerator setCanvasAlpha(UIPanel uIPanel,AnimationCurve alphaCurve)
        {
            while (uIPanel.Progress<=1)
            {
                uIPanel.Canvas.alpha = alphaCurve.Evaluate(uIPanel.Progress);
                yield return null;
            }
            uIPanel.Canvas.alpha = alphaCurve.Evaluate(1);
            _setCanvasAlpha = null;
        }

    }
}
