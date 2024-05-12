# if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using FGUFW;
using FGUFW.EditorUtils;
using UnityEditor;
using UnityEngine;

namespace FGUFW.MonoGameplay
{

    public static class CreateScript
    {
        [MenuItem("Assets/Create/MonoGameplay/Play",false,80)]
        static void createPlay()
        {
            string createPath = FGUFW.EditorUtils.EditorUtils.GetSeleceFolderPath()+"/Play.cs";
            var endNameEditAction = ScriptableObject.CreateInstance<CreateScriptHelper>();
            endNameEditAction.Callback = (filePath)=>
            {
                var scriptText = 
@"using System.Collections;
using System.Collections.Generic;
using FGUFW.MonoGameplay;
using UnityEngine;

namespace |NAME_SPACE|
{
    public class |CLASS_NAME| : Play<|CLASS_NAME|>
    {
        public override IEnumerator OnCreating(Part play,Part parent)
        {
            //AddPart<MonoGameplayTestPart>();
            
            yield return base.OnCreating(this,this);
        }

        public override IEnumerator OnDestroying(Part parent)
        {

            yield return base.OnDestroying(parent);
        }
    }
}

";

                var className = Path.GetFileName(filePath).Replace(".cs","");
                scriptText = scriptText.Replace("|CLASS_NAME|",className);

                MonoGameplaySettingsProvider.SettingData.NameSpace = className.Replace("Play","");
                MonoGameplaySettingsProvider.SettingData.PlayName = className;
                MonoGameplaySettingsProvider.SettingData.Save();

                scriptText = scriptText.Replace("|NAME_SPACE|",MonoGameplaySettingsProvider.SettingData.NameSpace);

                return scriptText;
            };
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,endNameEditAction,createPath,null,null);
        }

        [MenuItem("Assets/Create/MonoGameplay/Part",false,80)]
        static void createPart()
        {
            string createPath = FGUFW.EditorUtils.EditorUtils.GetSeleceFolderPath()+"/Part.cs";
            var endNameEditAction = ScriptableObject.CreateInstance<CreateScriptHelper>();
            endNameEditAction.Callback = (filePath)=>
            {
                var scriptText = 
@"using System.Collections;
using System.Collections.Generic;
using FGUFW.MonoGameplay;
using UnityEngine;

namespace |NAME_SPACE|
{
    
    public class |CLASS_NAME| : Part
    {
        private |PLAY_NAME| _play;

        public override IEnumerator OnCreating(Part play,Part parent)
        {
            _play = play as |PLAY_NAME|;
            addListener();
            yield return base.OnCreating(parent);
        }

        public override IEnumerator OnDestroying(Part parent)
        {

            removeListener();
            yield return base.OnDestroying(parent);
        }

        private void addListener()
        {

        }

        private void removeListener()
        {
            
        }

    }
}

";

                var className = Path.GetFileName(filePath).Replace(".cs","");
                scriptText = scriptText.Replace("|CLASS_NAME|",className);
                scriptText = scriptText.Replace("|NAME_SPACE|",MonoGameplaySettingsProvider.SettingData.NameSpace);
                scriptText = scriptText.Replace("|PLAY_NAME|",MonoGameplaySettingsProvider.SettingData.PlayName);

                return scriptText;
            };
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,endNameEditAction,createPath,null,null);
        }
    }


}

#endif