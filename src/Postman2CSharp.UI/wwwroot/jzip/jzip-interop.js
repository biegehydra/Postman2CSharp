window.createZipAndDownload = async function (fileName, sourceCodeDict, fileNamePathDict) {
    var zip = new JSZip();

    for (let fileName in sourceCodeDict) {
        if (sourceCodeDict.hasOwnProperty(fileName)) {
            const fileContent = sourceCodeDict[fileName];
            const filePath = fileNamePathDict[fileName];
            if (filePath) {
                // Create a subfolder and add the file to it
                zip.folder(filePath).file(fileName, fileContent);
            } else {
                // No subfolder specified, add the file to the root
                zip.file(fileName, fileContent);
            }
        }
    }

    const content = await zip.generateAsync({ type: "blob" });
    const url = URL.createObjectURL(content);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName + ".zip";
    link.click();
}