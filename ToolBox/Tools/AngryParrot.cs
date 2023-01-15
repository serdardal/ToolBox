using ToolBox.Tools.Common;
using ToolBox.UserControls;
using ToolBox.Utils;

namespace ToolBox.Tools
{
    public class AngryParrot : Tool
    {
        private UserControlWithTextBox? UserControlWithTextBox { get; set; }
        public AngryParrot(DirectoryCopier.ProgressChangeCallback updateProgressBarPercentage,
            DirectoryCopier.CompletedCallback removeProgressBar)
            
            : base("Angry Parrot",
            @$"{FileHelper.TOOLS_SHARED_BASE_PATH}\AngryParrot",
            @$"{FileHelper.TOOLS_LOCAL_BASE_PATH}\AngryParrot",
            updateProgressBarPercentage,
            removeProgressBar)
        {
            
        }
        public override UserControl? GetUserControl()
        {
            UserControlWithTextBox = new UserControlWithTextBox();
            return UserControlWithTextBox;
        }

        public override void Launch()
        {
            if (UserControlWithTextBox == null)
                return;

            var textBoxValue = UserControlWithTextBox.GetTextValue();
            FileHelper.StartPowershellProcessAsAdmin(workingDirectory: LocalPath, command: @$".\AngryParrot.ps1 -Message '{textBoxValue}'");
        }
    }
}
