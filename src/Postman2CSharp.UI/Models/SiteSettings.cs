namespace Postman2CSharp.UI.Models;

public class SiteSettings
{
    public bool ShowSnackbarPopups { get; set; } = true;

    public bool SaveCollectionsToLocalStorage { get; set; } = true;

    public bool ShowDownloadInTreeView { get; set; }

    public int TotalApiClientsGenerated { get; set; }
    public string? Version { get; set; }

    public SiteSettings Clone()
    {
        return (SiteSettings) MemberwiseClone();
    }
}