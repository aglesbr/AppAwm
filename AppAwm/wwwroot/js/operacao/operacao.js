var openDocumentPdf = (obj) =>
{
    loading(true);

    let action = '../Anexo/loadFile';

    $('#divFile').css('display', 'none');
    $('#divObjPdfIntermitente').css('display', '');

    $('#modalOpenDocument').modal({ dismissible: false }).modal('open');

    $.ajax({
        type: 'Get',
        url: action,
        contentType: "application/json; charset=utf-8",
        responseType: 'arraybuffer',
        async: false,
        data: { id: JSON.stringify(obj.codigo) }
    })
        .done(function (data) {

            var contentType = "application/pdf";
            const blob = b64toBlob(data, contentType);
            const blobUrl = URL.createObjectURL(blob);

            $("#DivError").css('display', 'none');

            setTimeout(() => { $("#divFile").css('display', ''); $('#divObjPdfIntermitente').css('display', 'none'); $("#objFile").prop('data', blobUrl); loading(false); }, 1000);

        })
        .fail(function (data) {
            loading(false);
            $("#DivError").css("display", "");
            $("#DivError").empty().html(data.responseText);
        });


}


var b64toBlob = (b64Data, contentType = '', sliceSize = 1024) => {
    var byteCharacters = atob(b64Data);
    var byteArrays = [];

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
        async: false,
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

$('#openDownload').on('click', (event) => {

    const divDownload = `<div class="row">
        <div class="col s12" >
            <div class="card horizontal card blue-grey z-depth-3">
                <div class="card-image" style="margin-left:5px; margin-top:5px;">
                    <img src="../images/Software.png" style="height:100px; width:100px">
                </div>
                <div class="card-stacked">
                    <div class="card-content white-text">
                        <p>O <b>AppDoc</b>, é o sistema de checagem e conferência documental.<br /> Todos os documentos enviados pelos nossos clientes, são vlidados pela equipe HDDOC.</p>
                    </div>
                    <div class="card-action">
                        <a class="waves-effect waves-light btn-large modal-trigger" onclick="javascript: window.location.href = '/download/downloadFile/1'">
                            <i class="material-icons left" style="font-size:30px;">cloud_download</i>baixar o software
                        </a>
                    </div>
                </div>
            </div>
    </div >
  </div > `

    $.dialog({
        title:'Download ',
        boxWidth: '30%',
        type: 'dark',
        useBootstrap: false,
        content: divDownload,
    });
});