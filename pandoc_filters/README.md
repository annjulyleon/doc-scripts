# Фильтры Pandoc

[Фильтры Lua](https://pandoc.org/lua-filters.html) для Pandoc.

- `metadata_processor.lua`: переменные в тексте вида `${var-name}`. Значение переменной устанавливается в frontmatter yaml. Автор скрипта: [cdivita](https://github.com/cdivita/pandoc-curly-switch);
- `include_files,lua`: единый источник для markdown, стандартный фильтр из [репозитория pandoc](https://github.com/pandoc/lua-filters/tree/master/include-files);
- `linebreaks.lua`: унифицирует разрывы строки, [iaaras](https://gitlab.iaaras.ru/iaaras/gostdown).

Фильтры передаются команде pandoc в аргументе `lua-filter`:

```console
pandoc --lua-filter=include_files.lua --lua-filter=linebreaks.lua --lua-filter=metadata_processor.lua -f markdown MANUAL.txt
```

Фильтры выполняются последовательно в указанном в команде порядке. В качестве входа в каждый фильтр передается результат выполнения предыдущего фильтра. Поэтому порядок выполнения фильтров важен. `metadata_processor.lua` должен выполняться после `include_files.lua`.

## Использование

Фильтры встроены в скрипты вызова pandoc (`build.ps1`, если используется [Gostdown](https://gitlab.iaaras.ru/iaaras/gostdown), [create_simple_html](../create_simple_html). В Task VS Code скриптам передается путь к папке с фильтрами.

## Примеры

Если в frontmatter файла добавить:

```text
---
device-name: GOR 187F
---
```

то в тексте можно вызывать переменную `${device-name}`. При генерации выходного файла (pdf, docx, html) в `${device-name}` будет подставлено `GOR 187F`. Если при этом используется фильтр include_files, и во включаемых файлах тоже используется эта переменная, то `metadata_processor` должен быть применен после фильтра `include_files`.

Содержимое файла .md можно вставить в текущий файл md:

<pre>
```{.include shift-heading-level-by=1} >> force shift to be 1
file-a.md
```
</pre>

Если запустить pandoc с аргументом `-M include-auto`, то уровни заголовков будут автоматически сдвигаться в зависимости от раздела, из которого файл вызван.
