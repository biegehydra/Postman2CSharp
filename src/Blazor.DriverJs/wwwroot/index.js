const driver = window.driver.js.driver;

export function highlight(model) {
    const driverObj = driver();

    driverObj.highlight({
        element: model.element,
        popover: {
            title: model.title,
            description: model.description,
            side: model.side,
            align: model.align,
            popoverClass: model.popoverClass,
        }
    })
}

export function startDrive(model, dotObj) {
    const driveObj = driver({
        animate: model.configuration.animate,
        overlayColor: model.configuration.overlayColor,
        smoothScroll: model.configuration.smoothScroll,
        allowClose: model.configuration.allowClose,
        overlayOpacity: model.configuration.overlayOpacity,
        stagePadding: model.configuration.stagePadding,
        stageRadius: model.configuration.stageRadius,
        allowKeyboardControl: model.configuration.allowKeyboardControl,
        disableActiveInteraction: true,
        popoverClass: model.configuration.popoverClass,
        popoverOffset: model.configuration.popoverOffset,
        showButtons: model.configuration.showButtons,
        disableButtons: model.configuration.disableButtons,
        showProgress: model.configuration.showProgress,
        progressText: model.configuration.progressText,
        nextBtnText: model.configuration.nextButtonText,
        prevBtnText: model.configuration.previousButtonText,
        doneBtnText: model.configuration.doneButtonText,

        onHighlightStarted: (_, step) => {
            dotObj.invokeMethodAsync("HighlightStartedHandler", driveObj.getActiveIndex());
        },

        onHighlighted: (_, step) => {
            dotObj.invokeMethodAsync("HighlightedHandler", driveObj.getActiveIndex());
        },

        onDeselected: (_, step) => {
            dotObj.invokeMethodAsync("DeselectedHandler", driveObj.getActiveIndex());
        },
        
        steps: model.steps.map(x => ({
            element: x.element ?? x.elementSelector,
            popover: {
                title: x.title,
                description: x.description,

                showButtons: x.showButtons,
                disableButtons: x.disableButtons,
                side: x.side,
                align: x.align,

                showProgress: x.showProgress,

                progressText: x.progressText,
                popoverClass: x.popoverClass,
                
                onNextClick: (_, step) => {
                    dotObj.invokeMethodAsync("NextClickHandler", driveObj.getActiveIndex());
                    driveObj.moveNext();
                },

                onPrevClick: (_, step) => {
                    dotObj.invokeMethodAsync("PreviousClickHandler", driveObj.getActiveIndex());
                    driveObj.movePrevious();
                },

                onCloseClick: (_, step) => {
                    dotObj.invokeMethodAsync("CloseClickHandler", driveObj.getActiveIndex());
                    driveObj.destroy();
                }
            }
        }))
    })

    driveObj.drive();
}
