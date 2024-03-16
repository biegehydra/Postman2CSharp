using System.ComponentModel;
using Blazor.DriverJs.Enums;
using Blazor.DriverJs.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.DriverJs;

public partial class DriverJsPopover
{
    #region Props

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public PopoverModel Model { get; set; } = new();

    [Parameter, EditorRequired] public int Step { get; set; }

    [Parameter]
    public string? Class
    {
        get => Model.PopoverClass;
        set => Model.PopoverClass = value;
    }

    #region DriverJsParameters

    /// <summary>
    /// Title show in the popover.
    /// You can use HTML in these. Also, you can omit one of these to show only the other.
    /// </summary>
    [Parameter]
    public string? Title
    {
        get => Model.Title;
        set => Model.Title = value;
    }

    /// <summary>
    /// Descriptions show in the popover.
    /// You can use HTML in these. Also, you can omit one of these to show only the other.
    /// </summary>
    [Parameter]
    public string? Description
    {
        get => Model.Description;
        set => Model.Description = value;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public DriverSide Side
    {
        set => Model.Side = Enum.GetName(value)?.ToLower() ?? DriverJsDefaultValues.DefaultSide;
    }

    /// <summary>
    /// Custom class to add to the popover element.
    /// This can be used to style the popover.
    /// </summary>
    [Parameter]
    public string? PopoverClass
    {
        get => Model.PopoverClass;
        set => Model.PopoverClass = value;
    }

    [Parameter]
    public DriverAlign Align
    {
        set => Model.Align = Enum.GetName(value)?.ToLower() ?? DriverJsDefaultValues.DefaultAlign;
    }

    /// <summary>
    /// Template for the progress text. You can use the following placeholders in the template:
    /// - {{current}}: The current step number
    /// - {{total}}: Total number of steps
    /// </summary>
    [Parameter]
    public string? ProgressText
    {
        get => Model.ProgressText;
        set => Model.ProgressText = value;
    }

    /// <summary>
    /// Whether to show the progress text in popover. (default: false)
    /// </summary>
    [Parameter]
    public bool ShowProgress
    {
        get => Model.ShowProgress;
        set => Model.ShowProgress = value;
    }

    #endregion

    #region DisableButtons

    private bool _disableCloseButton;

    /// <summary>
    /// Disable close button
    /// </summary>
    [Parameter]
    [DefaultValue(false)]
    public bool DisableCloseButton
    {
        get => _disableCloseButton;
        set
        {
            _disableCloseButton = value;
            UpdateDisableButtons();
        }
    }

    private bool _disableNextButton;

    /// <summary>
    /// Disable next button
    /// </summary>
    [Parameter]
    [DefaultValue(false)]
    public bool DisableNextButton
    {
        get => _disableNextButton;
        set
        {
            _disableNextButton = value;
            UpdateDisableButtons();
        }
    }

    private bool _disablePreviousButton;

    /// <summary>
    /// Disable previous button
    /// </summary>
    [Parameter]
    [DefaultValue(false)]
    public bool DisablePreviousButton
    {
        get => _disablePreviousButton;
        set
        {
            _disablePreviousButton = value;
            UpdateDisableButtons();
        }
    }

    #endregion

    #region ShowButtons

    private bool _showCloseButtons = true;

    /// <summary>
    /// Show close button
    /// </summary>
    [Parameter]
    public bool ShowCloseButton
    {
        get => _showCloseButtons;
        set
        {
            _showCloseButtons = value;
            UpdateShowButtons();
        }
    }

    private bool _showNextButton = true;

    /// <summary>
    /// Show next button
    /// </summary>
    [Parameter]
    public bool ShowNextButton
    {
        get => _showNextButton;
        set
        {
            _showNextButton = value;
            UpdateShowButtons();
        }
    }

    private bool _showPreviousButton = true;

    /// <summary>
    /// Show previous button
    /// </summary>
    [Parameter]
    public bool ShowPreviousButton
    {
        get => _showPreviousButton;
        set
        {
            _showPreviousButton = value;
            UpdateShowButtons();
        }
    }

    #endregion

    #endregion
}