# Markdown stripper

[RU](README_RU.md)

What it does:

1. Copies all files with folder structure from `source` to `destination`.
2. In `destination` for each .md file except `ignored` removes or removes/replaces text using regular expressions defined in `PATTERNS` variable.

You can add your own patterns to the `PATTERNS` variable:

```python
PATTERNS = [
    {"pattern": "text to replace", "replace": "new text", "desc": "Replace words"},
    {"pattern": "\[-@(fig|tbl|sec):.+?(?=])]", "replace": "as follows ", "desc": "Replace patterns [-fig:name_of_object],[-tbl:name_of_object],[-sec:name_of_object]"}
]
```

Regular expressions should follow Python `re` library rules.  

Help:

```text
python .\stripper.py --help
```

Full command:

```text
python .\stripper.py -s docs -d docs_stripped -i -i *.png *.bat *.ps1 *.lua end.md begin.md
```

- `-s`, `--source` - directory in current folder where .md files are stored
- `-d`, `--destination` - name of directory to copy stripped files
- `-i`, `--ignored` - pass ignored files, separated by whitespace, for example: `-i *.png *.bat *.ps1 *.lua end.md begin.md`
