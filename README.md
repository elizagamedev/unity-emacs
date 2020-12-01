# unity-emacs

Open text files in Unity in Emacs.

Note that after you install this package, it will be **disabled** by default.
This is so that you do not inadvertently break all of your team members'
workflows by installing this package. See [Configuration](#configuration).

## Why not just use Editor Settings?

I use the Omnisharp language server with Emacs, and in some versions of Unity,
solution and project files were not being generated unless I had set Visual
Studio as my editor.

## Installation

### Unity 2019.x and Later

See the Unity documentation for [Git
dependencies](https://docs.unity3d.com/Manual/upm-git.html).

```json
{
  "dependencies": {
    "sh.eliza.unity": "https://github.com/elizagamedev/unity-emacs.git#v1.0.0"
  }
}
```

### Older Unity Versions

Simply clone or download the repo and place it in your project's Assets folder.

```shell
$ pwd
/some/unity/project/Assets

$ git clone https://github.com/elizagamedev/unity-emacs.git#v1.0.0
```

## Configuration

Check `Edit -> Project Settings -> Emacs` to toggle the feature on or off,
configure the path to `emacsclient`, and select the list of file extensions you
want to use.
