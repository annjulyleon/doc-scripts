# Docx2Pdf

📜[RU](README.md)  

Converts doc/docx documents to pdf. Updates fields (optional), you can adjust the quality of pdf (for viewing or for printing). Supports updating fields in figures and captions before output to pdf.

Usage:

```console
.\docx2pdf.ps1 -dir D:\path\to\docs -opt 0 -update $false
```

`-dir` - path to folder with docx files. If not specified, executed for files in the script folder
`-opt` - 0/1 - print quality - 1 for web (small file), 0 for print. Default if not specified = 0
`-update` - true/false - true - update fields and content, false - don't update. By default and if not specified - true 

The script writes the output for each document (shows the full path) to a log file with a date. The script performs a simple check on the document and outputs:

- `WARNING` - if there is no content in the document
- `ERROR` - if the text "Error!" is found in the document. Most often, this means that a field update error was found or some property is missing in the document.

If you need to change the keyword to search for an error (for example, for another language), then change the line in the script (the word with brackets):

```powershell
$wordFound = $range.Find.Execute("Ошибка!")
```

## Sources

- [powershell script convert doc to pdf](https://social.technet.microsoft.com/Forums/ie/en-US/445b2429-e33c-4ce0-9d64-dd31422571bf/powershell-script-convert-doc-to-pdf?forum=winserverpowershell)
- [Document.ExportAsFixedFormat Method](https://docs.microsoft.com/en-us/previous-versions/office/developer/office-2007/bb256835(v=office.12))
- [Powershell: Everything you wanted to know about hashtables](https://powershellexplained.com/2016-11-06-powershell-hashtable-everything-you-wanted-to-know-about/)
- [How can I introduce a config file to Powershell scripts?](https://stackoverflow.com/a/13698982)
- [How to change custom properties for many Word documents](https://stackoverflow.com/a/35920682)
- [Powershell Update Fields in Header and Footer in Word](https://stackoverflow.com/questions/24887905/powershell-update-fields-in-header-and-footer-in-word)
