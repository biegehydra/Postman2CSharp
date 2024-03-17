namespace Postman2CSharp.UI.Models;

public class SiteSettings
{
    public bool ShowSnackbarPopups { get; set; } = true;

    public bool SaveCollectionsToLocalStorage { get; set; } = true;

    public bool ShowDownloadInTreeView { get; set; }

    public int TotalApiClientsGenerated { get; set; }
    public int TotalApiClientsDownloaded { get; set; }
    public string? Version { get; set; }

    public bool ViewedCollectionTour { get; set; }
    public bool ViewedApiClientTour { get; set; }
    public bool ViewedDuplicateTour { get; set; }
}