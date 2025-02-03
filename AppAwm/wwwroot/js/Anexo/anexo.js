
M.AutoInit();
const tipoDocAnexo = document.querySelector('#tipoAnexo');
var tipoAnexoIncremento = [];

$('#btnCloseModaEmpresa').on('click', () => { tipoAnexoIncremento = []; });

$('#enviarAnexo').on('click', () => {

    loading(true);
    var files = $('#files')[0].files;

    if (files.length == 0) {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - Selecione um arquivo para enviar.', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    if (files[0].name.length > 50) {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - O nome do arquivo não pode conter mais que 50 caractéres.', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    var escopo = $('#scope').val();

    let obj = {
        codigo: $('#codigo').val(),
        descricao: $('#descricao').val() == '' ? null : $('#descricao').val(),
        scope: escopo,
        documento: $('#documento').val(),
        tipoAnexo: escopo == 'anexoComumColaborador' || escopo == 'anexoComumEmpresa' ? 0 : $('#tipoAnexo').val()
    }

    formData.append('comandoAnexoInformacao', JSON.stringify(obj));

    $.ajax({
        type: 'POST',
        url: '/Anexo/AddFiles',
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
                obj.descricao = null;
                obj.tipoAnexo = 0;
                obj.documento = 0;

                bindAnexos(obj);
                $('#frmAnexo').trigger("reset");
            }
        })
        .fail(function (data) {

            loading(false);

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>&nbsp - Não foi possivel anexar o arquivo' , classes: 'red darken-2 rounded'
            });
        });
});

var bindAnexos = (obj) => {

    
    const divRecord = obj.scope == 'colaborador' ? '#DivRecordAnexoColaborador' :'#DivRecordAnexo'

    $(divRecord).css('display', '');

    $(divRecord).empty().html('<div class="progress"><div class="indeterminate"></div></div>');


    let action = '/Anexo/Search/' + (obj.pagina == undefined ? 1 : obj.pagina);
    $.ajax({
        type: 'Get',
        url: action,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: { comandoAnexoInformacao: JSON.stringify(obj) }
    })
        .done(function (data) {

            $("#DivRecordAnexo").empty();
            $("#DivRecordAnexoColaborador").empty()

            if (obj.scope == 'empresa') {
                $("#DivRecordAnexo").empty().html(data);
                 populaTipoDocumento(2020);// pega os tipo de anexo com codigo 2020 que é anexo do tipo empresa
            }

            if (obj.scope == 'colaborador') {
                populaTipoDocumento(obj);
                $("#DivRecordAnexoColaborador").empty().html(data);
            }

            if (obj.scope == 'anexoComumColaborador' || obj.scope == 'anexoComumEmpresa' ) {
                $("#DivRecordAnexo").empty().html(data);
                return;
            }

            var tipo = $('#listAnexo').val().split(',').filter(t => t != '');

            if (tipo.filter(t => t != '').length > 0) {

                var totalTipoAnexoDisabled = true;

                for (var op of tipoAnexo.options) {

                    if (op.value == '0') continue;

                    if (!tipo.some(s => s == op.value)) {
                        op.removeAttribute('disabled');
                        totalTipoAnexoDisabled = false;

                        if (!tipoAnexoIncremento.some(s => s == op.value))
                            tipoAnexoIncremento.push(op.value);
                    }
                    else {

                        if (tipoAnexoIncremento.some(s => s == op.value))
                        {
                            op.removeAttribute('disabled');
                            continue;
                        }

                        $('#tipoAnexo option[value=' + op.value + ']').attr('disabled', 'disabled');
                    }
                }
            }

            $('select').formSelect();

            var msgContent = `<p>Após a validação carreta de todos os documentos enviados, solicite a administração da HDDOC a liberação da
                              ${(obj.scope == 'colaborador' ? ' integração do colaborador' : ' empresa para o acesso a planta da obra')}.<br><br><b>Contato: (11) 98539-9315<br>E-mail:documentacao@hddoc.com.br</b></p>`

            if (obj.scope == 'colaborador') {
                if (obj.integrado.toLowerCase() == 'false') {
                    if (totalTipoAnexoDisabled) {
                        $.alert({
                            title: 'Alerta de Integração!',
                            boxWidth: '25%',
                            useBootstrap: false,
                            content: msgContent
                        });
                    }
                }
            }
            obj.documentacaoPendente = obj.documentacaoPendente == undefined

            if (obj.scope == 'empresa') {
                if (obj.documentacaoPendente == false) {
                    if (totalTipoAnexoDisabled) {
                        $.alert({
                            title: 'Liberação de acesso a planta da obra!',
                            boxWidth: '25%',
                            useBootstrap: false,
                            content: msgContent
                        });
                    }
                }
            }

        })
        .fail(function (data) {
            $("#DivRecordAnexo").empty().html('Ocorreu um erro ao tentar executar a conslta');
        });
}

