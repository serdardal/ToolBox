using ToolBox.Tools;
using ToolBox.Tools.Common;
using ToolBox.Utils;

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
                new IsMyPcOn(),
                new AngryParrot(),
                new LightSwitch()
            });

            SetToolComboBoxItems();

            WaitCursor.AddWaitChangeCallback(DisableControls);
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

            Controls.Remove(ActiveUserControl);
            ActiveUserControl?.Dispose();

            SelectedTool = Tools[resultIndex];
            SelectedTool.SetProgressCallbacks(UpdateProgressBarPercentage, RemoveProgressBar);
            ActiveUserControl = SelectedTool.GetUserControl();

            AdjustFormControls();
        }

        private void AdjustFormControls()
        {
            // hide label when item selected.
            if (toolLabel.Visible)
                toolLabel.Visible = false;

            // make toolStrip visible
            if (!toolStrip.Visible)
                toolStrip.Visible = true;

            if (SelectedTool != null)
            {
                openLocalFolderButton.Enabled = FileHelper.IsDirectoryExists(SelectedTool.LocalPath);
            }

            if (ActiveUserControl != null && !Controls.Contains(ActiveUserControl))
            {
                ActiveUserControl.Location = new Point(24, 80);
                ActiveUserControl.Name = "activeUserControl";
                ActiveUserControl.Size = new Size(450, 300);
                ActiveUserControl.TabIndex = 1;

                Controls.Add(ActiveUserControl);
            }
        }

        private void UpdateProgressBarPercentage(int percentage)
        {
            if (!progressBar.Visible)
                progressBar.Visible = true;

            progressBar.Value = percentage;
        }

        private void RemoveProgressBar()
        {
            progressBar.Visible = false;
            progressBar.Value = 0;

            AdjustFormControls();
        }

        private void DisableControls(bool disabled)
        {
            var disableChangeControls = new Control[]{
                toolComboBox,
                runButton,
                toolStrip
            };

            // method can be called from another thread
            // preventing cross-thread exception with .Invoke method
            foreach (var control in disableChangeControls)
            {
                control.Invoke(delegate { control.Enabled = !disabled; });
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            SelectedTool?.Run();
        }

        private void openSourceFolderButton_Click(object sender, EventArgs e)
        {
            if (SelectedTool == null)
                return;

            FileHelper.OpenFolderInExplorer(SelectedTool.SourcePath);
        }

        private void openLocalFolderButton_Click(object sender, EventArgs e)
        {
            if (SelectedTool == null)
                return;

            FileHelper.OpenFolderInExplorer(SelectedTool.LocalPath);
        }
    }
}