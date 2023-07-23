let jsonEditors = {};
function initJsonEditor(dotNetObjRef, id) {
    if (jsonEditors[id] != null) return;
    let jsonEditor = ace.edit(id);
    jsonEditor.setTheme("ace/theme/monokai");
    jsonEditor.session.setMode("ace/mode/json");
    jsonEditor.setShowPrintMargin(false);

    jsonEditor.on('change', function () {
        var json = jsonEditor.getValue();
        dotNetObjRef.invokeMethodAsync('CheckJsonValidity', json);
    });

    autoResize(jsonEditor);
    jsonEditors[id] = jsonEditor;
}

function getJsonEditorValue(id) {
    return jsonEditors[id] ? jsonEditors[id].getValue() : null;
}

function setJsonEditorValue(id, value) {
    if (jsonEditors[id]) {
        jsonEditors[id].setValue(value);
        jsonEditors[id].clearSelection();
    }
}

function resetJsonEditor(id) {
    if (jsonEditors[id]) {
        jsonEditors[id].setValue("");
    }
}

function destroyJsonEditor(id) {
    if (jsonEditors[id]) {
        jsonEditors[id].destroy();
        delete jsonEditors[id];
    }
}

function autoResize(editor) {
    if (!editor || !editor.container) {
        return;
    }
    var height = window.innerHeight;
    editor.container.style.height = `${(height * .6)}px`;
    editor.resize();
}

window.addEventListener('resize', function () {
    for (let id in jsonEditors) {
        autoResize(jsonEditors[id]);
    }
});