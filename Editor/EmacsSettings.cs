using System.IO;
using System;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Emacs {

  public class EmacsSettings : SettingsProvider {
    private const string _enabledName = "Emacs/Enable";
    private const string _emacsclientPathName = "Emacs/EmacsclientPath";
    private const string _fileExtensionsName = "Emacs/FileExtensions";

#if UNITY_EDITOR_WIN
    private const string _defaultEmacsclientPath =
        @"C:\Program Files\Emacs\x86_64\bin\emacsclientw.exe";
#elif UNITY_EDITOR_OSX
    private const string _defaultEmacsclientPath =
        "/Applications/Emacs.app/Contents/MacOS/bin/emacsclient";
#else
    private const string _defaultEmacsclientPath = "emacsclient";
#endif

    private const string _defaultFileExtensions =
        ".cs,.txt,.js,.javascript,.json,.html,.shader,.template,.proto";

    public static bool Enabled {
      get { return EditorPrefs.GetBool(_enabledName, true); }
      set { EditorPrefs.SetBool(_enabledName, value); }
    }

    public static string EmacsclientPath {
      get { return EditorPrefs.GetString(_emacsclientPathName, _defaultEmacsclientPath); }
      set { EditorPrefs.SetString(_emacsclientPathName, value); }
    }

    public static string FileExtensions {
      get { return EditorPrefs.GetString(_fileExtensionsName, _defaultFileExtensions); }
      set {
        EditorPrefs.SetString(_fileExtensionsName, string.Join(",", SplitFileExtensions(value)));
      }
    }

    public static IEnumerable<string> EnumerableFileExtensions {
      get { return SplitFileExtensions(FileExtensions); }
    }

    private static IEnumerable<string> SplitFileExtensions(string extensions) {
      return Regex.Split(extensions, @"\s*,\s*").Where(e => !string.IsNullOrEmpty(e));
    }

    // SettingsProvider
    public EmacsSettings() : base("Preferences/Emacs", SettingsScope.User) {}

    public override void OnGUI(string searchContext) {
      Enabled = EditorGUILayout.Toggle("Enabled", Enabled);
      EmacsclientPath = EditorGUILayout.DelayedTextField("Emacsclient Path", EmacsclientPath);
      FileExtensions = EditorGUILayout.DelayedTextField("File Extensions", FileExtensions);
    }

    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider() {
      return new EmacsSettings();
    }
  }
}
