using System;
using System.Windows.Forms;

namespace RunnerTray
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            txtCommand.Text = AppSettings.LoadCommand();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            AppSettings.SaveCommand(txtCommand.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Select command file";
                dialog.Filter = "Executable files (*.exe;*.com;*.bat)|*.exe;*.com;*.bat|All files (*.*)|*.*";
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                if (!string.IsNullOrWhiteSpace(txtCommand.Text))
                {
                    try
                    {
                        dialog.FileName = txtCommand.Text;
                    }
                    catch
                    {
                        // ignore invalid paths
                    }
                }

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtCommand.Text = dialog.FileName;
                }
            }
        }
    }
}

