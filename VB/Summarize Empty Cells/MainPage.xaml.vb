Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Windows.Controls
Imports DevExpress.Data
Imports DevExpress.Xpf.Data
Imports DevExpress.Xpf.Grid

Namespace Summarize_Empty_Cells
	Partial Public Class MainPage
		Inherits UserControl
		Private list As List(Of TestData)
		Private selectedValues As New Dictionary(Of Integer, Boolean)()
		Public Sub New()
			InitializeComponent()
			list = New List(Of TestData)()
			For i As Integer = 0 To 99
				list.Add(New TestData() With {.Text = "group " & i Mod 10, .Number = i})
				If i Mod 3 = 0 Then
					list(i).Number = Nothing
				End If
			Next i
			grid.DataSource = list
		End Sub
		Private emptyCellsTotalCount As Integer = 0
		Private emptyCellsGroupCount As Integer = 0
		Private Sub grid_CustomSummary(ByVal sender As Object, ByVal e As CustomSummaryEventArgs)
			If (CType(e.Item, GridSummaryItem)).FieldName <> "Number" Then
				Return
			End If
			If e.IsTotalSummary Then
				If e.SummaryProcess = CustomSummaryProcess.Start Then
					emptyCellsTotalCount = 0
				End If
				If e.SummaryProcess = CustomSummaryProcess.Calculate Then
					Dim val? As Integer = CType(e.FieldValue, Integer?)
					If (Not val.HasValue) Then
						emptyCellsTotalCount += 1
					End If
					e.TotalValue = emptyCellsTotalCount
				End If
			End If
			If e.IsGroupSummary Then
				If e.SummaryProcess = CustomSummaryProcess.Start Then
					emptyCellsGroupCount = 0
				End If
				If e.SummaryProcess = CustomSummaryProcess.Calculate Then
					Dim val? As Integer = CType(e.FieldValue, Integer?)
					If (Not val.HasValue) Then
						emptyCellsGroupCount += 1
					End If
					e.TotalValue = emptyCellsGroupCount
				End If
			End If
		End Sub
		Public Class TestData
			Private privateText As String
			Public Property Text() As String
				Get
					Return privateText
				End Get
				Set(ByVal value As String)
					privateText = value
				End Set
			End Property
			Private privateNumber? As Integer
			Public Property Number() As Integer?
				Get
					Return privateNumber
				End Get
				Set(ByVal value? As Integer)
					privateNumber = value
				End Set
			End Property
		End Class
	End Class
End Namespace
