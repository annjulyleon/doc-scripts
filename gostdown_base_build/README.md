# Gostdown+

Модифицированная версия скрипта [Gostdown](https://gitlab.iaaras.ru/iaaras/gostdown) [build.ps1](build.ps1): всегда последняя версия скрипта.

## Changelog

### 2022-12-20

- добавлен параметр `luafilter`, в котором указывается путь к папке с фильтрами;
- добавлена возможность передавать файлы в виде списка в .txt;

## Вызов

Вызов оригинального скрипта:

```bat
powershell.exe ^
-executionpolicy bypass ^
-command .\build_original.ps1 ^
-md .\docs\00_begin.md,.\docs\99_end.md ^
-template template-report.docx ^
-docx report.docx ^
-counters
```

Вызов скрипта последней версии. `docx_include.txt` -- файл со списком файлов .md в нужном порядке.

```bat
.\build.ps1 -md $(Get-Content docx_include.txt) -template template.docx -luafilter .\scripts -docx output.docx -counters -embedfonts 
```

## Использование

Пример таски для VS Code с возможностью выбора документа (папки):

```json
"tasks": [       
        {
            "label": "Build doc",
            "detail": "Build selected in docx_include.txt to build docs",
            "type": "shell",
            "options": {
                "cwd": "${workspaceFolder}/docs_gost/${input:doc}/"
            },
            "command": "${workspaceFolder}\\scripts\\build.ps1 -md $(Get-Content docx_include.txt) -template template.docx -luafilter ${workspaceFolder}\\scripts -docx ${workspaceFolder}\\build\\${input:doc}.docx -embedfonts",
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
        }
    ],
    // list of folders inside doc folder = document
    "inputs": [        
        {
            "id": "doc",
            "description": "Document Path: ",
            "type": "pickString",
            "options": [
                "dev1_guide_deploy",
                "all_pmi"
            ],
            "default": "report"
        }
    ]
```
