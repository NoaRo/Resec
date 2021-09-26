// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function savefile(text) {
    var fileName = document.getElementById('saveAsName').value;
    $.ajax({
        type: "POST",
        url: "/SaveFile",
        data: { 'text': text, 'name': fileName},
        success: function (dataString) {
            $('#files_list').html(dataString);
        }
    })
}
function loadFileAsText() {
    var fileToLoad = document.getElementById("fileToLoad").files[0];
    document.getElementById('saveAsName').value = fileToLoad.name;

    var fileReader = new FileReader();
    fileReader.onload = function (fileLoadedEvent) {
        var textFromFileLoaded = fileLoadedEvent.target.result;
        CKEDITOR.instances.editorForFiles.setData("<!DOCTYPE html><html><body>" + textFromFileLoaded + "</body></html>");
    };
    fileReader.readAsText(fileToLoad, "UTF-8");
}


function removeFile(fileName) {

    $.ajax({
        type: "DELETE",
        url: "/RemoveFile/" + fileName,
        success: function () {
            getAllfilesInTheServer();
        }
    })
}


function getAllfilesInTheServer() {
    $.ajax({
        type: "GET",
        url: "/GetAllfilesInTheServer",
        success: function (data) {
            const ul = document.getElementById("files_list");
            ul.innerHTML = "";
            const liLists = data.list.map(path => {
                path = path.replace(/^.*[\\\/]/, '');
                const li = document.createElement("li");
                const span = document.createElement("span");
                span.innerHTML = path;

                const editBtn = document.createElement("button");
                editBtn.innerHTML = "Edit"
                editBtn.addEventListener("click", () => getFileContent(path));

                const deleteBtn = document.createElement("button");
                deleteBtn.innerHTML = "Remove"
                deleteBtn.addEventListener("click", () => removeFile(path));

                li.append(span);
                li.append(editBtn);
                li.append(deleteBtn);
                return li;
            })
            liLists.forEach(li => {
                ul.append(li)
            })
        }
    })
}

function getFileContent(fileName) {
        $.ajax({
        type: "GET",
            url: "/GetFileContent/" + fileName,
        success: function (content) {
            CKEDITOR.instances.editorForFiles.setData("<!DOCTYPE html><html><body>" + content + "</body></html>");
            document.getElementById('saveAsName').value = fileName;

        }
    })
}



