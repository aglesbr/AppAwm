var action = null;
var isModalOpen = null;

var editTipoDocumento = (obj) => {

    if (obj.origem == 1) 
        $('#codigoCargo').val(obj.id);
    else 
        $('#codigoEmpresa').val(obj.id);
    

    $('#titleTipo').empty().html(`<h6><b>${obj.origem == 2 ? 'Empresa' : 'Função'}:</b> ${obj.titleTipo}</h6> `);

    $('#modalDocumentoTipoDocumento').modal(
        {
            dismissible: false,
            onOpenStart: () => { getTipoDocumento(obj); }
        }).modal('open');
    
}

$('#cd_empresa').on('change', (event) => {
    var obj = { pagina: 1, id: $('#codigoCargo').val(), origem: $('#tipoDocumento').val(), cd_empresa_id:  $('#cd_empresa').val() };
    getTipoDocumento(obj);
})

var getTipoDocumento = (obj) => {

    $('#DivRecordDocumentoComplementar').empty().html('<div class="progress"><div class="indeterminate"></div></div>');
    $('#cd_documentoComplementar_id').val(obj.id);
    action = 'documentacao/typeDocument/' + (obj.pagina == undefined ? 1 : obj.pagina);

     $.ajax({
        url: action,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
         method: 'get',
         data: { id: obj.id, cd_empresa_id: obj.cd_empresa_id ?? 0, origem: obj.origem }
    }).done(function (response) {
        $("#DivRecordDocumentoComplementar").empty().html(response);
        
    }).fail(function (dataError) {
        $("#DivRecordDocumentoComplementar").empty().html("ocorreu um erro" + dataError);
    });
}

var vincularDocumento = (obj) => {
   
    var comandoDocumento = {
        cd_cargo_id: $('#codigoCargo').val(),
        cd_documento_complementar: obj.cd_documento_complementar,
        cd_documento_id: obj.cd_documento_id,
        cd_empresa_id: ($('#tipoDocumento').val() == 1 ? $('#cd_empresa').val() : $('#codigoEmpresa').val()),
        vinculado: obj.checked,
        origem: $('#tipoDocumento').val(),
        status: true
    }

    $.ajax({
        type: 'POST',
        url: 'Documentacao/vincular-documento',
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


$('#tipoDocumento').on('change', (event) => {
    $("#titlePesquisa").text(event.currentTarget.value == 2 ? 'Nome da Empresa' : 'Cargo');
    $("#tituloPagina").text(event.currentTarget.value == 2 ? 'Pesquisa por Empresa' : 'Pesquisa por Cargo/Função');
    $('#modalCd_Empresa').css('display', event.currentTarget.value == 2 ? 'none' : '');
    $("#DivRenderData").empty();
})

