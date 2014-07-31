using InsertGenerator.Query;
using InsertGenerator.Writers;
using ScintillaNET.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertGenerator.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PopulateDestinationLanguages();
            PopulateQueryTypes();
            UpdateGenerateButtonEnabledState();

            connectionStringTextBox.Text = Configuration.ConnectionString;
            queryTypeComboBox.SelectedIndex = 0;
        }
        
        private void UpdateGenerateButtonEnabledState()
        {
            bool enabled = true;
            if (connectionStringTextBox.Text.Trim() == "" || 
                sqlTextBox.Text.Trim() == "" ||
                dstTableTextBox.Text.Trim() == "" || 
                queryTypeComboBox.SelectedIndex < 0 ||
                destinationLanguage.SelectedIndex < 0)
            {
                enabled = false;
            }
            GenerateButton.Enabled = enabled;
        }

        private void PopulateDestinationLanguages()
        {
            destinationLanguage.Items.Clear();
            foreach (string language in QueryWriterFactory.Instance.GetKeys().OrderBy(x => x))
            {
                destinationLanguage.Items.Add(language);
            }
        }

        private void PopulateQueryTypes()
        {
            queryTypeComboBox.Items.Clear();
            foreach (string type in QueryFactory.Instance.GetKeys().OrderBy(x => x))
            {
                queryTypeComboBox.Items.Add(type);
            }
        }

        private void StartScriptGeneration()
        {
            sqlTextBox.Enabled = queryTypeComboBox.Enabled = dstTableTextBox.Enabled = destinationLanguage.Enabled = connectionStringTextBox.Enabled = false;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Enabled = true;
            statusLabel.Text = "Working";
        }

        private void StopScriptGeneration()
        {
            sqlTextBox.Enabled = queryTypeComboBox.Enabled = dstTableTextBox.Enabled = destinationLanguage.Enabled = connectionStringTextBox.Enabled = true;
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 0;
            progressBar.Enabled = false;
        }

        private void queryTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGenerateButtonEnabledState();
        }

        private void dstTableTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateGenerateButtonEnabledState();
        }

        private void InitialiseSaveDialog()
        {
            string writerName = destinationLanguage.SelectedItem.ToString();
            saveFileDialog.Filter = string.Format("{0} Files|*{1}", writerName, QueryWriterFactory.Instance.GetExtension(writerName));
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string filename;

            InitialiseSaveDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
                string dstLang = destinationLanguage.SelectedItem.ToString();
                string queryType = queryTypeComboBox.SelectedItem.ToString();
                Job job = new Job(
                                connectionStringTextBox.Text,
                                dstTableTextBox.Text,
                                filename,
                                sqlTextBox.Text,
                                () => QueryWriterFactory.Instance.Get(dstLang),
                                () => QueryFactory.Instance.Get(queryType),
                                (j,c) => HandleKeysRequired(j,c),
                                (j, ex) => HandleException(j, ex),
                                (j) => HandleComplete(j));


                StartScriptGeneration();
                job.Process();
            }
            else
            {
                statusLabel.Text = "Cancelled.";
            }
        }

        private List<string> HandleKeysRequired(Job job,IEnumerable<string> columns)
        {
            object monitorObject = new object();
            List<string> keys = new List<string>();
            this.Invoke((MethodInvoker)delegate
            {
                SelectKeysForm form = new SelectKeysForm(columns);

                form.ShowDialog(this);
                keys.AddRange(form.SelectedColumns);
            });

            return keys;
        }

        private void HandleException(Job job, Exception ex)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error: " + ex.Message;
                StopScriptGeneration();
            });
        }

        private void HandleComplete(Job job)
        {
            this.Invoke((MethodInvoker)delegate
            {
                statusLabel.Text = "Complete.";
                StopScriptGeneration();
            });
        }

        private void destinationLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGenerateButtonEnabledState();
        }

        private void sqlTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateGenerateButtonEnabledState();
        }
    }
}
