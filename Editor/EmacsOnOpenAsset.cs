using System.IO;
using System.Text.RegularExpressions;
using System;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;

namespace Emacs {

  public class EmacsOnOpenAsset : MonoBehaviour {
#if ENABLE_VSTU
    [MenuItem("Emacs/Regenerate Project Files")]
    public static void RegenerateProjectFiles() {
      SyntaxTree.VisualStudio.Unity.Bridge.ProjectFilesGenerator.GenerateProject();
    }
#endif

#if UNITY_2019_2_OR_NEWER
    [OnOpenAssetAttribute()]
#endif
    public static bool OnOpenAsset(int instanceID, int line, int column) {
      if (!EmacsSettings.Enabled) {
        return false;
      }

      UnityEngine.Object selectedObject = EditorUtility.InstanceIDToObject(instanceID);
      string filePath = AssetDatabase.GetAssetPath(selectedObject);

      if (!Regex.Match(filePath, EmacsSettings.FileMatchPattern, RegexOptions.IgnoreCase).Success) {
        return false;
      }

      if (line == -1) {
        line = 0;
      }
      if (column == -1) {
        column = 0;
      }
      string projectPath = System.IO.Path.GetDirectoryName(UnityEngine.Application.dataPath);
      string completeFilePath = Path.Combine(projectPath, filePath);

      try {
        using (System.Diagnostics.Process proc = new System.Diagnostics.Process()) {
          proc.StartInfo.FileName = EmacsSettings.EmacsclientPath;
          proc.StartInfo.Arguments = String.Format("-n --alternate-editor= +{0}:{1} \"{2}\"", line,
                                                   column, completeFilePath);
          proc.StartInfo.UseShellExecute = false;
          proc.Start();
        }
      } catch (Exception) {
        Debug.LogError("Could not start Emacs. Check your Preferences.");
      }
      return true;
    }

#if !UNITY_2019_2_OR_NEWER
    [OnOpenAssetAttribute()]
    public static bool OnOpenAsset(int instanceID, int line) {
      return OnOpenAsset(instanceID, line, -1);
    }
#endif
  }

}
