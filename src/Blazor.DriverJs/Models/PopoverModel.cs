using Microsoft.AspNetCore.Components;

namespace Blazor.DriverJs.Models;

public class PopoverModel
{
    public ElementReference? Element { get; set; }
    public string? ElementSelector { get; set; }

    /// <summary>
    /// Title show in the popover.
    /// You can use HTML in these. Also, you can omit one of these to show only the other.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Descriptions show in the popover.
    /// You can use HTML in these. Also, you can omit one of these to show only the other.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// values: top, right, bottom, left
    /// </summary>
    public string Side { get; set; } = DriverJsDefaultValues.DefaultSide;

    /// <summary>
    /// values: start, center, end
    /// </summary>
    public string Align { get; set; } = DriverJsDefaultValues.DefaultAlign;

    /// <summary>
    /// values: next, previous, close
    /// </summary>
    public string[]? ShowButtons { get; set; }

    /// <summary>
    /// values: next, previous, close
    /// </summary>
    public string[]? DisableButtons { get; set; }

    /// <summary>
    /// Whether to show the progress text in popover. (default: false)
    /// </summary>
    public bool ShowProgress { get; set; }

    /// <summary>
    /// Template for the progress text. You can use the following placeholders in the template:
    /// - {{current}}: The current step number
    /// - {{total}}: Total number of steps
    /// </summary>
    public string? ProgressText { get; set; }

    /// <summary>
    /// Custom class to add to the popover element.
    /// This can be used to style the popover.
    /// </summary>
    public string? PopoverClass { get; set; }
}