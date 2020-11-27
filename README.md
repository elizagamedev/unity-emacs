# unity-emacs

Open text files in Unity in Emacs.

## Why not just use Editor Settings?

I use the Omnisharp language server with Emacs, and in some versions of Unity,
solution and project files were not being generated unless I had set Visual
Studio as my editor.

## Installation

See the Unity documentation for [Git
dependencies](https://docs.unity3d.com/Manual/upm-git.html).

```json
{
  "dependencies": {
    "sh.eliza.unity": "https://github.com/elizagamedev/unity-emacs.git#v0.1.0"
  }
}
```

## Configuration

Check `Edit -> Project Settings -> Emacs` to toggle the feature on or off,
configure the path to `emacsclient`, and select the list of file extensions you
want to use.
