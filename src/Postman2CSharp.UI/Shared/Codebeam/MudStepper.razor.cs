using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using MudExtensions;
using System.ComponentModel;

namespace CodeBeamMudExtensions
{
    public enum StepStatus
    {
        [Description("continued")]
        Continued,
        [Description("completed")]
        Completed,
        [Description("skipped")]
        Skipped,
    }

    public enum HeaderTextView
    {
        [Description("none")]
        None,
        [Description("only-active-text")]
        OnlyActiveText,
        [Description("new-line")]
        NewLine,
        [Description("all")]
        All,
    }

    public enum StepChangeDirection
    {
        [Description("none")]
        None = 0,
        [Description("forward")]
        Forward,
        [Description("backward")]
        Backward
    }

    public partial class MudStepper : MudComponentBase
    {
        MudAnimate? _animate;
        Guid _animateGuid = Guid.NewGuid();

        protected string HeaderClassname => new CssBuilder("d-flex align-center mud-stepper-header gap-4 pa-2")
            .AddClass("mud-ripple", DisableRipple == false && Linear == false)
            .AddClass("cursor-pointer mud-stepper-header-non-linear", Linear == false)
            .AddClass("flex-column", HeaderTextView == HeaderTextView.NewLine)
            .Build();

        protected string ContentClassname => new CssBuilder($"mud-stepper-ani-{_animateGuid.ToString()}")
            .AddClass(ContentClass)
            .Build();

        protected string GetDashClassname(MudStep step)
        {
            return new CssBuilder("mud-stepper-header-dash flex-grow-1 mx-auto")
                .AddClass("mud-stepper-header-dash-completed", step.Status != StepStatus.Continued)
                //.AddClass("mud-stepper-header-dash-vertical", Vertical)
                .AddClass("mt-5", HeaderTextView == HeaderTextView.NewLine)
                //.AddClass("dash-tiny", Vertical && ActiveIndex != Steps.IndexOf(step))
                .AddClass($"mud-stepper-border-{Color.ToDescriptionString()}")
                .Build();
        }

        internal int ActiveIndex { get; set; }

        /// <summary>
        /// Provides CSS classes for the step content.
        /// </summary>
        [Parameter]
        public string? ContentClass { get; set; }

        /// <summary>
        /// Provides CSS styles for the step content.
        /// </summary>
        [Parameter]
        public string? ContentStyle { get; set; }

        /// <summary>
        /// If true, the header can not be clickable and users can step one by one.
        /// </summary>
        [Parameter]
        public bool Linear { get; set; }

        /// <summary>
        /// If true, disables ripple effect when click on step headers.
        /// </summary>
        [Parameter]
        public bool DisableRipple { get; set; }

        /// <summary>
        /// If true, disables the default animation on step changing.
        /// </summary>
        [Parameter]
        public bool DisableAnimation { get; set; }

