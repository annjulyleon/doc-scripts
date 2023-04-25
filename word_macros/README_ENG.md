# Microsoft Word Macros

Here is a VBA sheet with macros for Word to post-process gostdown generated docx.

1. Macros `FigCapAutoNum` and `TblCapAutoNum` search the document for text like `Рисунок N - Название` and `Таблица N - Название` and replace plain text with auto-numbered field. You can change russian search and replace text.
2. The `FigReferenceAutoInsert` and `TblReferenceAutoInsert` macros search the document for text of the form `(рисунок N)` and `(таблица N)` and replace the text in brackets with a link to objects automatically numbered in step 1.
3. The `TableHeaderFix()` macros center aligns the contents of the first row of tables with the specified styles.

Macros are called manually from Word > Developer > Macros.
After inserting into VBA **CHECK** encoding of Russian names for Figures and Tables and adjust as necessary.

Microsoft 365 and Microsoft 2021 format HYPERLINK fields differently. Therefore, the macros needs to be slightly adjusted depending on the version (macros `FigReferenceAutoInsert()`, `TblReferenceAutoInsert()`).

Make sure the code `If Mid(oField.Code, 15, 4) = "tbl:"` returns exactly `tbl:` or `fig:` (without quotation marks).
In different versions of Word, the length of the field unfortunately changes. On a PC running Microsoft Word 2021 and Pandoc 3.1, the correct code is `Mid(oField.Code, 16, 4)`

To debug this use `Debug.Print Mid(oField.Code, 15, 4)`.
Ctrl + G in the VBA editor will display the debug window for output.

Call macros automatically via PowerShell script:

```powershell
$application = New-Object -ComObject word.application
$application.Visible = $false
$doc = $application.Documents.Open($file)
$application.Run("FigCapAutoNum")
```

## CHANGELOG

### 2023-04-25

- added: error handling for error 5560 (pattern error due to regional settings)
- changed: general refactoring