var downloadFile = (objeto) => {
    window.location.href = '/Anexo/downloadFile/' + objeto.id;
}

var removeFile = (objeto) => {

    loading(true);

    $.ajax({
        type: 'DELETE',
        url: '/Anexo/remove/' + objeto.id,
        dataType: "json",
        async: true,
    })
        .done(function (data) {

            loading(false);

            if (data.success == true) {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
                });
                bindAnexos(objeto);
            }
        })
        .fail(function (data) {
            loading(false);

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>&nbsp - ' + data.responseJSON.message, classes: 'red darken-2 rounded'
            });
            bindAnexos(objeto);
        });
}

var modalParams = (objeto) => {

    var scopes = ['empresa', 'anexoComumColaborador', 'anexoComumEmpresa'];
    
    if (objeto.pagina == undefined) {

        $('#scope').val(objeto.scope);
        $('#codigo').val(objeto.codigo);
        $('#documento').val(objeto.documento);
        $('#codigoEmpresa').val(objeto.codigoEmpresa);
        $('#codigoCargo').val(objeto.codigoCargo);
        $('#isIntegrado').val(objeto.integrado);

        if (scopes.some(s => s == objeto.scope)) {

            $('#titulo').html(`<h6>${objeto.titulo} - ${objeto.documento}</h6>`);

            if (objeto.scope == 'empresa') {
                $('#divTipoAnexo').css('display', '');
                $("#divFileAnexo").removeClass('s8').addClass('s4');
                $('#modalAnexoEmpresa').modal({ dismissible: false }).modal('open');
            }

            if (objeto.scope == 'anexoComumEmpresa') {

                $('#divTipoAnexo').css('display', 'none');
                $("#divFileAnexo").removeClass('s4').addClass('s8');
                $('#modalAnexoEmpresa').modal({ dismissible: false }).modal('open');
            }

            if (objeto.scope == 'anexoComumColaborador') {
                $('#modalAnexoFuncionario').modal({ dismissible: false }).modal('open');
            }

            $('#frmAnexo').trigger("reset");

            bindAnexos(objeto);
        }

        else {

            if (objeto.scope == 'colaborador') {

                $('#frmDocumentoColaborador').trigger("reset");
                $('#rotulo').html(`<h6>${objeto.titulo} - ${objeto.documento}</h6>`);
                $('#modalAnexoColaborador').modal({ dismissible: false }).modal('open');
                bindAnexos(objeto);
            }
        }
    }

}

// enviar anexo do colaborador para analise do documento
$('#enviarAnexoColaborador').on('click', () => {

    loading(true);

    var files = $('#filesColaborador')[0].files;

    if (files.length == 0) {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - Selecione um arquivo para enviar.', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    if (files[0].name.length > 50) {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - O nome do arquivo não pode conter mais que 50 caractéres.', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    var dataValid = $('#dataValidade').val();
    var dataAtual = new Date();

    if (dataValid.trim() == '' || new Date(dataValid) <= new Date(dataAtual.toDateString())) {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - Informe o prazo de validade do documento e que seja maior que a data atual', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    if ($('#tipoAnexo').val() == null || $('#tipoAnexo').val() == '0') {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - Selecione o tipo do documento.', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    let obj = {
        codigo: $('#codigo').val(),
        descricao: $('#descricaoColaborador').val(),
        scope: $('#scope').val(),
        documento: $('#documento').val(),
        dataValidade: $('#dataValidade').val(),
        tipoAnexo: $('#tipoAnexo').val(),
        codigoEmpresa: $('#codigoEmpresa').val(),
    }

    formData.append('comandoAnexoInformacao', JSON.stringify(obj));

    $.ajax({
        type: 'POST',
        url: '/Anexo/AddFiles',
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

                obj.descricao = null;
                obj.tipoAnexo = 0;

                bindAnexos(obj);
                $('#frmDocumentoColaborador').trigger("reset");
            }
        })
        .fail(function (data) {

            loading(false);

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>&nbsp - Não foi possivel anexar o arquivo', classes: 'red darken-2 rounded'
            });
        });
});

