# Макрос для Word

📜[ENG](README_ENG.md)

Здесь выложен лист VBA с макросами для Word.

1. Макросы `FigCapAutoNum` и `TblCapAutoNum` при запуске ищут по документу текст вида `Рисунок N - Название` и `Таблица N - Название` и заменяют простой текст на поле с автонумерацией.
2. Макросы `FigReferenceAutoInsert` и `TblReferenceAutoInsert` при запуске ищут по документу текст вида `(рисунок N)` и `(таблица N)` и заменяют текст в скобках на ссылку на автоматически пронумерованные на шаге 1 объекты.
3. Макрос `TableHeaderFix()` выравнивает по центру содержимое первой строки таблиц с указанными стилями.

Макросы вызываются вручную в Word.
После вставки в VBA **ПРОВЕРЬТЕ** кракозябры (кодировку) русских названий Рисунок и Таблица.

Microsoft 365 и Microsoft 2021 по разному формируют поля HYPERLINK. Поэтому макрос нужно будет нужно немного отредактировать в зависимости от версии (макросы `FigReferenceAutoInsert()`, `TblReferenceAutoInsert()`).

В коде `If Mid(oField.Code, 15, 4) = "tbl:"` Then нужно убедиться, что `Mid(oField.Code, 15, 4)` возвращает именно `tbl:` или `fig:` (без кавычек). В разных версиях ворд длина поля к сожалению меняется (в старых версиях в полях есть пробел, в новых - нет). На машине с Microsoft Word 2021 правильный код `Mid(oField.Code, 16, 4)`

Чтобы отследить это, можно использовать `Debug.Print Mid(oField.Code, 15, 4)`. Ctrl + G в редакторе VBA отобразит окно отладки для вывода.

Макросы вызываются вручную в Word или автоматически в скрипте PowerShell:

```powershell
$application = New-Object -ComObject word.application
$application.Visible = $false
$doc = $application.Documents.Open($file)
$application.Run("FigCapAutoNum")
```

## CHANGELOG

### 2023-04-25

- added: обработка ошибки `5560: The find what text contains an expression that is not valid` при разных региональных настройках. Теперь макрос содержит оба варианта List Separator (`;` и `,`).
