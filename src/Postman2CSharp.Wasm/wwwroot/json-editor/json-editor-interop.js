let jsonEditor = null;
function initJsonEditor(dotNetObjRef) {
    if (jsonEditor != null) return;
    jsonEditor = ace.edit("json-editor");
    jsonEditor.setTheme("ace/theme/monokai");
    jsonEditor.session.setMode("ace/mode/json");
    jsonEditor.setValue("{\n    \"put\" : \"your\",\n    \"json\" : \"here\",\n    \"to\" : \"be converted\"\n}");
    jsonEditor.clearSelection();
    jsonEditor.setShowPrintMargin(false);

    jsonEditor.on('change', function () {
        var json = jsonEditor.getValue();

        // Call the C# function
        dotNetObjRef.invokeMethodAsync('CheckJsonValidity', json);
    });

    autoResize(jsonEditor);
}
function getJsonEditorValue() {
    return jsonEditor.getValue();
}
function resetJsonEditor() {
    jsonEditor.setValue("");
}

function destroyJsonEditor() {
    jsonEditor = null;
}

function autoResize(editor) {
    if (!editor || !editor.container) {
        return;
    }
    var height = window.innerHeight;
    editor.container.style.height = `${(height * .6)}px`;
    editor.resize();
}

// Call autoResize whenever the window is resized
window.addEventListener('resize', function () {
    autoResize(jsonEditor);
});