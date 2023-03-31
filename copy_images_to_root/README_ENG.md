# copy_img

📜[RU](README.md)  

Script to copy pictures from child folders to one root folder.
The script creates a list of copied files in json.
The copied files can be deleted by calling the script with the `-r` command. Only copied files will be deleted, files that were already in the root folder will not be deleted.

Command:

```bat
python .\copy_img.py -s ".\docs" -d ".\img" -c
```

Command line arguments:

`-s` -- directory to process. You can specify the root, for example, `.`;
`-d` -- destination folder inside root where to pictures will be copied;
`-c` -- flag, if specified, copy will be performed;
`-r` -- flag, if specified, then files copied earlier will be deleted.