using ToolBox.Tools.Common;
using ToolBox.Utils;

namespace ToolBox.Tools
{
    public class IsMyPcOn : Tool
    {
        public IsMyPcOn(DirectoryCopier.ProgressChangeCallback updateProgressBarPercentage,
            DirectoryCopier.CompletedCallback removeProgressBar)
            
            : base("Is My PC On?",
            @$"{FileHelper.TOOLS_SHARED_BASE_PATH}\IsMyPcOn",
            @$"{FileHelper.TOOLS_LOCAL_BASE_PATH}\IsMyPcOn",
            updateProgressBarPercentage,
            removeProgressBar)
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
