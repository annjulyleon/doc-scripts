# GitLab Global TOC Maker

Adapted version of the script: [darrida](https://github.com/darrida/md_file_tree.py).
All headers other than level 1 are removed from the script, indents is corrected, and the default encoding is set to UTF-8. Additionaly it generated last edited date.

Адаптированная версия скрипта: [darrida](https://github.com/darrida/md_file_tree.py).  
Из скрипта убраны все заголовки 2 и далее уровней из файла, исправлены отступы и кодировка по умолчанию установлена UTF-8.

Запускается из корня проекта wiki Gitlab (или любого корня с .md файлами).
Записывает в `global_toc.md` перечень страниц Виви по уровням с названием.

Скрипт сломается если в директории есть файлы без заголовка первого уровня.

Запуск:

```text
python .\global_toc_maker.py
```
