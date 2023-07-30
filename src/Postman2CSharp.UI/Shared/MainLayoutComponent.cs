using Microsoft.AspNetCore.Components;
using MudBlazor;
using Postman2CSharp.UI.Models;
using Postman2CSharp.UI.Services;

namespace Postman2CSharp.UI.Shared
{
    public class MainLayoutComponent : ComponentBase
    {
        [Inject] private Lazy<IDialogService?> DialogService { get; set; } = null!;
        [Inject] protected Lazy<Interop> Interop { get; set; } = null!;
        [Inject] protected Lazy<ISnackbar?> SnackBar { get; set; } = null!;
        [Inject] protected Lazy<Navigate> Navigate { get; set; } = null!;
        [Inject] protected Lazy<TabsService> TabsService { get; set; } = null!;
        [CascadingParameter] protected List<ApiCollection>? ApiCollections { get; set; }
        [CascadingParameter] protected MainLayout Layout { get; set; } = null!;
    }
}
