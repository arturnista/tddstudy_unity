using UnityEditor; 
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace UnityToolbarExtender.Helper
{

    [InitializeOnLoad]
    public class SceneSwitchLeftButton
    {
        static SceneSwitchLeftButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

			if (GUILayout.Button(new GUIContent("+B", "Start Boot"), EditorStyles.miniButton, GUILayout.Width(30f)))
            {
                SceneHelper.StartScene("Assets/_Project/_Scenes/Menu.unity");
            }
			if (GUILayout.Button(new GUIContent(" G", "Load game scene"), EditorStyles.miniButton, GUILayout.Width(30f)))
            {
                SceneHelper.OpenScene("Assets/_Project/_Scenes/Game.unity");
            }
			if (GUILayout.Button(new GUIContent("+G", "Start scene"), EditorStyles.miniButton, GUILayout.Width(30f)))
            {
                SceneHelper.StartScene("Assets/_Project/_Scenes/Game.unity");
            }
        }
    }

    #region helper

    static class SceneHelper
	{
		static string[] s_ScenesToOpen;
		static bool s_ShouldPlay;
		static bool s_IsAdditive;

		public static void OpenAdditiveScene(params string[] scenes)
		{
			s_ShouldPlay = false;
			s_IsAdditive = true;
			if (EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			s_ScenesToOpen = scenes;
			EditorApplication.update += OnUpdate;
		}

		public static void OpenScene(params string[] scenes)
		{
			s_ShouldPlay = false;
			s_IsAdditive = false;
			if (EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			s_ScenesToOpen = scenes;
			EditorApplication.update += OnUpdate;
		}

		public static void StartScene(params string[] scenes)
		{
			s_ShouldPlay = true;
			s_IsAdditive = false;
			if (EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			s_ScenesToOpen = scenes;
			EditorApplication.update += OnUpdate;
		}

		static void OnUpdate()
		{
			if (s_ScenesToOpen == null || s_ScenesToOpen.Length == 0 ||
				EditorApplication.isPlaying || EditorApplication.isPaused ||
				EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				EditorApplication.update -= OnUpdate;
				return;
			}

			EditorApplication.update -= OnUpdate;

			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				var openMode = OpenSceneMode.Single;
				if (s_IsAdditive) openMode = OpenSceneMode.Additive;
				
				EditorSceneManager.OpenScene(s_ScenesToOpen[0], openMode);
				for (int i = 1; i < s_ScenesToOpen.Length; i++)
				{
					var item = s_ScenesToOpen[i];
					EditorSceneManager.OpenScene(item, OpenSceneMode.Additive);
				}
				
				EditorApplication.isPlaying = s_ShouldPlay;
			}
			s_ScenesToOpen = null;
		}
	}
    #endregion

}
