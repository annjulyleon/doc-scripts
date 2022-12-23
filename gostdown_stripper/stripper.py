import re
import shutil
import argparse
from pathlib import Path

PATTERNS = [
    {"pattern": "{#(fig|tbl|sec):.+?(?=})}", "replace": "", "desc": "caption"},
    {"pattern": "\[-@(fig|tbl|sec):.+?(?=])]", "replace": "ниже ", "desc": "reference"},
    {"pattern": "{.+?(?=})}", "replace": "", "desc": "custom style"},
    {"pattern": "# \[(.+?(?=\]))\]", "replace": r"# \1", "desc": "headings brackets"}
]


def stripper(patterns: list[dict], file_content) -> str:
    stripped_file_content = file_content
    for pattern in patterns:
        stripped_file_content = re.sub(pattern["pattern"], pattern["replace"], stripped_file_content)

    return stripped_file_content


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Gostdown stripper for md files",
                                     formatter_class=argparse.ArgumentDefaultsHelpFormatter)
    parser.add_argument("-s","--source", type=str, default="docs",
                        help="Directory in current folder where .md files are stored")
    parser.add_argument("-d", "--destination", type=str, default="docs_stripped",
                        help="Name of directory, where stripped files will be placed")
    parser.add_argument("-i","--ignored", nargs='+',
                        help="Pass ignored files, separated by spase, for example: '-i *.png *.bat *.ps1 *.lua end.md begin.md'")
    args = vars(parser.parse_args())

    # copy directories with files
    shutil.copytree(args["source"],args["destination"], ignore = shutil.ignore_patterns(*args["ignored"]))

    stripped_dir = args["destination"]
    paths = Path(stripped_dir).rglob("*.md")

    for path in paths:
        try:
            with open(path, 'r', encoding="utf-8") as file:
                file_content = file.read()
        except FileNotFoundError:
            print(f'File "{path}" not found')

        stripped_content = stripper(PATTERNS, file_content)

        with open(path, "w", encoding="utf-8") as file:
            file.write(stripped_content)


