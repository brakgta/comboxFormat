using System.Windows.Forms;
using System.Drawing;
using System;

namespace comboxFormat
{
    public partial class ComboBoxMaster: UserControl
    {
        public bool MostrarSubItem;
        public int indexSubItem;
        public string SelectedValue = "";
        public int CBMaltura = 110;
        public int ListViewLargura;
        public View MododeVizualização = View.Details;
        public bool MostraHeader;
      
        public ComboBoxMaster()
        {
            InitializeComponent();
        }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Height = CBMaltura;
            comboBox1page1.Enabled = false;
            //inicializacao da listView 
            listView1.Visible = true;
            listView1.Width = comboBox1page1.Width;
            listView1.Location = new Point(0, 17);
            this.BringToFront();
            listView1.BringToFront();
            listView1.View = MododeVizualização;
            

            if (!MostraHeader)
                listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.FullRowSelect = true;

        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
                MostrarSubitem(MostrarSubItem, indexSubItem);
        }
        private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && IsInBound(e.Location, listView1.SelectedItems[0].Bounds))
            {
                listView1.Visible = false;
                MostrarSubitem(MostrarSubItem, indexSubItem);
                comboBox1page1.Enabled = true;
                this.Height = 21;
            }
        }
        public bool IsInBound(Point location, Rectangle bound)
        {
            return (bound.Y <= location.Y &&
                    bound.Y + bound.Height >= location.Y &&
                    bound.X <= location.X &&
                    bound.X + bound.Width >= location.X);
        }
       public void EditaColunas(int numColunas ,string TamanhoDeCadaColuna)
        {
            string[] lista = TamanhoDeCadaColuna.Split(';');
            for (int i = 0; i < numColunas; i++)
                listView1.Columns.Add(i.ToString()).Width = Convert.ToInt32(lista[i]);
        }
        public void MostrarSubitem(bool MostrarSubItem, int indexSubItem)
        {
            comboBox1page1.Items.Clear();
            if (MostrarSubItem)
                comboBox1page1.Items.Add(listView1.SelectedItems[0].SubItems[indexSubItem].Text);
            else
                comboBox1page1.Items.Add(listView1.SelectedItems[0].Text);
            comboBox1page1.SelectedIndex = 0;
        }
        public void PopulaListaView(ListViewItem listViewItem)
        {
            if (!listView1.Items.Contains(listViewItem))
                listView1.Items.Add(listViewItem);
        }
        public string AssociaListBox(bool subitem, int indexsubitem)
        {
            if (subitem)
                return listView1.FindItemWithText(comboBox1page1.SelectedItem.ToString(), true, 0).SubItems[indexsubitem].Text == null?
                    "" : 
                    listView1.FindItemWithText(comboBox1page1.SelectedItem.ToString(), true, 0).SubItems[indexsubitem].Text;
            else
                return  listView1.FindItemWithText(comboBox1page1.SelectedItem.ToString(), true, 0).Text == null ?
                    "" :
                    listView1.FindItemWithText(comboBox1page1.SelectedItem.ToString(), true, 0).Text;
             
        }
    }
}
