using Blazor.DriverJs.Enums;
using Blazor.DriverJs.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.DriverJs;

public partial class DriverJsPopover : IDisposable
{
    private ElementReference _ref;

    [CascadingParameter] public DriverStore? Store { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        Model.Element = _ref;
        if (Store == null)
        {
            throw new Exception("DriverJsPopover not inside driver store");
        }
        if (firstRender) Store.AddPopover(Step, Model);
        base.OnAfterRender(firstRender);
    }

    private void UpdateShowButtons()
    {
        var buttons = new List<string>();
        
        if (ShowNextButton) buttons.Add(DriverJsDefaultValues.ButtonNext);
        if (ShowCloseButton) buttons.Add(DriverJsDefaultValues.ButtonClose);
        if (ShowPreviousButton) buttons.Add(DriverJsDefaultValues.ButtonPrevious);

        Model.ShowButtons = buttons.Count > 0 ? buttons.ToArray() : null;
    }

    private void UpdateDisableButtons()
    {
        var buttons = new List<string>();
        
        if (DisableCloseButton) buttons.Add(DriverJsDefaultValues.ButtonClose);
        if (DisableNextButton) buttons.Add(DriverJsDefaultValues.ButtonNext);
        if (DisablePreviousButton) buttons.Add(DriverJsDefaultValues.ButtonPrevious);

        Model.DisableButtons = buttons.Count > 0 ? buttons.ToArray() : null;
    }

    public void Dispose()
    {
        Store?.RemovePopover(Step);
    }
}