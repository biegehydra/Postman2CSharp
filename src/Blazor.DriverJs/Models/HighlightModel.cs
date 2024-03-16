using Microsoft.AspNetCore.Components;

namespace Blazor.DriverJs.Models;

public class HighlightModel
{
    public ElementReference Element { get; set; }
    
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

    public HighlightModel(ElementReference reference)
    {
        Element = reference;
    }
}