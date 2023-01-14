namespace ToolBox.UserControls
{
    public partial class UserControlWithTextBox : UserControl
    {
        public UserControlWithTextBox()
        {
            InitializeComponent();
        }

        public string GetTextValue()
        {
            return textBox1.Text;
        }
    }
}
