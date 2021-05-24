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

    var provider = new DevExpress.fileManagement.RemoteFileSystemProvider({
        endpointUrl: "https://localhost:44341/api/file-manager-file-system"
    })

    filemanager = $(container).dxFileManager(
        {
            fileSystemProvider: provider,
            currentPath: "Widescreen",
            "permissions": {
                "copy": true,
                "create": true,
                "download": true,
                "move": true,
                "rename": true
            },
            onSelectedFileOpened: function (e) {
                var popup = $("#photo-popup").dxPopup("instance");
                popup.option({
                    "title": e.file.name,
                    "contentTemplate": "<img src=\"" + e.file.dataItem.url + "\" class=\"photo-popup-image\" />"
                });
                popup.show();
            }

        });

    $("#photo-popup").dxPopup({
        maxHeight: 600,
        closeOnOutsideClick: true,
        onContentReady: function (e) {
            var $contentElement = e.component.content();
            $contentElement.addClass("photo-popup-content");
        }
    });
}
