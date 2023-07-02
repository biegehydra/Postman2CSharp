using Microsoft.Extensions.Primitives;

namespace Postman2CSharp.Wasm.Models
{
    public class SiteSettings
    {
        private static SiteSettings _instance = new() { Version = CurrentVersion };
        public static SiteSettings Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                OnSettingsChanged?.Invoke(_instance);
            }
        }
        public static event Action<SiteSettings>? OnSettingsChanged;
        private bool _showSnackBarPopups = true;
        public bool ShowSnackbarPopups
        {
            get => _showSnackBarPopups;
            set
            {
                _showSnackBarPopups = value;
                OnSettingsChanged?.Invoke(this);
            }
        }

        private bool _saveCollectionsToLocalStorage = true;

        public bool SaveCollectionsToLocalStorage
        {
            get => _saveCollectionsToLocalStorage;
            set
            {
                _saveCollectionsToLocalStorage = value;
                OnSettingsChanged?.Invoke(this);
            }
        }

        private bool _showDownloadInTreeView;

        public bool ShowDownloadInTreeView
        {
            get => _showDownloadInTreeView;
            set
            {
                _showDownloadInTreeView = value;
                OnSettingsChanged?.Invoke(this);
            }
        }
        public int TotalApiClientsGenerated { get; set; }
        public string? Version { get; set; }

        public const string CurrentVersion = "0.2";
    }
}
