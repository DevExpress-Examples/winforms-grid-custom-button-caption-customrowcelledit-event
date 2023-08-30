Imports DevExpress.XtraEditors.Repository
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows.Forms

Namespace WindowsApplication1

    Public Partial Class Form1
        Inherits Form

        Private Function CreateTable(ByVal RowCount As Integer) As DataTable
            Dim tbl As DataTable = New DataTable()
            tbl.Columns.Add("Name", GetType(String))
            tbl.Columns.Add("ID", GetType(Integer))
            tbl.Columns.Add("Number", GetType(Integer))
            tbl.Columns.Add("Date", GetType(Date))
            For i As Integer = 0 To RowCount - 1
                tbl.Rows.Add(New Object() {String.Format("Name{0}", i), i, 3 - i, Date.Now.AddDays(i)})
            Next

            Return tbl
        End Function

        Public Sub New()
            InitializeComponent()
            gridControl1.DataSource = CreateTable(20)
            gridView1.BestFitColumns()
        End Sub

        Private repositoryItemsCache As Dictionary(Of Object, RepositoryItemButtonEdit) = New Dictionary(Of Object, RepositoryItemButtonEdit)()

        Private Function GetRepositoryItem(ByVal value As Object) As RepositoryItemButtonEdit
            If defaultRepositoryItemButtonEdit.TextEditStyle <> DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor Then Return defaultRepositoryItemButtonEdit
            'you can cache repository items as follows
            Dim repositoryItem As RepositoryItemButtonEdit
            If repositoryItemsCache.TryGetValue(value, repositoryItem) Then
                Return repositoryItem
            Else
                repositoryItem = New RepositoryItemButtonEdit()
                repositoryItem.Assign(defaultRepositoryItemButtonEdit)
                repositoryItem.Buttons(0).Caption = String.Format("Find: {0}", value)
                repositoryItemsCache(value) = repositoryItem
                Return repositoryItem
            End If
        End Function

        Private Sub gridView1_CustomRowCellEdit(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs)
            e.RepositoryItem = GetRepositoryItem(e.CellValue)
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            defaultRepositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        End Sub

        Private Sub repositoryItemButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
            MessageBox.Show(e.Button.Caption)
        End Sub
    End Class
End Namespace
