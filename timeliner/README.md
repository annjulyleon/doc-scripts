# Timeliner

Генерирует из списка коммитов git или redmine issues датасет для использования в [timeline-vis](https://visjs.org/).
Также в репозитории лежит архив с самим timliner'ом.

## Использование

Получить список коммитов из локального GIT:

```console
git log --date=format:'%Y-%m-%d' --pretty=format:'%H;%ad;%s' >> commits.txt
```

Пример получения списка задач из Redmine: `https://rm.inforion.ru/issues.xml?project_id=91&status_id=closed&created_on=%3E%3C2022-01-01|2022-12-31&sort=asc,closed_on&limit=100`. Сохранить как `issues.xml`.

Вызвать скрипт:

```bash
# Вызов хэлпа
python timeliner.py -h

# Обработка коммитов гита
python timeliner.py --format 'gm' --url 'https://gitlab.domain.com/common/cool_project/-/commit' --input 'commits.txt' --output 'newset.txt'

# Обработка списка задач в redmine
python timeliner.py -f 'rm' -u 'https://rm.inforion.ru/issues' -i 'issues.xml' -o 'rmwset.txt' 
```

Содержимое из результирующего файла вставить в `const items = new vis.DataSet`.
