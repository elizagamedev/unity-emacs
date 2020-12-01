using UnityEditor;

namespace Emacs {

  public static class EmacsSettings {
    private const string _enabledName = "Emacs/Enable";
    private const string _emacsclientPathName = "Emacs/EmacsclientPath";
    private const string _fileMatchPatternName = "Emacs/FileMatchPattern";

#if UNITY_EDITOR_WIN
    private const string _defaultEmacsclientPath =
        @"C:\Program Files\Emacs\x86_64\bin\emacsclientw.exe";
#elif UNITY_EDITOR_OSX
    private const string _defaultEmacsclientPath =
        "/Applications/Emacs.app/Contents/MacOS/bin/emacsclient";
#else
    private const string _defaultEmacsclientPath = "emacsclient";
#endif

    private const string _defaultFileMatchPattern =
        "(.cs|.txt|.js|.javascript|.json|.html|.shader|.template|.proto|.xml)$";

    public static bool Enabled {
      get { return EditorPrefs.GetBool(_enabledName, false); }
      set { EditorPrefs.SetBool(_enabledName, value); }
    }

    public static string EmacsclientPath {
      get { return EditorPrefs.GetString(_emacsclientPathName, _defaultEmacsclientPath); }
      set { EditorPrefs.SetString(_emacsclientPathName, value); }
    }

    public static string FileMatchPattern {
      get { return EditorPrefs.GetString(_fileMatchPatternName, _defaultFileMatchPattern); }
      set { EditorPrefs.SetString(_fileMatchPatternName, value); }
    }

#if UNITY_2018_3_OR_NEWER
    private class EmacsSettingsProvider : SettingsProvider {
      public EmacsSettingsProvider() : base("Preferences/Emacs", SettingsScope.User) {}

      public override void OnGUI(string searchContext) {
        OnGUI();
      }

      [SettingsProvider]
      public static SettingsProvider CreateSettingsProvider() {
        return new EmacsSettings();
      }
    }
#else
    [PreferenceItem("Emacs")]
#endif
    private static void OnGUI() {
      Enabled = EditorGUILayout.Toggle("Enabled", Enabled);
      EmacsclientPath = EditorGUILayout.TextField("Emacsclient Path", EmacsclientPath);
      FileMatchPattern = EditorGUILayout.TextField("File Extensions", FileMatchPattern);
    }
  }

}
