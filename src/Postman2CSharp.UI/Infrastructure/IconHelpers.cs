using Icons = MudBlazor.Icons.Material.Filled;

namespace Postman2CSharp.UI.Infrastructure
{
    public static class IconHelpers
    {
        public static string Folder(bool opened)
        {
            return opened ? Icons.FolderOpen : Icons.Folder;
        }
    }
}
