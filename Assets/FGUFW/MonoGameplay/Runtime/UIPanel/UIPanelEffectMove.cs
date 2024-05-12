using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGUFW.MonoGameplay
{
    [RequireComponent(typeof(UIPanel))]
    public class UIPanelEffectMove : UIPanelEffect
    {
        public float MaxSpace;
        public AnimationCurve ShowCurve;
        public Vector2 ShowOri = new Vector2(0,1);
        public AnimationCurve HideCurve;
        public Vector2 HideOri = new Vector2(0,1);
        private Coroutine _setCanvasMove;

        public override void Hide(UIPanel uIPanel)
        {
            if(_setCanvasMove!=null)StopCoroutine(_setCanvasMove);
            var startPoint = Vector2.zero;
            var endPoint = HideOri.normalized*MaxSpace;
            _setCanvasMove = StartCoroutine(setCanvasMove(uIPanel,HideCurve,startPoint,endPoint));
        }

        public override void Show(UIPanel uIPanel)
        {
            if(_setCanvasMove!=null)StopCoroutine(_setCanvasMove);
            var startPoint = -ShowOri.normalized*MaxSpace;
            var endPoint = Vector2.zero;
            _setCanvasMove = StartCoroutine(setCanvasMove(uIPanel,HideCurve,startPoint,endPoint));
        }

        IEnumerator setCanvasMove(UIPanel uIPanel,AnimationCurve alphaCurve,Vector2 startPoint,Vector2 endPoint)
        {
            uIPanel.transform.localPosition = startPoint;
            while (uIPanel.Progress<=1)
            {
                uIPanel.transform.localPosition = Vector3.Lerp(startPoint,endPoint,alphaCurve.Evaluate(uIPanel.Progress));
                yield return null;
            }
            uIPanel.transform.localPosition = endPoint;
            _setCanvasMove = null;
        }

    }
}