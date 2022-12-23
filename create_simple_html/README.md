# MD to One page html

[RU](README_RU.md)

The script generates a single page html file using the pandoc command with parameters.

Requirements: pandoc >2.17, pandoc-crossref

Example:

```console
.\create_simple_html.ps1 mdpath template gdfilter
```

- `mdpath`: path to the folder with .md files. This folder should contain a `html_include.txt` file, which lists the md files on each line to build the html in that order;
- `template`: template name without extension. I recommend [easy_template](https://github.com/ryangrose/easy-pandoc-templates);
- `gdfilter`: path to the folder with filters. The filters themselves are set in the script (default is `include_files.lua`, `linebreaks.lua` and `metadata_processor.lua`, see [pandoc_filters](../pandoc_filters/)).

## Use case for VS Code

An example task for VS Code to call a script for the current directory. The user can select a template.

```json
    "tasks": [
        {
            "label": "Make html",
            "detail": "Create simple html from md files in html_include.txt in current dir",
            "type": "shell",
            "command": "${workspaceFolder}\\scripts\\create_simple_html.ps1 ${fileDirname} ${workspaceFolder}\\scripts\\${input:html_template}.html ${workspaceFolder}\\scripts\\",
            "options": {
                "cwd": "${fileDirname}"
            },
            "problemMatcher": []
        }
    ],
    "inputs": [        
        {
            "id": "html_template",
            "description": "Html Template: ",
            "type": "pickString",
            "options": [
                "easy_template",
                "easy_template_local"
            ],
            "default": "easy_template"
        }
    ]
```
