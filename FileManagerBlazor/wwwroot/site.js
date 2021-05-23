function windowMinWidthMatchesQuery(dotNetHelper) {
    var pendingCall;
    var match = window.matchMedia("(min-width: 1200px)")
    handleMinWidthQuery(match).then(function () { match.addListener(handleMinWidthQuery) });
    function handleMinWidthQuery(queryMatch) {
        return (pendingCall || Promise.resolve(true))
            .then(function () {
                return pendingCall = new Promise(function (resolve, reject) {
                    dotNetHelper.invokeMethodAsync('OnWindowMinWidthQueryChanged', queryMatch.matches).then(resolve).catch(reject);
                });
            });
    }
}


var filemanager;

function renderDevExtremeControls(filemanagerContainerID) {

    renderFileManager(filemanagerContainerID);
}

function renderFileManager(container) {
    filemanager = $(container).dxFileManager({
        fileSystemProvider: new DevExpress.fileManagement.RemoteFileSystemProvider({
            endpointUrl: "https://localhost:44341/api/file-manager-file-system"
        }),
    });
}
