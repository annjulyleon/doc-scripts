' Fix КРАКОЗЯБРЫ for your encound, when pasting to Word
' Note, that Microsoft 365 and Microsoft 2021 and different versions of Pandoc make slightly different HYPERLINK fields. 
' So if FigReferenceAutoInsert() and TblReferenceAutoInsert() don't work look if Mid(oField.Code, 15, 4) 
' returns expected result and adjust for your version. 
' Use Depug.Print and Ctrl + G in VBA to debug output

Sub FigCapAutoNum()
   Dim Rng As Range
   Set Rng = ActiveDocument.Range
   Do While Rng.Find.Execute(FindText:="Рисунок [0-9]{1;}", Forward:=True, Format:=False, Wrap:=wdFindStop, MatchWildcards:=True, MatchCase:=True) = True
        Rng.MoveStart Unit:=wdCharacter, Count:=8
        Rng.Fields.Add Range:=Rng, Type:=wdFieldEmpty, Text:="SEQ Рисунок \* ARABIC", PreserveFormatting:=True
   Loop
   ActiveDocument.Fields.Update
End Sub

Sub TblCapAutoNum()

   Dim Rng As Range
   Set Rng = ActiveDocument.Range
   Do While Rng.Find.Execute(FindText:="Таблица [0-9]{1;}", Forward:=True, Format:=False, Wrap:=wdFindStop, MatchWildcards:=True, MatchCase:=True) = True
        Rng.MoveStart Unit:=wdCharacter, Count:=8
        Rng.Fields.Add Range:=Rng, Type:=wdFieldEmpty, Text:="SEQ Таблица \* ARABIC", PreserveFormatting:=True
   Loop
   ActiveDocument.Fields.Update
End Sub


Sub FigReferenceAutoInsert()
   Dim figReference As String
   
   Dim oField As Field
   For Each oField In ActiveDocument.Fields
    If oField.Type = wdFieldHyperlink Then
      Debug.Print Mid(oField.Code, 15, 4)
      If Mid(oField.Code, 15, 4) = "fig:" Then
        oField.Unlink
      End If
    End If
   Next
   Set oField = Nothing
   
   Dim Rng As Range
   Set Rng = ActiveDocument.Range
   Do While Rng.Find.Execute(FindText:="\(рисунок [0-9]{1;}\)", Forward:=True, Format:=False, Wrap:=wdFindStop, MatchWildcards:=True, MatchCase:=True) = True
        figReference = Rng.Text
        figReference = Mid(figReference, 10)
        figReference = Replace(figReference, ")", "")
        figInt = CInt(figReference)
        figVar = CVar(figInt)
        Rng.MoveStart Unit:=wdCharacter, Count:=1
        Rng.MoveEnd Unit:=wdCharacter, Count:=-1
        Rng.InsertCrossReference ReferenceType:="Рисунок", ReferenceKind:= _
        wdOnlyLabelAndNumber, ReferenceItem:=figVar, InsertAsHyperlink:=True, _
        IncludePosition:=False, SeparateNumbers:=False, SeparatorString:=" "
   Loop
   ActiveDocument.Fields.Update
End Sub

Sub TblReferenceAutoInsert()
   Dim tblReference As String
   
   Dim oField As Field
   For Each oField In ActiveDocument.Fields
    If oField.Type = wdFieldHyperlink Then
      If Mid(oField.Code, 15, 4) = "tbl:" Then
        oField.Unlink
      End If
    End If
   Next
   Set oField = Nothing
  
   Dim Rng As Range
   Set Rng = ActiveDocument.Range
   Do While Rng.Find.Execute(FindText:="\(таблица [0-9]{1;}\)", Forward:=True, Format:=False, Wrap:=wdFindStop, MatchWildcards:=True, MatchCase:=True) = True
        tblReference = Rng.Text
        tblReference = Mid(tblReference, 10)
        tblReference = Replace(tblReference, ")", "")
        tblInt = CInt(tblReference)
        tblVar = CVar(tblInt)
        Rng.MoveStart Unit:=wdCharacter, Count:=1
        Rng.MoveEnd Unit:=wdCharacter, Count:=-1
        Rng.InsertCrossReference ReferenceType:="Таблица", ReferenceKind:= _
        wdOnlyLabelAndNumber, ReferenceItem:=tblVar, InsertAsHyperlink:=True, _
        IncludePosition:=False, SeparateNumbers:=False, SeparatorString:=" "
   Loop
   ActiveDocument.Fields.Update
End Sub

Sub TableHeaderFix()
'
' Fixed tables alignment in first row for specific styles only
'
  Dim atable As Table
  For Each atable In ActiveDocument.Tables
    If atable.Style = "TableStyleGostNoHeader" Then
      atable.Columns.DistributeWidth
      atable.Rows(1).Select
      Selection.ParagraphFormat.Alignment = wdAlignParagraphCenter
    End If
    If atable.Style = "TableStyleGost" Then
      atable.Columns.DistributeWidth
      atable.Rows(1).Select
      Selection.ParagraphFormat.Alignment = wdAlignParagraphCenter
    End If
  Next
End Sub