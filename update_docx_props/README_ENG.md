# UpdateDocxProps.ps1

📜[RU](README.md)

Add or update custom properties .docx for all documents in folder. Script updates all fields, headers and footers on save. Specify any number of properties via .xml configuration file.
See old versions in [old_versions](old_versions) folder.

```powershell
.\update_docx_props.ps1 -dir D:\path\to\docs -conf D:\path\to\config\config.xml [-filename] testwordfile.docx
```

- `-dir` -- path to folder with docx, default is current dir;
- `-conf` -- path to config, default is `$dir/config.xml`;
- `-filename` -- if set, that script will be applied to a single file with specified name in the `$dir`. `filename` version is here [update_docx_props_single](update_docx_props_single).

## V5 - Macro

[v5 - Macro](update_docx_props_macro)

Added the ability to call Microsoft Word macros in a script.
The list of macros is specified in the `if ($callmacro)` condition.
See [macros](https://github.com/annjulyleon/doc-scripts/tree/main/word_macros).

```console
.\UpdateDocxPropsConfig_v3.ps1 -dir . -conf config.xml -callmacro:$true [-filename]
```

- `-dir` -- directory for documents
- `-conf` -- path to configuration file
- `-callmacro` -- switch to call macros, e.g. `callmacro:$true`
- `-filename` -- optional, filename. If specified, the script will be executed only for this file in `$dir`

## V4 `Single`

- added a [version](update_docx_props_single) with an option to apply script for single file.

## v3

- added optional update for vsd files (built-in properties)
- update for built-in properties for doc/docx
- script can now update fields inside shapes and labels
- changed .xml config (just remove properties you don't need)

Folder structure:

- `config.xml` - configuration file. Now contains 3 sections: `customProperties` for custom word properties, `builtinProperties` - built-in word properties, `vsdProperties` - built-in visio properties.
  
    ```xml
    <?xml version="1.0"?>
    <configuration>
      <customProperties>    
        <add key="property1" value="new value for the property"/>
      </customProperties>
      <builtinProperties>
        <add key="Title" value="This is Title property"/>
        <add key="Subject" value="This is Subject property"/>
        <add key="Keywords" value="some tag more tag"/>
        <add key="Comments" value="somecomment"/>
      </builtinProperties>
      <vsdProperties>
        <add key="Company" value="LLC COMPANY"/>
        <add key="Category" value="Category of the document"/>
        <add key="Title" value="Title of the document"/>
        <add key="Subject" value="Subject of the document"/>
        <add key="Keywords" value="Some tags"/>
        <add key="Description" value="Desc comment"/>
        <add key="Manager" value="Project Manager"/>
      </vsdProperties>
    </configuration>
    ```
  
  If you don't need to change any vsd or built-in word properties just remove them from the section

```xml
    <?xml version="1.0"?>
    <configuration>
      <customProperties>    
        <add key="property1" value="new value for the property"/>
      </customProperties>
      <builtinProperties>        
      </builtinProperties>
      <vsdProperties>        
      </vsdProperties>
    </configuration>
```

In [old_versions folder](old_versions) in each folder:

- `updateProps.bat` to launch script for current directory and all child directories. You can comment out this line, if you don't need to update vsd properties:

    ```bat
    Powershell.exe -noprofile -executionpolicy bypass -File UpdateVsdProps_v1.ps1 > %CurrentDateTime%_vsdprops.txt
    ```

- `UpdateDocxProps_v3.ps1` - updates docx properties;
- `UpdateVsdProps_v1.ps1` - updates vsd properties;
- two test files: `testvsdfile.vsd` and `teswordfile.docx`.

Just change config and launch the script from the`.bat` file. Both scripts have the same parameters: `-dir` (defaults to current dir) and `-conf` (defaults to current dir and `config,xml` file).

### v2

- code refactoring
- exclude folders (`exclude` variable), default is `old,_old,source,_source`
- example bat-file with logging added

Example usage:

```powershell
.\UpdateDocxProps.ps1 -dir D:\path\to\docs -conf D:\path\to\config\UpdateDocxPropsConfig.xml
```

Use with .bat:

1. copy script files to doc/docx folder
2. change config.xml, add properties
3. launch updateProps.bat with administrative rights (to overwrite PowerShell restriction). Uncomment "chcp 1251" string if using with RU language
4. check log file

Example config:

```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
<!--Vars -->
    <add key="NameOfProperty1" value="ValueOfProperty1"/>
    <add key="NameOfProperty2" value="ValueOfProperty2"/>
  </appSettings>
</configuration>
```

Source:

- [Powershell: Everything you wanted to know about hashtables](https://powershellexplained.com/2016-11-06-powershell-hashtable-everything-you-wanted-to-know-about/)
- [How can I introduce a config file to Powershell scripts?](https://stackoverflow.com/a/13698982)
- [How to change custom properties for many Word documents](https://stackoverflow.com/a/35920682)
- [Powershell Update Fields in Header and Footer in Word](https://stackoverflow.com/questions/24887905/powershell-update-fields-in-header-and-footer-in-word)
  