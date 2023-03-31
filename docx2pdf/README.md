# Скрипт Docx2Pdf

📜[ENG](README_ENG.md)  

ENG: Convert .docx and .doc to pdf + update fields (optional), including fields in headers and footers.

Конвертирует документы doc/docx в pdf. Обновляет поля (опционально), можно настраивать качество pdf (для просмотра или для печати). Умеет обновлять перед выводом в pdf поля в фигурах и надписях.

Использование:

```console
.\docx2pdf.ps1 -dir D:\path\to\docs -opt 0 -update $false
```

`-dir` - путь к папке с docx файлами. Если не указан, то выполняется для файлов в папке скрипта  
`-opt` - 0/1 - качество вывода на печать - 1 для веба (маленький файл), 0 на печать. По умолчанию, если не указан = 0  
`-update` - true/false - true - обновлять поля и содержание, false - не обновлять. По умолчанию и если не указано - true  

Скрипт пишет вывод для каждого документа (показывает полный путь) в  лог-файл с датой. Скрипт выполняет простейшую проверку документа и  выводит:

- WARNING - если в документе нет содержания (нормальная ситуация для спецификаций, ведомостей и пр.)
- ERROR - если в документе обнаружен текст "Ошибка!". Чаще всего это означает, что найдена ошибка обновления поля или отсутствует какое-то  свойство в документе

Если требуется изменить ключевое слово для поиска ошибки (например, для другого языка), то изменить в скрипте строку (слово с скобках):

```powershell
$wordFound = $range.Find.Execute("Ошибка!")
```

## Источники

- [powershell script convert doc to pdf](https://social.technet.microsoft.com/Forums/ie/en-US/445b2429-e33c-4ce0-9d64-dd31422571bf/powershell-script-convert-doc-to-pdf?forum=winserverpowershell)
- [Document.ExportAsFixedFormat Method](https://docs.microsoft.com/en-us/previous-versions/office/developer/office-2007/bb256835(v=office.12))
- [Powershell: Everything you wanted to know about hashtables](https://powershellexplained.com/2016-11-06-powershell-hashtable-everything-you-wanted-to-know-about/)
- [How can I introduce a config file to Powershell scripts?](https://stackoverflow.com/a/13698982)
- [How to change custom properties for many Word documents](https://stackoverflow.com/a/35920682)
- [Powershell Update Fields in Header and Footer in Word](https://stackoverflow.com/questions/24887905/powershell-update-fields-in-header-and-footer-in-word)
