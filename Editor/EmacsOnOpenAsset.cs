using System.IO;
using System;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Emacs {

  public class EmacsOnOpenAsset : MonoBehaviour {
#if ENABLE_VSTU
    [MenuItem("Emacs/Regenerate Project Files")]
    public static void RegenerateProjectFiles() {
      SyntaxTree.VisualStudio.Unity.Bridge.ProjectFilesGenerator.GenerateProject();
    }
#endif

    [OnOpenAssetAttribute()]
    public static bool OnOpenAsset(int instanceID, int line, int column) {
      if (!EmacsSettings.Enabled) {
        return false;
      }

      UnityEngine.Object selectedObject = EditorUtility.InstanceIDToObject(instanceID);
      string filePath = AssetDatabase.GetAssetPath(selectedObject);

      string fileExtension = Path.GetExtension(filePath);
      if (string.IsNullOrEmpty(fileExtension)) {
        return false;
      }
      if (!EmacsSettings.EnumerableFileExtensions.Contains(fileExtension.ToLower())) {
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
          proc.StartInfo.Arguments =
              $"-n --alternate-editor= +{line}:{column} \"{completeFilePath}\"";
          proc.StartInfo.UseShellExecute = false;
          proc.Start();
        }
      } catch (Exception) {
        Debug.LogError("Could not start Emacs. Check your Preferences.");
      }
      return true;
    }
  }

}
