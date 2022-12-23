# Structure creator

Скрипт для создания структуры папок и файлов .md для научно-технических отчетов. Структура описывается в файле `config.json`.

```text
python .\structure_creator.py 
```

1. В `config.json` указаны названия верхних директорий `folders` > `name`.  
2. Далее для каждой директории список файлов (`top_files` > `file_name`). Для файла можно указать контент, который туда запишется.  
3. В параметре `devices` указан список устройств. На каждый `top_files` будет создано количество `devices` файлов.  
4. Плюс создается стандартный файл с Приложением.

Структура в результате примерно такая с точки зрения отчета:

```text
Раздел ТЗ < папка 01_nameoffolder1
Подпункт 1 < файл 0100_ver.md
Устройство 1 < файл 0101_ver_device1.md
Устройство 2 < файл 0102_ver_device2.md
... Устройство N
Раздел 2 ТЗ
Раздел 3 ТЗ
Приложение
```