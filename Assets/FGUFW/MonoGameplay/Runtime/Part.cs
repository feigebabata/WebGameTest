using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FGUFW;

namespace FGUFW.MonoGameplay
{
    public abstract class Part : MonoBehaviour,IPartUpdate,IPartPreload
    {
        public List<Part> SubParts = new List<Part>();

        protected UIPanel _uiPanel;

        public virtual IEnumerator OnCreating(Part play,Part parent)
        {
            foreach (var subPart in SubParts)
            {
                yield return subPart.OnCreating(play,this);
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if(_uiPanel)
            {
                Destroy(_uiPanel.gameObject);
            }
            SubParts.Clear();
        }

        public T GetPart<T>() where T : Part
        {
            foreach (var subPart in SubParts)
            {
                if(subPart is T)
                {
                    return (T)subPart;
                }
            }
            return default;
        }

        public T AddPart<T>() where T : Part
        {
            var part = Create<T>(this);
            SubParts.Add(part);
            return part;
        }

        public virtual void OnUpdate(in PlayFrameData playFrameData)
        {
            foreach (var item in SubParts)
            {
                item.OnUpdate(in playFrameData);
            }
        }

        public virtual IEnumerator OnPreload()
        {
            yield return loadUIPanel();

            foreach (var subPart in SubParts)
            {
                yield return subPart.OnPreload();
            }
        }

        private IEnumerator loadUIPanel()
        {
            var uiPanelLoader = this.GetAttribute<UIPanelLoaderAttribute>();
            if (uiPanelLoader != null)
            {
                var path = uiPanelLoader.PrefabPath;
                var loader = AssetHelper.CopyAsynchronous(path,null);
                yield return loader;
                GameObject go = loader.Result;
                DontDestroyOnLoad(go);
                _uiPanel = go.GetComponent<UIPanel>();
            }
        }

        public static T Create<T>(Part parent) where T : Part
        {
            Transform tp = parent==default?null:parent.transform;
            var part = new GameObject(typeof(T).Name).AddComponent<T>();
            DontDestroyOnLoad(part.gameObject);
            part.transform.parent = tp;
            part.transform.localPosition = Vector3.zero;
            return part;
        }
        

    }

}
