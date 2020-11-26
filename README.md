# unity-emacs

Open text files in Unity in Emacs.

## Why not just use Editor Settings?

I use the Omnisharp language server with Emacs, and in some versions of Unity,
solution and project files were not being generated unless I had set Visual
Studio as my editor.

## Configuration

Check `Edit -> Project Settings -> Emacs` to toggle the feature on or off,
configure the path to `emacsclient`, and select the list of file extensions you
want to use.
