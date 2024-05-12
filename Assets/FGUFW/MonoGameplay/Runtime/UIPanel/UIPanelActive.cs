using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGUFW.MonoGameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIPanel))]
    public class UIPanelActive : UIPanelEffect
    {       

        public override void Hide(UIPanel uIPanel)
        {
            uIPanel.gameObject.SetActive(false);
        }

        public override void Show(UIPanel uIPanel)
        {
            uIPanel.gameObject.SetActive(true);
        }
    }
}
