// Does not store any sensitive data
// This is used to track the usage of the tool to help improve it
function trackImportantEvent(eventType, eventAction, eventLabel) {
    gtag('event', eventType, {
        event_category: 'Important',
        event_action: eventAction,
        event_label: eventLabel,
    });
}