using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
                private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }
        

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            gridView1.BestFitColumns();
        }


        RepositoryItemButtonEdit GetRI(object value)
        {
            // you can cache your items here
            if (repositoryItemButtonEdit1.TextEditStyle != DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor) return repositoryItemButtonEdit1;
            RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            ri.Assign(repositoryItemButtonEdit1);
            ri.Buttons[0].Caption = String.Format("Find: {0}", value);
            return ri;
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
                e.RepositoryItem = GetRI(e.CellValue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show(e.Button.Caption);
        }
    }
}