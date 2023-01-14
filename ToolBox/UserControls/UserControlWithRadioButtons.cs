namespace ToolBox.UserControls
{
    public partial class UserControlWithRadioButtons : UserControl
    {
        public UserControlWithRadioButtons()
        {
            InitializeComponent();
        }

        public bool GetValue()
        {
            return radioButtonYes.Checked;
        }
    }
}
