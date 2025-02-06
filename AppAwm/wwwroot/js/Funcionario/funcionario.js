$('#remuneracao').mask('#.##0,00', { reverse: true });
$('#cnpjEmpresa').mask('999.99999.99.9', { reverse: true });

$('#btnImportaColaboradores').on('click', (event) => {

    var cd_empresa = event.currentTarget.dataset.cd_empresa
    $('#cd_empresa').prop('value', cd_empresa);
    var nomeEmpresa = $('#cd_empresa option:selected').text()
    $('select').formSelect();

    $.confirm({
        title: 'Importar Planilha de Colaboradores',
        boxWidth: '30%',
        useBootstrap: false,
        type: 'purple',
        content: '' +
            '<form method="post" enctype="multipart/form-data id="formImportarColaborador">' +
            '<div class="input-field">' +
            '<input type="file" name="filePlan" id="filePlan" required />' +
            '</div>' +
            '<div class="input-field">' +
            '<input type="text" maxlength="100" id="nomeEmpresa", name="nomeEmpresa" value="'+nomeEmpresa+'"/> ' +
            '</form>',

        buttons: {
            formSubmit: {
                text: 'importar',
                btnClass: 'btn-blue',
                action: function () {

                    if (this.$content.find('#filePlan').val() == '') {
                        $.dialog({
                            title: 'Aviso!',
                            useBootstrap: false,
                            boxWidth: '25%',
                            content: 'Selecione a planilha Excel de colaboradores para iniciar a importação.',
                        });

                        return false;
                    }
                        var files = $('#filePlan')[0].files;

                        var formData = new FormData();

                        for (var i = 0; i != files.length; i++) {
                            formData.append("files", files[i]);
                    }
                        formData.append('cd_empresa', cd_empresa);

                        loading(true);

                        $.ajax({
                            type: 'POST',
                            url: 'Colaborador/importar',
                            processData: false,
                            contentType: false,
                            async: false,
                            data: formData
                        })
                            .done(function (data) {

                                debugger

                                if (data.success) {

                                    $('#btnSearch').trigger('click',);
                                    M.toast({ html: '<i class="material-icons white-text">check_circle</i>&nbsp; Importação concluida com sucesso.', classes: 'blue rounded' });
                                }
                                else {

                                    M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;' + data.message, classes: 'red rounded' });
                                }
                                loading(false);
                        })
                        .fail(function (data) {
                            M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;Ocorreu um erro ao tentar localiar o endereço pelo Cep', classes: 'red rounded' });
                            loading(false);
                        });
                }
            },
            cancelar: function () {
                //close
            },
        }
    });
    $('#nomeEmpresa').trigger('focus');

});

$('#cep').on({
    focusout: (event) => {
        let ceptext = event.currentTarget.value.replace('-', '');

        if (ceptext.length < 8) {
            return;
        }

        $.ajax({
            type: 'Get',
            url: '/search',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: { cep: ceptext }
        })
            .done(function (data) {
                SetEndereco(data);
            })
            .fail(function (data) {
                M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;Ocorreu um erro ao tentar localiar o endereço pelo Cep', classes: 'red rounded' });
            });
    }
});

$('#Cpf').on('focusout', (event) => {
    var numberPattern = /\d+/g;
    var _cpf = event.target.value;

    if (_cpf.trim().length == 0)
        return;

    _cpf = _cpf.match(numberPattern).join('');

    if (_cpf.length != 11) {
        M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;ERRO: O Cpf é composto por 8 digitos', classes: 'red rounded' });
        document.getElementById('postButton').disabled = true;
        return;
    }

    var validarNumeroCpf = validarCPF(_cpf);
    if (!validarNumeroCpf) {
        M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;Número de Cpf invalido.', classes: 'red rounded' });
        event.target.focus();
    }

    document.getElementById('postButton').disabled = !validarNumeroCpf;
});


$('#postButton').on('click', (event) => {

    var dataNascimento = new Date($('#dtNacimento').val())
    var dataAdmissao = new Date($('#dtAdmissao').val())

    if (dataAdmissao <= dataNascimento) {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - A Data de admissão não pode ser menor que a data de nascimento. ', classes: 'red darken-2 rounded'
        });

        return;
    }

    var frm = $('form').serialize();

    loading(true);

    $.ajax({
        type: 'POST',
        url: location.pathname,
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: frm
    })
        .done(function (data) {

            loading(false);

            if (data.success == true) {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
                });

                setTimeout(() => location.href = location.origin + '/Colaborador', 2000);
            }
            else {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - Registo salvo, mas ocorreu erro durante a execução', classes: 'red darken-2 rounded'
                });
            }
        })
        .fail(function (data) {

            loading(false);

            let msg = '';
            if (data.responseJSON.erros.length > 0)
                data.responseJSON.erros.forEach(s => { msg += ' - ' + s + '</br>' });
            else
                msg = data.responseJSON.message;

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>' + msg, classes: 'red darken-2 rounded'
            });
        });
});

var SetEndereco = (obj) => {
    $('#logradouro').val(obj.logradouro).focus();
    $('#bairro').val(obj.bairro).focus();
    $('#cidade').val(obj.estado).focus();
    $('#estado').val(obj.uf).focus();
    $('#numero').focus();
}


$('.datepicker').datepicker({
    format: 'dd/mm/yyyy',
    autoClose: true,
    yearRange: 30,
    defaultDate: new Date(new Date().getFullYear() - 30, new Date().getMonth(), 1),
    i18n: {
        months: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthsShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        weekdays: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sabádo'],
        weekdaysShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        weekdaysAbbrev: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
        today: 'Hoje',
        clear: 'Limpar',
        cancel: 'Sair',
        done: 'Confirmar',
        labelMonthNext: 'Próximo mês',
        labelMonthPrev: 'Mês anterior',
        labelMonthSelect: 'Selecione um mês',
        labelYearSelect: 'Selecione um ano',
        selectMonths: true
    }
});

$('#file-input').on('input', () => {

    var files = $('#file-input')[0].files;
   
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("file", files[i]);
    }

    formData.append('cd_funcionario', $('#cd_funcionario').val());

    $.ajax({
        type: 'PUT',
        url: '/Anexo/updateFoto',
        processData: false,
        contentType: false,
        data: formData
    })
        .done(function (data) {

            loading(false);

            if (data.success == true) {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
                });

                window.location.reload();
            }
        })
        .fail(function (data) {

            loading(false);

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>&nbsp -' + data.responseJSON.message, classes: 'red darken-2 rounded'
            });
        });
});

$('#baixarModeloexcel').on('click', () => {
    window.location.href = '/Anexo/downloadFile/101?isApp=true';
})