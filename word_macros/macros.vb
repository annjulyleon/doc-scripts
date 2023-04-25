' This macros sheet contains macros for:
' - Autonumbering Figures and Tables by pattern in `*Pattern` variable and replacing with the field `FieldText` variable
' - Inserting references to autonumbered Figures and Tables by searching pattern `*Pattern` and replacing it with reference (cross reference) `ObjectName`. Note, that `ObjectName` should exist in your references setting list
' - Finding specific table style and applying table settings.
' Macros work with US and NonUS regional settings (specificaly List Separator setting which is ; for NonUS and , for US)
' NOTES
' Microsoft 365 and Microsoft 2021 and different versions of Pandoc make slightly different HYPERLINK fields. 
' So if FigReferenceAutoInsert() and TblReferenceAutoInsert() don't work look if Mid(oField.Code, 15, 4) 
' returns expected result and adjust for your version. 
' Use Depug.Print and Ctrl + G in VBA to debug output

Sub FigCapAutoNum()
   On Error GoTo errHandler
   
   NonUsPattern = "Рисунок [0-9]{1;}"
   UsPattern = "Рисунок [0-9]{1,}"
   FieldText = "SEQ Рисунок \* ARABIC"
   Call CapAutoNum(NonUsPattern, FieldText)
   
procDone:
  ActiveDocument.Fields.Update
  Exit Sub
errHandler:
  Select Case Err.Number
    Case 5560
     Call CapAutoNum(UsPattern, FieldText)
    Case Else
      Exit Sub
  End Select
Resume procDone
End Sub

Sub TblCapAutoNum()

   On Error GoTo errHandler

   Dim Rng As Range
   Set Rng = ActiveDocument.Range
   NonUsPattern = "Таблица [0-9]{1;}"
   UsPattern = "Таблица [0-9]{1,}"
   FieldText = "SEQ Таблица \* ARABIC"
   Call CapAutoNum(NonUsPattern, FieldText)
   
procDone:
  ActiveDocument.Fields.Update
  Exit Sub
errHandler:
  Select Case Err.Number
    Case 5560
     Call CapAutoNum(UsPattern, FieldText)
    Case Else
      Exit Sub
  End Select

Resume procDone
End Sub


Sub FigReferenceAutoInsert()
   On Error GoTo errHandler
   
   NonUsPattern = "\(рисунок [0-9]{1;}\)"
   UsPattern = "\(рисунок [0-9]{1,}\)"
   ObjectName = "Рисунок"
   
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
   
   Call ReferenceAutoInsert(NonUsPattern, ObjectName)
   
procDone:
  ActiveDocument.Fields.Update
  Exit Sub
errHandler:
  Select Case Err.Number
    Case 5560
      Call ReferenceAutoInsert(UsPattern, ObjectName)
    Case Else
      Exit Sub
  End Select
Resume procDone
End Sub

Sub TblReferenceAutoInsert()
   On Error GoTo errHandler
   
   NonUsPattern = "\(таблица [0-9]{1;}\)"
   UsPattern = "\(таблица [0-9]{1,}\)"
   ObjectName = "Таблица"
   
   Dim oField As Field
   For Each oField In ActiveDocument.Fields
    If oField.Type = wdFieldHyperlink Then
      If Mid(oField.Code, 15, 4) = "tbl:" Then
        oField.Unlink
      End If
    End If
   Next
   Set oField = Nothing
  
      Call ReferenceAutoInsert(NonUsPattern, ObjectName)
   
procDone:
  ActiveDocument.Fields.Update
  Exit Sub
errHandler:
  Select Case Err.Number
    Case 5560
      Call ReferenceAutoInsert(UsPattern, ObjectName)
    Case Else
      Exit Sub
  End Select
Resume procDone
End Sub

Sub TableHeaderFix()
'
' This macors find all tables with specific styles and set first row to center
' Additionaly set width of columns to Distribute.
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

Sub ReferenceAutoInsert(Pattern, ObjectName)
'
' This macros replace reference to object
' This is subrotine macros which is called from other macros
' Do not call this directly
'
     Dim objReference As String
     Dim Rng As Range
     Set Rng = ActiveDocument.Range
       Do While Rng.Find.Execute(FindText:=Pattern, Forward:=True, Format:=False, Wrap:=wdFindStop, MatchWildcards:=True, MatchCase:=True) = True
          objReference = Rng.Text
          objReference = Mid(objReference, 10)
          objReference = Replace(objReference, ")", "")
          objInt = CInt(objReference)
          objVar = CVar(objInt)
          Rng.MoveStart Unit:=wdCharacter, Count:=1
          Rng.MoveEnd Unit:=wdCharacter, Count:=-1
          Rng.InsertCrossReference ReferenceType:=ObjectName, ReferenceKind:= _
          wdOnlyLabelAndNumber, ReferenceItem:=objVar, InsertAsHyperlink:=True, _
          IncludePosition:=False, SeparateNumbers:=False, SeparatorString:=" "
     Loop
   End Sub

Sub CapAutoNum(Pattern, FieldText)
   Dim Rng As Range
   Set Rng = ActiveDocument.Range
   Do While Rng.Find.Execute(FindText:=Pattern, Forward:=True, Format:=False, Wrap:=wdFindStop, MatchWildcards:=True, MatchCase:=True) = True
        Rng.MoveStart Unit:=wdCharacter, Count:=8
        Rng.Fields.Add Range:=Rng, Type:=wdFieldEmpty, Text:=FieldText, PreserveFormatting:=True
   Loop
End Sub
