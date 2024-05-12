#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace FGUFW.CSV
{
    static class CreateConfigFile
    {

        [MenuItem("Assets/Create/CSV/enum",false,80)]
        static void createEnum()
        {
            string createPath = EditorUtils.EditorUtils.GetSeleceFolderPath()+"/Type.csv";
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,ScriptableObject.CreateInstance<CreateEnum>(),createPath,null,null);
        }

        class CreateEnum : EndNameEditAction
        {

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                //创建资源
                UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile);
                ProjectWindowUtil.ShowCreatedAsset(obj);//高亮显示资源
            }
    
            internal static UnityEngine.Object CreateScriptAssetFromTemplate(string filePath, string resourceFile)
            {
                string scriptText = 
@"|NAME|,enum,枚举类型
枚举名,枚举值,概要
None,0,无
";
                var className = Path.GetFileNameWithoutExtension(filePath);

                scriptText = scriptText.Replace("|NAME|",className);


                File.WriteAllText(filePath,scriptText,Encoding.UTF8);
                //刷新资源管理器
                AssetDatabase.ImportAsset(filePath);
                AssetDatabase.Refresh();
                return AssetDatabase.LoadAssetAtPath(filePath, typeof(UnityEngine.Object));
            }
        }
        
        
        [MenuItem("Assets/Create/CSV/class",false,80)]
        static void createClass()
        {
            string createPath = EditorUtils.EditorUtils.GetSeleceFolderPath()+"/Config.csv";
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,ScriptableObject.CreateInstance<CreateClass>(),createPath,null,null);
        }

        class CreateClass : EndNameEditAction
        {

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                //创建资源
                UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile);
                ProjectWindowUtil.ShowCreatedAsset(obj);//高亮显示资源
            }
    
            internal static UnityEngine.Object CreateScriptAssetFromTemplate(string filePath, string resourceFile)
            {
                string scriptText = 
@"|NAME|,class,配置概要
int,string,string
ID,Name,Summer
ID,名称,概要
";
                var className = Path.GetFileNameWithoutExtension(filePath);

                scriptText = scriptText.Replace("|NAME|",className);


                File.WriteAllText(filePath,scriptText,Encoding.UTF8);
                //刷新资源管理器
                AssetDatabase.ImportAsset(filePath);
                AssetDatabase.Refresh();
                return AssetDatabase.LoadAssetAtPath(filePath, typeof(UnityEngine.Object));
            }
        }


        
        
        
        [MenuItem("Assets/Create/CSV/struct",false,80)]
        static void createStruct()
        {
            string createPath = EditorUtils.EditorUtils.GetSeleceFolderPath()+"/Config.csv";
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,ScriptableObject.CreateInstance<CreateStruct>(),createPath,null,null);
        }

        class CreateStruct : EndNameEditAction
        {

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                //创建资源
                UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile);
                ProjectWindowUtil.ShowCreatedAsset(obj);//高亮显示资源
            }
    
            internal static UnityEngine.Object CreateScriptAssetFromTemplate(string filePath, string resourceFile)
            {
                string scriptText = 
@"|NAME|,struct,配置概要
int,int,int
ID,Name,Summer
ID,名称,概要
";
                var className = Path.GetFileNameWithoutExtension(filePath);

                scriptText = scriptText.Replace("|NAME|",className);


                File.WriteAllText(filePath,scriptText,Encoding.UTF8);
                //刷新资源管理器
                AssetDatabase.ImportAsset(filePath);
                AssetDatabase.Refresh();
                return AssetDatabase.LoadAssetAtPath(filePath, typeof(UnityEngine.Object));
            }
        }

    }
}
#endif