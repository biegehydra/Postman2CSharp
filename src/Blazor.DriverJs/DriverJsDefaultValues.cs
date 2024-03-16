namespace Blazor.DriverJs;

public static class DriverJsDefaultValues
{
    public const string ButtonNext = "next";
    public const string ButtonClose = "close";
    public const string ButtonPrevious = "previous";

    public const string ButtonNextText = "Next";
    public const string ButtonDoneText = "Done";
    public const string ButtonPreviousText = "Previous";
    
    public const string DefaultSide = "left";
    public const string DefaultAlign = "start";
    public const string OverlayColor = "black";

    public const bool Animate = true;
    public const bool SmoothScroll = false;
    public const bool AllowClose = true;
    public const bool AllowKeyboardControl = true;
    public const bool DisableActiveInteraction = false;
    public const bool ShowProgress = false;
    
    public const double OverlayOpacity = 0.5f;
    public const double StagePadding = 10;
    public const double StageRadius = 5;
    public const double PopoverOffset = 10;

    public static readonly string[] ShowButtons = {
        ButtonClose,
        ButtonNext,
        ButtonPrevious
    };
}