        /// <summary>
        /// If true, disables built-in "completed"/"skipped" step result indictors shown in the actions panel.
        /// </summary>
        [Parameter]
        public bool DisableStepResultIndicator { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool MobileView { get; set; }

        /// <summary>
        /// If true, a linear loading indicator shows under the header.
        /// </summary>
        [Parameter]
        public bool Loading { get; set; }

        /// <summary>
        /// A static content that always show with all steps.
        /// </summary>
        [Parameter]
        public RenderFragment? StaticContent { get; set; }

        /// <summary>
        /// If true, action buttons have icons instead of text to gain more space.
        /// </summary>
        [Parameter]
        public bool IconActionButtons { get; set; } = false;

        /// <summary>
        /// The predefined Mud color for header and action buttons.
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// The variant for header and action buttons.
        /// </summary>
        [Parameter]
        public Variant Variant { get; set; }

        /// <summary>
        /// Choose header text view. Default is all.
        /// </summary>
        [Parameter]
        public HeaderTextView HeaderTextView { get; set; } = HeaderTextView.All;

        // TODO
        //[Parameter]
        //public bool Vertical { get; set; }

        /// <summary>
        /// The child content where MudSteps should be inside.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Overrides the action buttons (previous, next etc.) with custom render fragment.
        /// </summary>
        [Parameter]
        public RenderFragment? ActionContent { get; set; }

        [Parameter]
        public EventCallback<int> ActiveStepChanged { get; set; }

        /// <summary>
        /// Provides the step direction, active step index. If the step isn't occuring during a step, it determines if
        /// the next and back button are disabled.
        /// Expects a bool in return to indicate if the step is valid. If the step isn't valid the step is aborted.
        /// </summary>
        [Parameter]
        public Func<StepChangeDirection, int, Task<bool>>? ValidateStep { get; set; }

        [Parameter]
        public Func<int, Task<bool>>? AllowNext { get; set; }

        [Parameter]
        public Func<int, Task<bool>>? AllowBack { get; set; }

        [Parameter] public bool UseStepBorder { get; set; }

        public string? StepBorderStyle => UseStepBorder ? "border-color: rgb(208 213 221 / 1); border-width: 1px; padding: 0 10px; background: black;" : null;

        List<MudStep> _steps = new();
        List<MudStep> _allSteps = new();
        public List<MudStep> Steps
        {
            get => _steps;
            protected set
            {
                if (_steps.Equals(value))
                {
                    return;
                }
                if (_steps.Select(x => x.GetHashCode()).Contains(value.GetHashCode()))
                {
                    return;
                }
                _steps = value;
            }
        }

        public async Task TriggerReRender()
        {
            await InvokeAsync(StateHasChanged);
        }

        internal void AddStep(MudStep step)
        {
            _allSteps.Add(step);
            if (step.IsResultStep == false)
            {
                Steps.Add(step);
            }

            StateHasChanged();
        }

        internal void RemoveStep(MudStep step)
        {
            Steps.Remove(step);
            _allSteps.Remove(step);
            StateHasChanged();
        }

        public async Task SetActiveIndex(MudStep step)
        {
            if (_animate != null)
            {
                await _animate.Refresh();
            }
            ActiveIndex = Steps.IndexOf(step);
            await ActiveStepChanged.InvokeAsync(ActiveIndex);
        }

        public async Task SetActiveIndexByIndex(int index, bool validate = true)
        {
            var count = index - ActiveIndex;
            var stepChangeDirection = (
                count == 0 ? StepChangeDirection.None :
                count >= 1 ? StepChangeDirection.Forward :
                StepChangeDirection.Backward
            );

            if (validate)
            {
                var valid = ValidateStep == null || await ValidateStep.Invoke(stepChangeDirection, ActiveIndex);
                if (!valid)
                {
                    return;
                }
            }

            if (_animate != null)
            {
                await _animate.Refresh();
            }
            ActiveIndex = index;
           
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            ActiveStepChanged.InvokeAsync(ActiveIndex);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public async Task Step(int stepAmount, bool validate = true)
        {
            var stepChangeDirection = (
                stepAmount == 0 ? StepChangeDirection.None :
                    stepAmount >= 1 ? StepChangeDirection.Forward :
                        StepChangeDirection.Backward
            );

            if (validate)
            {
                var valid = ValidateStep == null || await ValidateStep.Invoke(stepChangeDirection, ActiveIndex);
                if (!valid)
                {
                    return;
                }
            }

            int oldIndex = ActiveIndex;
            if (_animate != null)
            {
                await _animate.Refresh();
            }

            if (ActiveIndex == Steps.Count - 1 && HasResultStep() == false && 0 < stepAmount)
            {
                return;
            }
            if (ActiveIndex + stepAmount < 0)
            {
                ActiveIndex = 0;
            }
            else if (ActiveIndex == Steps.Count - 1 && IsAllStepsCompleted() == false && 0 < stepAmount)
            {
                var nextNotCompletedStep = Steps.FirstOrDefault(x => x.Status == StepStatus.Continued);
                if (nextNotCompletedStep != null)
                {
                    ActiveIndex = Steps.IndexOf(nextNotCompletedStep);
                }
            }
            else
            {
                ActiveIndex += stepAmount;
            }

            if (oldIndex != ActiveIndex)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                ActiveStepChanged.InvokeAsync(ActiveIndex);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public async Task CompleteStep()
        {
            var valid = ValidateStep == null || await ValidateStep.Invoke(StepChangeDirection.Forward, ActiveIndex);
            if (!valid)
            {
                return;
            }

            if (Steps.Count == 0) return;

            try
            {
                Steps[ActiveIndex].SetStatus(StepStatus.Completed);

                await Step(1, false);

            }
            catch (Exception)
            {
                return;
            }
        }

        private async Task SkipStep()
        {
            var valid = ValidateStep == null || await ValidateStep.Invoke(StepChangeDirection.Forward, ActiveIndex);
            if (!valid)
            {
                return;
            }

            Steps[ActiveIndex].SetStatus(StepStatus.Skipped);
            await Step(1, false);
        }
        protected bool IsStepActive(MudStep step)
        {
            return Steps.IndexOf(step) == ActiveIndex;
        }

        protected internal bool ShowResultStep()
        {
            if (IsAllStepsCompleted() && ActiveIndex == Steps.Count)
            {
                return true;
            }

            return false;
        }

        protected internal bool HasResultStep()
        {
            return _allSteps.Any(x => x.IsResultStep);
        }

        public bool IsAllStepsCompleted()
        {
            return Steps.All(x => x.Status != StepStatus.Continued);
        }
    }
}
