using ToolBox.Tools;
using ToolBox.Tools.Common;

namespace ToolBox
{
    public partial class MainForm : Form
    {
        private Tool? SelectedTool;
        private UserControl? ActiveUserControl;
        private List<Tool> Tools { get; } = new();
        public MainForm()
        {
            InitializeComponent();

            Tools.AddRange(new Tool[] { 
                new IsMyPcOn(UpdateProgressBarPercentage, RemoveProgressBar),
                new AngryParrot(UpdateProgressBarPercentage, RemoveProgressBar),
                new LightSwitch(UpdateProgressBarPercentage, RemoveProgressBar)
            });

            SetToolComboBoxItems();
        }

        private void SetToolComboBoxItems()
        {
            var toolNames = Tools.Select(x => x.Name).ToArray();
            toolComboBox.Items.AddRange(toolNames);
        }

        private void toolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedToolName = (string)toolComboBox.SelectedItem;
            int resultIndex = toolComboBox.FindStringExact(selectedToolName);

            if (resultIndex == -1)
                return;

            // hide label when item selected.
            if (toolLabel.Visible)
                toolLabel.Visible = false;

            Controls.Remove(ActiveUserControl);
            ActiveUserControl?.Dispose();

            SelectedTool = Tools[resultIndex];
            ActiveUserControl = SelectedTool.GetUserControl();

            if (ActiveUserControl == null)
                return;

            ActiveUserControl.Location = new Point(24, 80);
            ActiveUserControl.Name = "activeUserControl";
            ActiveUserControl.Size = new Size(450, 300);
            ActiveUserControl.TabIndex = 1;

            Controls.Add(ActiveUserControl);
        }

        private void UpdateProgressBarPercentage(int percentage) {
            if(!progressBar.Visible)
                progressBar.Visible = true;

            progressBar.Value = percentage;
        }

        private void RemoveProgressBar()
        {
            progressBar.Visible = false;
            progressBar.Value = 0;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            SelectedTool?.Run();
        }
    }
}