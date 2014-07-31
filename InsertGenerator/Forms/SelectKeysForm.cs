using InsertGenerator.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertGenerator.Forms
{
    public partial class SelectKeysForm : Form
    {
        public List<string> SelectedColumns
        {
            get
            {
                List<string> result = new List<string>();

                foreach (var item in columnListBox.CheckedItems)
                {
                    result.Add(item.ToString());
                }
                return result;
            }
        }

        public SelectKeysForm(IEnumerable<string> queryColumns)
        {
            InitializeComponent();
            PopulateColumns(queryColumns);
        }

        private void PopulateColumns(IEnumerable<string> queryColumns)
        {
            this.columnListBox.Items.Clear();
            this.columnListBox.Items.AddRange(queryColumns.OrderBy(x => x).ToArray());
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
