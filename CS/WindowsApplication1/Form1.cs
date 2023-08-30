using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WindowsApplication1 {
    public partial class Form1 : Form {
        private DataTable CreateTable(int RowCount) {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }
        public Form1() {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            gridView1.BestFitColumns();
        }
        Dictionary<object, RepositoryItemButtonEdit> repositoryItemsCache = new Dictionary<object, RepositoryItemButtonEdit>();
        RepositoryItemButtonEdit GetRepositoryItem(object value) {
            if (defaultRepositoryItemButtonEdit.TextEditStyle != DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor)
                return defaultRepositoryItemButtonEdit;

            //you can cache repository items as follows
            RepositoryItemButtonEdit repositoryItem;
            if (repositoryItemsCache.TryGetValue(value, out repositoryItem)) {
                return repositoryItem;
            } else {
                repositoryItem = new RepositoryItemButtonEdit();
                repositoryItem.Assign(defaultRepositoryItemButtonEdit);
                repositoryItem.Buttons[0].Caption = String.Format("Find: {0}", value);
                repositoryItemsCache[value] = repositoryItem;
                return repositoryItem;
            }
        }
        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e) {
            e.RepositoryItem = GetRepositoryItem(e.CellValue);
        }
        private void button1_Click(object sender, EventArgs e) {
            defaultRepositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            MessageBox.Show(e.Button.Caption);
        }
    }
}