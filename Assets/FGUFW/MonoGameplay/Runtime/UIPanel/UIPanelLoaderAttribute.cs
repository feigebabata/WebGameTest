using System;

namespace FGUFW.MonoGameplay
{
    public class UIPanelLoaderAttribute:Attribute
    {
        public string PrefabPath;
        public UIPanelLoaderAttribute(string prefabPath)
        {
            PrefabPath = prefabPath;
        }
    }

}