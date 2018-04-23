Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
				Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
				End Function


		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			gridView1.BestFitColumns()
		End Sub


		Private Function GetRI(ByVal value As Object) As RepositoryItemButtonEdit
			' you can cache your items here
			If repositoryItemButtonEdit1.TextEditStyle <> DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor Then
				Return repositoryItemButtonEdit1
			End If
			Dim ri As New RepositoryItemButtonEdit()
			ri.Assign(repositoryItemButtonEdit1)
			ri.Buttons(0).Caption = String.Format("Find: {0}", value)
			Return ri
		End Function

		Private Sub gridView1_CustomRowCellEdit(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles gridView1.CustomRowCellEdit
				e.RepositoryItem = GetRI(e.CellValue)
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
		End Sub

		Private Sub repositoryItemButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles repositoryItemButtonEdit1.ButtonClick
			MessageBox.Show(e.Button.Caption)
		End Sub
	End Class
End Namespace