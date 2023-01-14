using ToolBox.Tools.Common;
using ToolBox.Utils;

namespace ToolBox.Tools
{
    public class IsMyPcOn : Tool
    {
        public IsMyPcOn() : base("Is My PC On?",
            @$"{FileHelper.TOOLS_SHARED_BASE_PATH}\IsMyPcOn",
            @$"{FileHelper.TOOLS_LOCAL_BASE_PATH}\IsMyPcOn")
        {

        }
        public override UserControl? GetUserControl()
        {
            return null;
        }

        public override void Launch()
        {
            FileHelper.StartPowershellProcessAsAdmin(workingDirectory: LocalPath, command: @".\IsMyPcOn.ps1");
        }
    }
}
