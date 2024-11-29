
M.AutoInit();
const tipoDocAnexo = document.querySelector('#tipoAnexo');

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

    let obj = {
        codigo: $('#codigo').val(),
        descricao: $('#descricao').val(),
        scope: $('#scope').val(),
        documento: $('#documento').val()
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

            if (obj.scope != 'colaborador') {
                $("#DivRecordAnexo").empty().html(data);
            }
            else {

                for (var op of tipoAnexo.options) {

                    if (op.value != '') {
                        op.removeAttribute('disabled');
                    }
                }

                var documentos = listaTipoDocumento($('#codigoCargo').val());

                $("#DivRecordAnexoColaborador").empty().html(data);

                var tipo = $('#listAnexo').val();

                if (tipo != undefined) {

                    tipo.split(',').forEach(f => {
                        if (f != '') {
                            $('#tipoAnexo option[value=' + f + ']').attr('disabled', 'disabled');
                        }
                    });

                }

                $('select').formSelect();
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

    var scopes = ['empresa', 'funcionario'];
    
    if (objeto.pagina == undefined) {

        $('#scope').val(objeto.scope);
        $('#codigo').val(objeto.codigo);
        $('#documento').val(objeto.documento);
        $('#codigoEmpresa').val(objeto.codigoEmpresa);
        $('#codigoCargo').val(objeto.codigoCargo);

        if (scopes.some(s => s == objeto.scope)) {

            $('#titulo').html(`<h6>${objeto.titulo} - ${objeto.documento}</h6>`);

            if (objeto.scope == 'empresa') {
                $('#modalAnexoEmpresa').modal({ dismissible: false }).modal('open');
            }

            if (objeto.scope == 'funcionario') {
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

    if ($('#dataValidade').val() == '') {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - Informe o prazo de validade do documento.', classes: 'red darken-2 rounded'
        });

        loading(false);
        return;
    }

    if ($('#tipoAnexo').val() == null || $('#tipoAnexo').val() == '') {
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
                obj.tipoAnexo = null;
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

var pesquisarAnexo = () => {

    let obj = {
        codigo: $('#codigo').val(),
        descricao: $('#descricaoColaborador').val(),
        scope: $('#scope').val(),
        documento: $('#documento').val(),
        tipoAnexo: $('#tipoAnexo').val(),
        codigoEmpresa: $('#codigoEmpresa').val(),
    }

    bindAnexos(obj);
}


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
                url: 'Funcionario/Cracha/' + id,
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                method: 'get'
            }).done(function (response) {
                self.setTitle(' ');
                self.setContentAppend(response);
            }).fail(function (dataError) {
                self.setContent('Ocorreu um erro inesperado<br> contacto o suporte ténico');
            });
        }
    });
}

var listaTipoDocumento = (id) => {
    var list;
    $.ajax({
        type: 'Get',
        url: '/Anexo/listDocuments/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false
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