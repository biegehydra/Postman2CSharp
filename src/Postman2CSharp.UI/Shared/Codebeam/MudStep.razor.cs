﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace CodeBeamMudExtensions
{
    public partial class MudStep : MudComponentBase, IDisposable
    {
        protected string Classname => new CssBuilder()
            .AddClass(Class)
            .Build();

        private bool StepIsNotActive => ((MudStepper.ActiveIndex < MudStepper.Steps.Count && MudStepper.Steps[MudStepper.ActiveIndex] != this) || (MudStepper.ShowResultStep() && !IsResultStep)) || (IsResultStep && !MudStepper.ShowResultStep());

        [CascadingParameter]
        protected MudStepper MudStepper { get; set; } = null!;

        /// <summary>
        /// Step text to show on header.
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        StepStatus _status = StepStatus.Continued;
        /// <summary>
        /// The step status flag to show step is continued, skipped or completed. Do not set it directly unless you know what you do exactly.
        /// </summary>
        [Parameter]
        public StepStatus Status
        {
            get => _status;
            set
            {
                if (_status == value)
                {
                    return;
                }
                _status = value;
                StatusChanged.InvokeAsync(_status);
            }
        }

        /// <summary>
        /// If true the step is skippable.
        /// </summary>
        [Parameter]
        public bool Optional { get; set; }

        /// <summary>
        /// If true, the step show when the stepper is completed. There should be only one result step.
        /// </summary>
        [Parameter]
        public bool IsResultStep { get; set; } = false;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Fires when step status changed.
        /// </summary>
        [Parameter]
        public EventCallback<StepStatus> StatusChanged { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MudStepper.AddStep(this);
        }

        protected internal void SetStatus(StepStatus status)
        {
            Status = status;
        }

        public void Dispose()
        {
            try
            {
                MudStepper?.RemoveStep(this);
            }
            catch (Exception) { }
        }

    }
}