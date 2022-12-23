# Генератор одностраничного HTML

[ENG](README_ENG.md)

Скрипт генерирует одностраничный html файл с помощью команды pandoc с параметрами.  

Требования: pandoc >2.17, pandoc-crossref

Пример команды:

```console
.\create_simple_html.ps1 mdpath template gdfilter
```

где:

- `mdpath`: путь к папке с .md файлами. В этой папке должен быть файл `html_include.txt`, в котором на каждой строке перечислены md файлы для сборки html в указанном порядке;
- `template`: название шаблона без расширения. Рекомендую [easy_template](https://github.com/ryangrose/easy-pandoc-templates);
- `gdfilter`: путь к папке с фильтрами. Сами фильтры заданы в скрипте (фильтры `include_files.lua`, `linebreaks.lua` and `metadata_processor.lua`, см [здесь](../pandoc_filters/)).

## Использование

Пример task для VS Code для вызова скрипта для текущей директории. Пользователь может выбрать шаблон.

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
