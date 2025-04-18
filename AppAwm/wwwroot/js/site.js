﻿

window.onload = () => {

    //************************************************* INICIALIZAÇÃO DE COMPONENTES *************************************************
    $('.sidenav').sidenav();
    $('.slider').slider();
    $('.dropdown-trigger').dropdown();
    $('.fixed-action-btn').floatingActionButton();
    $('select').formSelect();
    $('.modal').modal();
    $('.collapsible').collapsible();
    //************************************************* VARIAVEIS DE AMBIENTE  *******************************************************

    var errorMsg = '<div class="col row z-depth-3 teal white-text center-align" style="height: 50px; border: 1px solid; border-radius: 10px; padding:12px 0px 0px 0px;">';
    errorMsg += '<div class="col s1 left-align"><i class="material-icons white-text">loop</i></div>';
    errorMsg += '<div class="col s11 center">Ocorreu um erro na requisição do serviço, atualize a pagina e tente novamente!</div></div>';

    //************************************************* MASCARAS / VALIDAÇÕES/ FUNÇÕES  **********************************************
    $("#Cnpj").mask("99.999.999/9999-99");
    $("#cnpjEmpresa").mask("99.999.999/9999-99");
    $("#cep").mask("99999-999");
    $("#Cpf").mask("999.999.999-99");
    $("#telefone").mask("(99) 99999-9999");
    //$("#Nu_Oab").mask("999999/##", { translation: { '#': { pattern: /[^0-9$]/, optional: true } } });

    $("#clearFrm").on('click', () => {
        //$("#Cnpj").val(null);
        //$("#empresa").val('');
        //$('select').find('option[value="0"]').prop('selected', true);
        //$('select').formSelect();

        $('form').trigger("reset");
        $("#Status").prop("checked", false);
    });

    $("#btnClear").on('click', () => {
        $('form').trigger("reset");
    });

    $("#backFrm").on('click', (event) => {
        location.href = '/' + location.pathname.split('/')[1];
    });

    
    //************************************************* CONSULTAR GENERIA **********************************************************
    $("#btnSearch").on('click', function (event, param) {
        let body = {};
        var tipo = location.pathname.replace(/[^\w\s]/gi, '')

        body = getObejct(tipo);

        if (tipo == 'Operacao') {
            if (body.cd_empresa == null) {
                M.toast({ html: '<i class="material-icons white-text">report</i>&ensp;-  Selecione uma empresa.</p>', classes: 'red darken-3 rounded' });
                return;
            }
        }

        if (param == "&gt;&gt;") // >>
            param = parseInt($('#pageCount').val());

        if (param == "&lt;&lt;") // <<
            param = 1;

        $('#DivRenderData').css("display", "");
        $('#DivRenderData').empty().html('<div class="progress"><div class="indeterminate"></div></div>');

        var action = location.pathname + '/Search/' + (isNaN(param) ? 1 : param);

        $.ajax({
            type: 'Get',
            url: action,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: body
        })
            .done(function (data) {
                $("#DivRenderData").empty().html(data);
            })
            .fail(function (data) {
                $("#DivRenderData").empty().html(errorMsg);
            });
    });
    //******************************************************************************************************************************

    
    //************************************************* AUTO-COMPLETE **************************************************************
    $('.autocomplete').on('input', function () {

        let filter = $('.autocomplete').val();

        if (filter.length < 3)
            return;

        var tipo = '/' + location.pathname.split('/')[1] + '/getAutoComplete';
        jQuery.ajax({
            type: 'Get',
            url: tipo,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { _campo: filter }
        })
            .done(function (dataComplete) {

                var dataCountry = {};
                dataComplete.forEach(function (v) {
                    dataCountry[v.value] = null;
                });

                $('input.autocomplete').autocomplete({
                    data: dataCountry,
                    onAutocomplete: (event) => {

                        let keyValue = dataComplete.find(s => s.value == event);
                        $('#Cd_Cargo').val(keyValue.key);
                    }
                });

                $('input.autocomplete').autocomplete('open')

            })
            .fail(function () {
                M.toast({ html: '<pre><i class="material-icons white-text">report</i> ERRO: AUTO-COMPLETE!<p>Ocorreu uma falha na busca de valores do auto-complete.</p></pre>', classes: 'gray darken-3 rounded' });
            });
    });

    //******************************************************************************************************************************

};


var getObejct = function (patname) {

    let obj = {};
    switch (patname) {
        case 'Empresa':
            obj = {
                nome: $("#Empresa").val().toUpperCase(),
                cnpj: $("#cnpjEmpresa").val(),
                nomeFantasia: $("#nomeFantasia").val(),
                statusfilter: $("#StatusFilter").val() 
            }
            return { empresa: JSON.stringify(obj) };

        case 'Colaborador':
             obj = {
                nome: $("#funcionario").val(),
                id_empresa: $('#cd_empresa').val(),
                tipoContrato: $('#tipoContrato').val(),
                statusfilter: $("#StatusFilter").val() 
            }
            return { funcionario: JSON.stringify(obj) };

        case 'Operacao':
            obj = {
                cd_empresa: $("#cd_empresa").val(),
            }
            return { cd_empresa: $("#cd_empresa").val() };

        case 'Usuario':
            obj = {
                nome: $("#nome").val(),
                login: $("#login").val(),
                statusfilter: $("#StatusFilter").val(),
            }
            return { usuario: JSON.stringify(obj) };

        case 'Cliente':
            obj = {
                nome: $("#cliente").val().toUpperCase(),
                cnpj: $("#cnpjCliente").val(),
                statusfilter: $("#StatusFilter").val() 
            }
            return { cliente: JSON.stringify(obj) };

        case 'Documentacao':
            obj = {
                nome: $("#nome").val().toUpperCase(),
                Origem: $("#tipoDocumento").val()
            }
            return { documentacao: JSON.stringify(obj) };

        case 'Download':
            obj = {
                nome: $("#nome").val().toUpperCase(),
                descricao: $("#descricao").val()
            }
            return { download: JSON.stringify(obj) };

        case 'Ajuda':
            obj = {
                titulo: $("#video_titulo").val().toUpperCase(),
                descricao: $("#video_descricao").val(),
                statusfilter: $("#StatusFilter").val() 
            }
            return { video: JSON.stringify(obj) };

        default:
            return { id: 100, error: 'caminho desconhecido' };
    }
 
}

function validarCPF(inputCPF) {
    var soma = 0;
    var resto;
    var num = '';
    var count = 0;

    do {

        num = count.toString();

        for (var i = 0; i < 10; i++) {
            num += count.toString();
        }

        if (num == inputCPF)
            return false;
        
    } while (count++ < 10);

    for (i = 1; i <= 9; i++) soma = soma + parseInt(inputCPF.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;

    if ((resto == 10) || (resto == 11)) resto = 0;
    if (resto != parseInt(inputCPF.substring(9, 10))) return false;

    soma = 0;
    for (i = 1; i <= 10; i++) soma = soma + parseInt(inputCPF.substring(i - 1, i)) * (12 - i);
    resto = (soma * 10) % 11;

    if ((resto == 10) || (resto == 11)) resto = 0;
    if (resto != parseInt(inputCPF.substring(10, 11))) return false;
    return true;
}

var loading = (isShow) => {

    $("#bodyMain").LoadingOverlay((isShow ? "show" : "hide"), {
        background: "rgba(0, 0, 0, 0.3",
        text: "Aguarde...",
        textColor: '#FFFFFF'
    });
}

