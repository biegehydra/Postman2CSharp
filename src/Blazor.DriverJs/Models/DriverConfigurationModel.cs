namespace Blazor.DriverJs.Models;

public class DriverConfigurationModel
{
    public bool ShowProgress { get; set; } = DriverJsDefaultValues.ShowProgress;

    public bool Animate { get; set; } = DriverJsDefaultValues.Animate;

    public bool SmoothScroll { get; set; } = DriverJsDefaultValues.SmoothScroll;

    public bool AllowClose { get; set; } = DriverJsDefaultValues.AllowClose;

    public bool AllowKeyboardControl { get; set; } = DriverJsDefaultValues.AllowKeyboardControl;

    public bool DisableActiveInteraction { get; set; } = DriverJsDefaultValues.DisableActiveInteraction;

    public double OverlayOpacity { get; set; } = DriverJsDefaultValues.OverlayOpacity;

    public double StagePadding { get; set; } = DriverJsDefaultValues.StagePadding;

    public double StageRadius { get; set; } = DriverJsDefaultValues.StageRadius;

    public string OverlayColor { get; set; } = DriverJsDefaultValues.OverlayColor;

    public string? PopoverClass { get; set; }

    public string NextButtonText { get; set; } = DriverJsDefaultValues.ButtonNextText;

    public string PreviousButtonText { get; set; } = DriverJsDefaultValues.ButtonPreviousText;

    public string DoneButtonText { get; set; } = DriverJsDefaultValues.ButtonDoneText;

    public double PopoverOffset { get; set; } = DriverJsDefaultValues.PopoverOffset;

    public string[] ShowButtons { get; set; } = DriverJsDefaultValues.ShowButtons;
    
    public string[]? DisableButtons { get; set; }

    public string? ProgressText { get; set; }
}