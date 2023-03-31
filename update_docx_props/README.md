# Update Docx Props

📜[ENG](README_ENG.md)

Скрипт PowerShell для добавления и обновления свойств в документах .docx. При сохранении обновляет поля, в том числе в колонтитулах. Свойства берет из конфигурационного файла .xml.

Запуск:

```console
.\update_docx_props.ps1 -dir D:\path\to\docs -conf D:\path\to\config\config.xml [-filename] testwordfile.docx
```

- `-dir` -- директория с файлами docx, по умолчанию -- текущая;
- `-conf` -- путь к конфигурационному файлу, по умолчанию `$dir/config.xml`;
- `-filename` -- опционально, имя файла. Если указано, то скрипт выполнится только для этого файла в `$dir`. Версия с `filename` в [update_docx_props_single](update_docx_props_single).

## V5 - Macro

[v5 - Macro](update_docx_props_macro)

Добавлена возможность в скрипте вызывать макросы Microsoft Word.  
Список макросов задается в условии `if ($callmacro)`.  
См. [макросы](https://github.com/annjulyleon/doc-scripts/tree/main/word_macros).

```console
.\UpdateDocxPropsConfig_v3.ps1 -dir . -conf config.xml -callmacro:$true [-filename]
```

- `-dir` -- директория для документов
- `-conf` -- путь к конфигурационному файлу
- `-callmacro` -- переключатель для вызова макросов, например, `callmacro:$true`
- `-filename` -- опционально, имя файла. Если указано, то скрипт выполнится только для этого файла в `$dir`

## V4 - Single

- добавлена версия скрипта (см. [update_docx_props_single](update_docx_props_single)) с возможностью запуска для одного файла.

## v3

- теперь скрипт умеет обновлять встроенные свойства в Visio файлах
- обновляет встроенные свойства в Word (Теги, Примечания, Тема, Руководитель...)
- скрипт обновляет поля внутри форм (shapes) и надписей
- изменен формат конфигурационного файла (секции для vsd и встроенных свойств Word)

Структура файлов:

- `config.xml` - конфигурационный файл. Теперь включает три секции: `customProperties` для кастомных свойств Word, `builtinProperties` - встроенные свойства Word, `vsdProperties` - встроенные Visio свойства. Пример:
  
    ```xml
    <?xml version="1.0"?>
    <configuration>
      <customProperties>    
        <add key="property1" value="new value for the property"/>
      </customProperties>
      <builtinProperties>
        <add key="Title" value="This is Title property"/>
        <add key="Subject" value="This is Subject property"/>
        <add key="Keywords" value="some tag more tag"/>
        <add key="Comments" value="somecomment"/>
      </builtinProperties>
      <vsdProperties>
        <add key="Company" value="LLC COMPANY"/>
        <add key="Category" value="Category of the document"/>
        <add key="Title" value="Title of the document"/>
        <add key="Subject" value="Subject of the document"/>
        <add key="Keywords" value="Some tags"/>
        <add key="Description" value="Desc comment"/>
        <add key="Manager" value="Project Manager"/>
      </vsdProperties>
    </configuration>
    ```

Если какие-то свойства или вся секция не нужны, то просто удалите все свойства в секции.

```xml
    <?xml version="1.0"?>
    <configuration>
      <customProperties>    
        <add key="property1" value="new value for the property"/>
      </customProperties>
      <builtinProperties>        
      </builtinProperties>
      <vsdProperties>        
      </vsdProperties>
    </configuration>
```

## Источники

- [Powershell: Everything you wanted to know about hashtables](https://powershellexplained.com/2016-11-06-powershell-hashtable-everything-you-wanted-to-know-about/)
- [How can I introduce a config file to Powershell scripts?](https://stackoverflow.com/a/13698982)
- [How to change custom properties for many Word documents](https://stackoverflow.com/a/35920682)
- [Powershell Update Fields in Header and Footer in Word](https://stackoverflow.com/questions/24887905/powershell-update-fields-in-header-and-footer-in-word)
