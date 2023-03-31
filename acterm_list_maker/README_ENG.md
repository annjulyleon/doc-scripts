# Abbreviations and Definitions List Generator

📜[RU](README.md)  

The Python script generates a formatted list (or table) from a list of abbreviations and terms passed as a plain text, with a  abbreviations in full form and term definitions.

**Requirements:** `pip install chardet` to work with file encoding.

Example for abbreviations list `abbreviations.txt`:

```bash
# Help
python acterm_list_maker.py --help
# Generate abbreviations table
python acterm_list_maker.py -i "abbreviations.txt" -d "acronyms/common.dic" -o "abbreviations.md" -f gd
```

Example to generate glossry list `glossary.txt`:

```bash
python acterm_list_maker.py -i "glossary.txt" -d "glossary/common.dic,glossary/ml.dic" -o "glossary.md" -f gd
```

## Input

Input file has a term or an abbreviation per line:

```text
ПО
АРМ
ML
ВМ
```

## Dictionaries

Several dictionaries are provided (you can pass a list in the argument). Each dictionary is a python `dictionary`, where `key` is a term, `value` is a definition:

```json
{
     "ARM": "workstation",
     "db": "database",
     "VM": "virtual machine"
}
```

```json
{
     "mapping": "determining the correspondence of data between potentially different semantics of the same object or different objects"
}
```

The search is performed by strict key matching (case is NOT ignored, i.e. "mapping" and "Mapping" will be different words).

## Workflow

1. Select the abbreviation text, press `Ctrl` + `Shift` + `A` (acronym).
2. To quickly search for abbreviations in the text, use the regular expression: `\w*[A-Z][A-Z\d]+\w*`.
3. The selected text is added to the `abbreviations.txt` file in the directory.
4. Select the term, press `Ctrl` + `Shift` + `T` (term).
5. The selected text is added to the `glossary.txt` file in the directory.
6. Repeat 1-4 for all necessary abbreviations and terms. 
7. Lines in the files can be sorted alphabetically in VScode (Select all text -> Ctrl + Shift + p -> Sort Lines Asc...).
8. Duplicated lines can be deleted in VScode (Select all text -> Ctrl + Shift + p -> Delete Duplicated lines...).
9. Run the script `acterm_list_maker.py` with parameters for terms or abbreviations, specify dictionaries in the parameters.

There can be several dictionaries, the recommended order of use is: project dictionary, special dictionary (for example, ml.dic - machine learning terms), general dictionary.
Output file `abbreviations.md` or `glossary.md` will contain a list of abbreviations / full forms and terms / definitions.

## VS Code Setup

In VSCode, add two tasks to `tasks.json` of the project.
`"cwd": "${workspaceFolder}"` can be replaced with `"cwd": "${fileDirname}"`, then `.txt` will be saved in the directory with the file from which the abbreviations or term are added.

```json
{
            "label": "add_acronym",
            "type": "shell",
            "options": {
                "cwd": "${workspaceFolder}" 
            },
            "command": "echo '${selectedText}' >> abbreviations.txt",
            "presentation": {
                "echo": false,
                "reveal": "silent",
                "focus": false,
                "panel": "shared",
                "showReuseMessage": false,
                "clear": true,
                "close": true
            },
            "promptOnClose": false,
            "group": {
                "kind": "none",
                "isDefault": false
            },
            "problemMatcher": []
        },
        {
            "label": "add_term",
            "type": "shell",
            "options": {
                "cwd": "${workspaceFolder}" 
            },
            "command": "echo '${selectedText}' >> glossary.txt",
            "presentation": {
                "echo": false,
                "reveal": "silent",
                "focus": false,
                "panel": "shared",
                "showReuseMessage": false,
                "clear": true,
                "close": true
            },
            "promptOnClose": false,
            "group": {
                "kind": "none",
                "isDefault": false
            },
            "problemMatcher": []
        }
```

In VScode go to File -> Preferences -> Keyboard Shortcuts. A list of hotkeys will open.
In the upper right corner, click the small Open Keyboard Shortcuts (JSON) button.
JSON will open with custom hotkeys, add new hotkeys as follows:

```json
{
        "key": "ctrl+shift+a",
        "command": "workbench.action.tasks.runTask",
        "when": "editorTextFocus",
        "args": "add_acronym" 
    },
    {
        "key": "ctrl+shift+t",
        "command": "workbench.action.tasks.runTask",
        "when": "editorTextFocus",
        "args": "add_term" 
    }
```

## Task VS Code

Example for abbreviations. The script will be executed for `.txt`, which is in the current open directory.

```json
{
            "label": "Make acronyms list",
            "detail": "Makes abbreviation list in table format",
            "type": "shell",
            "options": {
                "cwd": "${fileDirname}"
            },
            "command": "python ${workspaceFolder}\\scripts\\acterm_list_maker.py -i ${fileDirname}\\abbreviations.txt -d \"${workspaceFolder}\\scripts\\acronyms\\common.dic\" -o abbreviations.md",
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": false,
                "panel": "shared",
                "showReuseMessage": false,
                "clear": true
            },
            "group": {
                "kind": "build",
                "isDefault": false
            }
        },
```
