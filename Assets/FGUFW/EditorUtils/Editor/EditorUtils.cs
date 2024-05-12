#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using FGUFW.Platform;
using FGUFW;
using System;

namespace FGUFW.EditorUtils
{
    public static class EditorUtils
    {
        public const string META = ".meta";
        public static string[] GetAllAssetPath(string dirPath)
        {
            var dir = Application.dataPath.Replace("Assets",dirPath);
            if(!Directory.Exists(dir))return new string[0];
            int length = 0;
            var flies = Directory.GetFiles(dir);
            foreach (var path in flies)
            {
                if(Path.GetExtension(path)==META)continue;
                length++;
            }
            var paths = new string[length];
            int index = 0;
            foreach (var path in flies)
            {
                if(Path.GetExtension(path)==META)continue;
                paths[index] = path.Replace(Application.dataPath,"Assets").Replace("\\","/");
                index++;
            }
            return paths;
        }

        public static string GetSeleceFolderPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if(string.IsNullOrEmpty(path))
            {
                return null;
            }
            if(!AssetDatabase.IsValidFolder(path))
            {
                path = Path.GetDirectoryName(path).Replace("\\","/");
            }
            return path;
        }

        
        // [MenuItem("Assets/PrintAssetPath")]
        // static private void printAssetPath()
        // {
        //     string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //     Debug.Log(path);
        // }

        [MenuItem("文件夹/程序持续存储文件夹")]
        static void openDir()
        {
            WinPlatform.OpenExplorer(Application.persistentDataPath);
        }

        public static BuildTargetGroup GetCurrentBuildTargetGroup()
        {
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
            return buildTargetGroup;
        }

        public static void ScriptingDefineSymbols_Add(string define)
        {
            var buildTargetGroup = GetCurrentBuildTargetGroup();
            string[] defines = null;
            PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup,out defines);
            bool contains = defines!=null && Array.IndexOf<string>(defines,define)!=-1;
            if(!contains)
            {
                var newDefines = new string[defines.Length+1];
                Array.Copy(defines,newDefines,defines.Length);
                newDefines[newDefines.Length-1]=define;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,newDefines);
            }
        }

        public static void ScriptingDefineSymbols_Remove(string define)
        {
            var buildTargetGroup = GetCurrentBuildTargetGroup();
            string[] defines = null;
            PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup,out defines);
            bool contains = defines!=null && Array.IndexOf<string>(defines,define)!=-1;
            if(contains)
            {
                var newDefines = new string[defines.Length-1];
                for (int i = 0,newIdx=0; i < defines.Length; i++)
                {
                    if(defines[i]!=define)
                    {
                        newDefines[newIdx++]=defines[i];
                    }
                }
                
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,newDefines);
            }
        }

        public static bool ScriptingDefineSymbols_Contains(string define)
        {
            var buildTargetGroup = GetCurrentBuildTargetGroup();
            string[] defines = null;
            PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup,out defines);
            bool contains = defines!=null && Array.IndexOf<string>(defines,define)!=-1;
            return contains;
        }


    }
}
#endif