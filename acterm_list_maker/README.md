# Генератор списков сокращений и определений

Скрипт генерирует из списка сокращений и терминов, переданных в plain text, отформатированный список (таблицу) с расшифровкой сокращений и с определениями по словарю.

**Требования:** `pip install chardet` для работы с кодировкой файла.  

Алгоритм:

1. Скрипт берет файл .txt со списком терминов или сокращений и кодирует его в UTF-8 (если он в какой-то другой кодировке).
2. Для каждого из переданных в аргументах командной строки словаря:
   1. Для каждого слова (строки) из .txt проверяет, есть ли слово в словаре.
   2. Если слово есть, добавляет его в отдельный временный список, удаляет добавленное слово из списка на проверку (это сделано, что термины не дублировались).
3. После обработки всех словарей, если в списке еще остались слова, добавляет из во временный список с пустыми definitions.
4. Результат записывает в указанный файл.
5. Процесс работы скрипта логируется в файл, по умолчанию уровень INFO.

Пример запуска для обработки `abbreviations.txt`:

```bash
# Help
python acterm_list_maker.py --help
# Запуск для формирования списка в формате таблицы
python acterm_list_maker.py -i "abbreviations.txt" -d "acronyms/common.dic" -o "abbreviations.md" -f gd
```

Пример запуска для обработки `glossary.txt`:

```bash
python acterm_list_maker.py -i "glossary.txt" -d "glossary/common.dic,glossary/ml.dic" -o "glossary.md" -f gd
```

## Input

Формат входного файла: термин или сокращение на строке, например:

```text
ПО
АРМ
ML
ВМ
```

## Словари

Предусмотрено несколько словарей (в аргументе можно передать список). Каждый словарь - `dictionary`, где `key` - термин, `value` - определение:

```json
{
    "АРМ": "автоматизированное рабочее место",
    "БД": "база данных",
    "ВМ": "виртуальная машина" 
}
```

```json
{
    "маппинг": "определение соответствия данных между потенциально различными семантиками одного объекта или разных объектов" 
}
```

Поиск выполняется по строгому соответствию по ключам (регистр НЕ игнорируется, т.е. "маппинг" и "Маппинг" будут разными словами).

## Workflow

1. В тексте выделяем нужный текст сокращения, нажимаем `Ctrl` + `Shift` + `A` (acronym).
2. Для быстрого поиска аббревиатур по тексту используем регулярное выражение: `\w*[A-ZА-Я][A-ZА-Я\d]+\w*`.
3. Выделенный текст добавляется в файл `abbreviations.txt` в директории.
4. В тексте выделяем нужный термин, нажимаем `Ctrl` + `Shift` + `T` (term).
5. Выделенный текст добавляется в файл `glossary.txt` в директории.
6. Повторяем 1-4 для всех нужных сокращений и терминов. Получается два файла.
7. Файлы можно упорядочить по алфавиту в VScode (Выделить весь текст -> Ctrl + Shift + p -> Sort Lines Asc...).
8. Дубликаты можно удалить в VScode (Выделить весь текст -> Ctrl + Shift + p -> Delete Duplicated lines...).
9. Запускаем скрипт `acterm_list_maker.py` с параметрами для терминов или сокращений, в параметрах также указываем словари.

Словарей может быть несколько, рекомендуемый порядок применения: словарь проекта, специальный словарь (например, ml.dic - термины машинного обучения), общий словарь.
Получаем на выходе файл `abbreviations.md` или `glossary.md` со списком сокращений/расшифровок и терминов/определений в формате таблицы markdown без шапки.

## Настройка VS Code

В VSCode в `tasks.json` проекта добавляем две таски.
`"cwd": "${workspaceFolder}"` можно заменить на `"cwd": "${fileDirname}"`, тогда `.txt` будут сохранятся в директорию с тем файлом, из которого добавляются сокращения или термин.

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

В VScode идем в File -> Preferences -> Keyboard Shortcuts. Откроется список горячих клавиш.
В правом верхнем углу нажать маленькую кнопку Open Keyboard Shortcuts (JSON).
Откроется JSON с пользовательскими кнопками. Добавить в файл в массив кнопки:

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

Все, теперь в VSCode термины и сокращения можно добавлять в отдельный файл.

## Задача VS Code

Пример для сокращений. Скрипт будет выполнен для `.txt`, который находится в текущей открытой директории.

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
