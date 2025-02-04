var action = null;
var isModalOpen = null;
var editFuncao = (obj) => {

    $('#codigoCargo').val(obj.id);
    $('#nomeCargo').empty().html(`<h5><b>Função:</b> ${obj.nomeCargo}</h5> `);

    $('#modalDocumentoCargo').modal(
        {
            dismissible: false,
            onOpenStart: () => { getDocumentCargo(obj); }
        }).modal('open');
    
}

var getDocumentCargo = (obj) => {

    $('#DivRecordDocumentoComplementar').empty().html('<div class="progress"><div class="indeterminate"></div></div>');
    $('#cd_documentoComplementar_id').val(obj.id);
    action = 'funcoes/typeDocument/' + (obj.pagina == undefined ? 1 : obj.pagina);

     $.ajax({
        url: action,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
         method: 'get',
         data: { id: obj.id, origem: obj.origem, cd_empresa: $('#cd_empresa').val() }
    }).done(function (response) {
        $("#DivRecordDocumentoComplementar").empty().html(response);
        
    }).fail(function (dataError) {
        $("#DivRecordDocumentoComplementar").empty().html("ocorreu um erro" + dataError);
    });
}

var vincularFuncaoAoDocumento = (obj) => {

    var comandoDocumento = {
        cd_cargo_id: $('#codigoCargo').val(),
        cd_documento_id: obj.cd_documento_id,
        cd_empresa: $('#cd_empresa').val(),
        vinculado: obj.checked,
        status: true
    }

    $.ajax({
        type: 'POST',
        url: 'Funcoes/vincular-documento-cargo',
        dataType: 'json',
        data: comandoDocumento 
    })
        .done(function (data) {

            if (data.success == false) {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - Ocorreu um erro na execução', classes: 'red darken-2 rounded'
                });
            }
        })
        .fail(function (data) {
            M.toast({
                html: '<i class="material-icons white-text">cancel</i>Ocorreu um erro ao tentar vincular/desvincular o documento da função.', classes: 'red darken-2 rounded'
            });
        });

}

$('#btnCloseModaDdocumentCargo').on('click', () => {
    $('#cd_empresa').prop('selectedIndex', 0);
    $('select').formSelect();
})

$('#cd_Origem').on('change',(event) => {
    var obj = { id: $('#cd_documentoComplementar_id').val(), origem: event.currentTarget.value };
    getDocumentCargo(obj);
})

$('#cd_empresa').on('change', (event) => {
    var obj = { id: $('#cd_documentoComplementar_id').val(), origem: $('#cd_Origem').val() };
    getDocumentCargo(obj);
})
