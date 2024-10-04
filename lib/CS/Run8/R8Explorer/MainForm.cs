using System.Diagnostics;

namespace R8Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open file picker
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = FileFilterManager.GetOpenFileDialogFilter();
            openFileDialog.Title = "Open Run8 Train Simulator File";
            var res = openFileDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                // open file
                // Process.Start(openFileDialog.FileName);
                MessageBox.Show("Opening file: " + openFileDialog.FileName);
            }

            openFileDialog.Dispose();
        }
    }
}
