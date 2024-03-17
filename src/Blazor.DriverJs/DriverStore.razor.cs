using System.ComponentModel;
using System.Reflection.PortableExecutable;
using Blazor.DriverJs.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.DriverJs;

public partial class DriverStore : IDisposable
{
    #region Fields

    private DotNetObjectReference<DriverStore> _objRef = null!;

    private bool IsCallbackDrive => OnNextStep.HasDelegate || OnPreviousStep.HasDelegate || OnCloseClick.HasDelegate;

    #endregion

    #region Props

    [Parameter] public SortedList<int, PopoverModel> Popovers { get; set; } = new();

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public EventCallback<int> OnNextStep { get; set; }

    [Parameter] public EventCallback<int> OnPreviousStep { get; set; }

    [Parameter] public EventCallback<int> OnHighlightStarted { get; set; }

    [Parameter] public EventCallback<int> OnHighlighted { get; set; }

    [Parameter] public EventCallback<int> OnDeselected { get; set; }

    [Parameter] public EventCallback OnCloseClick { get; set; }

    [Parameter] public DriverConfigurationModel Configuration { get; set; } = new();

    #endregion

    #region Services

    [Inject] public DriverJs DriverJs { get; set; } = null!;

    #endregion

    #region Override

    protected override void OnInitialized()
    {
        _objRef = DotNetObjectReference.Create(this);
        base.OnInitialized();
    }

    #endregion

    #region Handlers

    public async ValueTask StartDrive()
    {
        await DriverJs.StartDrive(new DriverModel(Popovers.Values, Configuration), _objRef);
    }

    public void AddPopover(int step, PopoverModel model)
    {
        Popovers.Add(step, model);
    }

    public void RemovePopover(int step)
    {
        Popovers.Remove(step);
    }

    #endregion

    #region JsHandlers

    [JSInvokable]
    public void NextClickHandler(int index) => OnNextStep.InvokeAsync(index);

    [JSInvokable]
    public void PreviousClickHandler(int index) => OnPreviousStep.InvokeAsync(index);

    [JSInvokable]
    public void CloseClickHandler(int index) => OnCloseClick.InvokeAsync(index);

    [JSInvokable]
    public void HighlightStartedHandler(int index) => OnHighlightStarted.InvokeAsync(index);

    [JSInvokable]
    public void HighlightedHandler(int index) => OnHighlighted.InvokeAsync(index);

    [JSInvokable]
    public void DeselectedHandler(int index) => OnDeselected.InvokeAsync(index);

    #endregion

    public void Dispose()
    {
        DriverJs.Dispose();
    }
}