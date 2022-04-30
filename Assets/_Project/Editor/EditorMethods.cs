using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class EditorMethods
{

    [MenuItem("Tools/Open data folder")]
    static void OpenDataFolder()
    {
        EditorUtility.RevealInFinder(Path.Combine(Application.persistentDataPath, "Saves"));
    }

    [MenuItem("Tools/Clear PlayerPrefs")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/Build/Open build folder", false, 0)]
    static void OpenBuildFolder()
    {
        EditorUtility.RevealInFinder(Path.Combine(Application.dataPath, "..", "Build"));
    }

}
