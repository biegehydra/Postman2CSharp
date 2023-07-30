window.triggerPrism = () => {
    Prism.highlightAll();
};

let prismObserver;

window.setupPrismObserver = function () {
    prismObserver = new MutationObserver(function (mutations) {
        mutations.forEach(function (mutation) {
            // If new nodes are added and they contain 'code' elements, highlight them
            if (mutation.addedNodes) {
                mutation.addedNodes.forEach(function (node) {
                    if (node.nodeType === Node.ELEMENT_NODE && (node.className === 'language-csharp' || node.className === 'language-json')) {
                        Prism.highlightElement(node);
                    }
                });
            }
        });
    });

    // Observe the document and all its child elements for changes
    prismObserver.observe(document, { childList: true, subtree: true });
}

window.removePrismObserver = function () {
    if (prismObserver) {
        prismObserver.disconnect();
        prismObserver = null;
    }
}

window.registerButtons = function () {
    registeredCodeBlocks[id] = dotnetObjectRef;
}