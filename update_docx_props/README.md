# Update Docx Props

Скрипт PowerShell для добавления и обновления свойств в документах .docx. При сохранении обновляет поля, в том числе в колонтитулах. Свойства берет из конфигурационного файла .xml.

Запуск:

```console
.\update_docx_props.ps1 -dir D:\path\to\docs -conf D:\path\to\config\config.xml
```

Использование:

1. скопировать файлы в директорию, в которой лежат файлы для изменения (doc, docx)
2. поменять значения свойств в `config.xml` на нужные
3. запустить файл `updateProps.bat` от администратора. Если используется русский язык, то раскомментировать строку `chcp 1251` (убрать REM)
4. дождаться пока исчезнет окно консоли
5. проверить лог-файл на ошибки

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

- `updateProps.bat` запускает скрипт для текущей и дочерних директорий (за исключением директорий, указанных в `exclude` скрипта. Если не нужно запускать скрипт для vsd файлов, просто закомментируйте эту строку в .bat:

    ```bat
    Powershell.exe -noprofile -executionpolicy bypass -File update_vsd_props.ps1 > %CurrentDateTime%_vsdprops.txt
    ```

- `update_docx_props.ps1` -- скрипт для обновления doc/docx свойств;

- `update_vsd_props.ps1` -- обновляет vsd свойства.

Измените конфигурационный файл и запустите скрипт с помощью `.bat`. Оба скрипта имеют одинаковые параметры:`-dir` (по умолчанию - текущая директория) и `-conf` (по умолчанию - файл `config,xml`в текущей директории).

## Источники

- [Powershell: Everything you wanted to know about hashtables](https://powershellexplained.com/2016-11-06-powershell-hashtable-everything-you-wanted-to-know-about/)
- [How can I introduce a config file to Powershell scripts?](https://stackoverflow.com/a/13698982)
- [How to change custom properties for many Word documents](https://stackoverflow.com/a/35920682)
- [Powershell Update Fields in Header and Footer in Word](https://stackoverflow.com/questions/24887905/powershell-update-fields-in-header-and-footer-in-word)
