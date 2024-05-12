#if UNITY_EDITOR
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class FirstOpenPathWindow:EditorWindow
{
    readonly Vector2 windowSize = new Vector2(200,30);
    readonly Rect inputRect = new Rect(5,2.5f,190,25);

    public string PathName;
    public Action<string> OnEditEnd;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        this.titleContent = new GUIContent("输入名称 不能重复");
        this.minSize = windowSize;
        this.maxSize = windowSize;
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        PathName = EditorGUI.TextField(inputRect,PathName);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        OnEditEnd?.Invoke(PathName);
    }
}

public static class FirstOpenPath
{

    const string SCRIPT_PATH = "Assets/FGUFW/EditorTool/FirstOpenPath/FirstOpenPath.cs";

    [MenuItem("Assets/添加到快捷路径菜单")]
    private static void addPathToEditorMenu()
    {
        if(Selection.activeObject==null)return;

        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        var window = EditorWindow.GetWindow<FirstOpenPathWindow>();
        var pathName = Path.GetFileName(path);
        window.OnEditEnd += pn=>
        {
            if(string.IsNullOrEmpty(pn))return;
            addNewPath(path,pn);
        };
        window.PathName = pathName;
        window.Show();
    }

    private static void addNewPath(string path,string title)
    {
        var scriptPath = Application.dataPath.Replace("Assets",SCRIPT_PATH);
        var scriptText = File.ReadAllText(scriptPath);

        var funName = Regex.Replace(path, "[^A-Za-z0-9]+", "");

        if(scriptText.Contains($"private static void showPath_{funName}()"))
        {
            Debug.LogWarning("该路径已存在");
            return;
        }

        const string new_item = 
@"
    [MenuItem(""快捷路径/|NAME|"")]
    private static void showPath_|FUN_NAME|()
    {
        string path = ""|PATH|"";
        var obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
        ProjectWindowUtil.ShowCreatedAsset(obj);
    }
";   
        var scriptItem = new_item.Replace("|NAME|",title);
        scriptItem = scriptItem.Replace("|FUN_NAME|",funName);
        scriptItem = scriptItem.Replace("|PATH|",path);

        var insertIndex = scriptText.LastIndexOf("#endregion");

        scriptText = $"{scriptText.Substring(0,insertIndex)}{scriptItem}\n{scriptText.Substring(insertIndex,scriptText.Length-insertIndex)}\n";
        
        File.WriteAllText(scriptPath,scriptText);
        AssetDatabase.Refresh();
    }

#region 已生成路径

#endregion
}

#endif







