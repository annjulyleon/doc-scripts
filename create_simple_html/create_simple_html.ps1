<#
.Description
	This script run pandoc command with arguments. 
.Example
	.\create_simple_html.ps1 mdpath template gdfilter
.Positional Arguments
 mdpath - path to document folder with html_include.txt. html_include.txt stores list of .md files to pass to pandoc 
 template - template used for html output
 gdfilter - path to folder with filters. Filters are hardcoded.
#>

$mdpath = $args[0]
$template = $args[1]
$gdfilter = $args[2]
pandoc -s $(Get-Content $mdpath\html_include.txt) -f markdown --quiet --template=$template --lua-filter=$gdfilter\include_files.lua --lua-filter=$gdfilter\linebreaks.lua --lua-filter=$gdfilter\metadata_processor.lua --filter pandoc-crossref --citeproc -o index.html --toc