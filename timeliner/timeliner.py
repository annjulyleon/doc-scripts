import xml.etree.ElementTree as ET
import argparse
'''
Commits from git command:
git log --date=format:'%Y-%m-%d' --pretty=format:"%H;%ad;%s" >> commits.txt

Example request to rm api to list issues (https://www.redmine.org/projects/redmine/wiki/Rest_Issues)
https://rm.inforion.ru/issues.xml?project_id=91&status_id=closed&created_on=%3E%3C2022-01-01|2022-12-31&sort=asc,closed_on&limit=100
'''

def read_redmine_issues(path):
    tree = ET.parse(path)
    root = tree.getroot()
    issues = []

    for child in root:
        id = child.find('id').text
        name = child.find('subject').text
        closed = child.find('closed_on').text
        issues.append(f'{id};{closed};{name}')

    return issues

def read_commits(path):
    try:
        with open(path, encoding='utf-8') as file:
            data = [line.rstrip() for line in file.readlines()]
        return data

    except FileNotFoundError:
        print("File with commits {path} not found in the directory")

def write_commits(result, output_path):
    with open(output_path, 'w', encoding='utf-8') as f:
        for line in result:
            f.write(f"{line}\n")

def make_timeline_set(url,input,output):
    lines = []
    for i,c in enumerate(input):
        record = c.split(';')
        line = f'{{id: {i}, content: \'<a href="{url[:-1] if url.endswith("/") else url}/{record[0]}" target="_blank">{record[2]}</a>\', start: \'{record[1]}\'}},'
        lines.append(line)

    write_commits(lines, output)

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Definition table builder",
                                     formatter_class=argparse.ArgumentDefaultsHelpFormatter)
    parser.add_argument("-f", "--format", type=str, default="gm",
                        help="Format of input file: gm - git commits,"
                             "rm - redmine commits")
    parser.add_argument("-u", "--url", type=str, default="https://gitlab.lab403.inforion.ru/a.leonova/report_sr12/-/commit",
                        help="Url to append issue or commit")
    parser.add_argument("-i", "--input", type=str, default="commits.txt",
                        help="Input file with list of items for the timeline")
    parser.add_argument("-o", "--output", type=str, default="set.txt",
                        help="Output file with data, ready for vis-timeline")

    args = vars(parser.parse_args())

    if args["format"] == 'gm':
        i = read_commits(args["input"])
    elif args["format"] == 'rm':
        i = read_redmine_issues(args["input"])

    make_timeline_set(args["url"], i, args["output"])






