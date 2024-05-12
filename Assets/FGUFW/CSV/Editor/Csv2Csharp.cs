#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FGUFW.CSV
{
    public static class Csv2Csharp
    {
        const string Extension = ".csv";

        static string codeGenDirectory => $"{Application.dataPath}/CodeGen/CSV";
        

        [MenuItem("Assets/Csv2Csharp")]
        private static void Build()
        {
            var selects = Selection.objects;
            if(selects==null)
            {
                return;
            }

            if(!Directory.Exists(codeGenDirectory))
            {
                Directory.CreateDirectory(codeGenDirectory);
            }

            //筛选csv文件
            List<string> paths = new List<string>();
            foreach (var obj in selects)
            {
                string path = Application.dataPath.Replace("Assets",AssetDatabase.GetAssetPath(obj));
                if(Path.GetExtension(path)==Extension)
                {
                    paths.Add(path);
                }
            }
            if(paths.Count==0)return;
            
            foreach (var path in paths)
            {
                var table = CsvHelper.Parse2(File.ReadAllText(path));
                var name = Path.GetFileNameWithoutExtension(path);
                var savePath = $"{codeGenDirectory}/{name}.cs";
                var csharptype = table[0,1];
                switch (csharptype)
                {
                    case "class":
                    {
                        File.WriteAllText(savePath,ScriptTextHelper.Csv2CsharpClass(table));
                    }
                    break;
                    case "struct":
                    {
                        File.WriteAllText(savePath,ScriptTextHelper.Csv2CsharpStruct(table));
                    }
                    break;
                    case "enum":
                        File.WriteAllText(savePath,ScriptTextHelper.Csv2CsharpEnum(table));
                    break;
                    default:
                        Debug.LogError($"未标注类型 {csharptype}");
                    break;
                }
            }
            
            AssetDatabase.Refresh();
        }


    }
}
#endif