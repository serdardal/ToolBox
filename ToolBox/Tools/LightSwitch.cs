using ToolBox.Tools.Common;
using ToolBox.UserControls;
using ToolBox.Utils;

namespace ToolBox.Tools
{
    public class LightSwitch : Tool
    {
        private UserControlWithRadioButtons? UserControlWithRadioButtons { get; set; }
        public LightSwitch() : base("Light Switch",
            @$"{FileHelper.TOOLS_SHARED_BASE_PATH}\LightSwitch",
            @$"{FileHelper.TOOLS_LOCAL_BASE_PATH}\LightSwitch")
        {

        }
        public override UserControl? GetUserControl()
        {
            UserControlWithRadioButtons = new UserControlWithRadioButtons();
            return UserControlWithRadioButtons;
        }

        public override void Launch()
        {
            if (UserControlWithRadioButtons == null)
                return;

            var value = UserControlWithRadioButtons.GetValue();
            var switchParam = value ? "-TurnTheLightOn" : "";
            FileHelper.StartPowershellProcessAsAdmin(workingDirectory: LocalPath, command: @$".\LightSwitch.ps1 {switchParam}");
        }
    }
}
