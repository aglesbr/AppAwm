var openDocumentPdf = (obj) =>
{
    let action = '../Anexo/loadFile';
    $.ajax({
        type: 'Get',
        url: action,
        contentType: "application/json; charset=utf-8",
        responseType: 'arraybuffer',
        async: true,
        data: { id: JSON.stringify(obj.codigo) }
    })
        .done(function (data) {

            var contentType = "application/pdf";
            const blob = b64toBlob(data, contentType);
            const blobUrl = URL.createObjectURL(blob);

            $("#DivError").css("display", "none");
            $("#objFile").css("display", "");
            $("#btnSearch").trigger('click');
            $("#objFile").prop('data', blobUrl);

        })
        .fail(function (data) {
            $("#DivError").css("display", "");
            $("#DivError").empty().html(data.responseText);
        });


    $('#modalOpenDocument').modal({ dismissible: false }).modal('open');
}


const b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);

        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
}


var updateStatus = (obj) => {

    loading(true);

    $.ajax({
        type: 'PUT',
        url: '/Anexo/updateStatus',
        dataType: "json",
        async: true,
        data: { obj: JSON.stringify(obj) }
    })
        .done(function (data) {

            loading(false);

            if (data.success == true) {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
                });
                $('#btnSearch').trigger('click');
            }
        })
        .fail(function (data) {
            loading(false);

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>&nbsp - ' + data.responseJSON.message, classes: 'red darken-2 rounded'
            });
            $('#btnSearch').trigger('click');
        });

}