//var pesquisarAnexo = () => {

//    let obj = {
//        codigo: $('#codigo').val(),
//        descricao: $('#descricaoColaborador').val(),
//        scope: $('#scope').val(),
//        documento: $('#documento').val(),
//        tipoAnexo: $('#tipoAnexo').val(),
//        codigoEmpresa: $('#codigoEmpresa').val(),
//        origemPesquuisa: true
//    }

//    bindAnexos(obj);
//}

var abreCracha = (id) => {
    $.confirm({
        boxWidth: '420px',
        useBootstrap: false,
        type: 'dark',
        buttons: {
            Imprimir: {
                btnClass: 'btn-blue',
                action: function () {

                    jQuery.fn.extend({
                        printElem: function () {
                            var cloned = this.clone();
                            var printSection = $('#printSection');
                            if (printSection.length == 0) {
                                printSection = $('<div id="printSection" style="width:350px; height:500px; font-size:11px;"></div>')
                                $('body').append(printSection);
                            }
                            printSection.append(cloned);
                            var toggleBody = $('body *:visible');
                            toggleBody.hide();
                            $('#printSection, #printSection *').show();
                            //window.resizeTo(378, 530);
                            window.print();
                            printSection.remove();
                            toggleBody.show();
                        }
                    });

                    $('.printMe').printElem();
                }
            },
            Fechar: {
                btnClass: 'btn-red any-other-class', // multiple classes.
                }
        },
        content: function () {
            var self = this;
            return $.ajax({
                url: 'Colaborador/Cracha/' + id,
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                method: 'get',
            }).done(function (response) {
                self.setTitle(' ');
                self.setContentAppend(response);
            }).fail(function (dataError) {
                self.setContent('Ocorreu um erro inesperado<br> contacto o suporte ténico');
            });
        }
    });
}

var historicoDocumento = (item) => {

    let obj = {
        codigo: $('#codigo').val(),
        descricao: $('#descricaoColaborador').val(),
        scope: $('#scope').val(),
        documento: $('#documento').val(),
        tipoAnexo: item.tipoAnexo,
        codigoAnexo: item.codigoAnexo,
        codigoEmpresa: $('#codigoEmpresa').val(),
    }

    $.confirm({
        boxWidth: '50%',
        useBootstrap: false,
        type: 'dark',
        buttons: {
            Fechar: {
                btnClass: 'btn-red any-other-class', // multiple classes.
            }
        },
        content: function () {
            var self = this;
            return $.ajax({
                url: 'Anexo/historico/',
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                method: 'get',
                data: { comandoAnexoInformacao: JSON.stringify(obj) }
            }).done(function (response) {
                self.setTitle('Historio de Documentação');
                self.setContentAppend(response);
            }).fail(function (dataError) {
                self.setContent('Ocorreu um erro inesperado<br> contacto o suporte ténico');
            });
        }
    });
}

var populaTipoDocumento = (obj) => {
    var list;

    $.ajax({
        type: 'Get',
        url: '/Anexo/listDocuments/' + obj.codigoCargo,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false,
        data: { tipoAnexoEmpresa: obj.codigoCargo == 2020, cd_empresa: obj.codigoEmpresa }
    })
        .done(function (data) {

            list = JSON.parse(data)
            while (tipoDocAnexo.options.length > 1)
                tipoDocAnexo.remove(1);

            if (list.success == true) {

                M.FormSelect.getInstance(tipoDocAnexo);
                list.documentos.forEach(s => {
                    tipoDocAnexo.append(new Option(s.nome, s.cd_Documentaco_Complementar))
                })

                M.FormSelect.init(tipoDocAnexo);
            }
            
        })
        .fail(function (data) {
            list = JSON.parse(data)
        });

    return list;
}