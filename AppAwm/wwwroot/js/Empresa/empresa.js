M.AutoInit();
const obra = document.querySelector('#comboBoxObras');

$('#postButton').on('click', (event) => {

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

$('#enviarObra').on('click', (event) => {

    var form = $('#frmObra')
    var formData = new FormData(form.get(0));

    let obj = {
        cd_obra: $('#cd_obra').val(),
        cd_empresa_id: $('#cd_empresa_id').val(),
        descricao: $('#descricaoObra').val(),
        nome: $('#nomeObra').val(),
        status: $('#Status').prop('checked')
    }

    if (obj.nome == '') {
        M.toast({
            html: '<i class="material-icons white-text">check_circle</i>&nbsp - Informe o nome da Obra', classes: 'red darken-2 rounded'
        });

        return;
    }

    formData.append('comandoObra', JSON.stringify(obj));

    $.ajax({
        type: 'POST',
        url: '/Empresa/AddObra',
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

                bindObras(obj);
                $('#frmObra').trigger("reset");
            }
        })
        .fail(function (data) {

            loading(false);

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>&nbsp - ' + data.responseJSON.message, classes: 'red darken-2 rounded'
            });
        });

});

//************************************************* CONSULTAR CNPJ *************************************************************

$('#Cnpj').on('keyup', function (event) {
    if (event.currentTarget.value.trim().length == 18) {

        loading(true);
        let action = `${window.location.origin}/Empresa/search/cnpj`
        $.ajax({
            type: 'POST',
            url: action,
            async: false,
            dataType: "json",
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: { cnpj: event.currentTarget.value }
        })
            .done(function (data) {

                loading(false);

                if (data.success == true) {
                    setFileds(data.receitaConsumerCnpj);
                }
                else {
                    M.toast({
                        html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
                    });
                }
            })
            .fail(function (data) {

                loading(false);

                if (data.responseJSON.success == false) {
                    M.toast({
                        html: '<i class="material-icons white-text">cancel</i>&nbsp - ' + data.responseJSON.message, classes: 'red darken-2 rounded'
                    });
                }
            })
    }
});

//******************************************************************************************************************************

var setFileds = (objeto) => {

    // Formatar Telefone
    let value = objeto.phones[0].area + objeto.phones[0].number;
    const regexTel = /^\(?([0-9]{2})\)?([0-9]{4,5})\-?([0-9]{4})$/mg;
    const substTel = `($1)$2-$3`;
    let result = value.replace(regexTel, substTel);
    $('#telefone').val(result).trigger('focus');

    // Formatar Cep
    value = objeto.address.zip;
    const regexCep = /^([0-9]{5})\-?([0-9]{3})$/mg;
    const substCep = `$1-$2`;
    result = value.replace(regexCep, substCep);
    $('#cep').val(result).trigger('focus');


    $('#empresa').val(objeto.company.name).trigger('focus');
    $('#equity').val(objeto.company.equity).trigger('focus');
    $('#nomeFantasia').val(objeto.alias).trigger('focus');
    $('#logradouro').val(objeto.address.street).trigger('focus');
    $('#numero').val(objeto.address.number).trigger('focus');
    $('#detalhes').val(objeto.address.details).trigger('focus');
    $('#bairro').val(objeto.address.district).trigger('focus');
    $('#cidade').val(objeto.address.city).trigger('focus');
    $('#estado').val(objeto.address.state).trigger('focus');
    $('#email').val(objeto.emails[0].address).trigger('focus');
   // $('#complemento').val(JSON.stringify(objeto));
    $("#status").prop("checked", objeto.status.text == 'Ativa');
}


var editarObra = (obj) => {

    $('#cd_obra').val(obj.cd_Obra);
    $('#cd_empresa_id').val(obj.cd_Empresa_Id);
    $('#nomeObra').val(obj.nome).trigger('focus');
    $('#descricaoObra').val(obj.descricao).trigger('focus');
    $("#Status").prop("checked", obj.status)
}

var openModal = (objeto) => {

    var scopes = ['obra', 'vincularColaborador'];

    if (objeto.pagina == undefined) {

        if (scopes.some(s => s == objeto.scope)) {

            $('#tituloEmpresaObra').html(`<h6>${objeto.titulo} - ${objeto.cnpj}</h6>`);

            if (objeto.scope == 'obra') {
                
                $('#cd_empresa_id').val(objeto.cd_empresa_id);
                $('#modalObraEmpresa').modal({ dismissible: false }).modal('open');

                $('#frmObra').trigger("reset");

                bindObras(objeto);
            }

            if (objeto.scope == 'vincularColaborador') {
                $('#cd_empresa').val(objeto.cd_empresa_id);
                $('#modalVincularColaborador').modal({ dismissible: false }).modal('open');
                $('#tituloColaboradorObra').html(`<h6>${objeto.nome} - ${objeto.cnpj}</h6>`)
                $('#frmVincularColaborador').trigger("reset");                
                getObras(objeto)
            }
        }

    }

}

$('#comboBoxObras').on('change', (event) => {
    $('#DivRecordColaborador').empty();
    if (event.currentTarget.value == '' || event.currentTarget.value == '0')
        return;

    var id_Empresa = $('#cd_empresa').val();
    let obj = { pagina: 1, id_Empresa: id_Empresa, id_Obra: event.currentTarget.value }
    getFuncionario(obj);
})


var vinculaFuncionario = (obj) => {

    let vinculo = {

        cd_funcionario_id: obj.cd_funcionario_id,
        cd_empresa_id: $('#cd_empresa').val(),
        cd_obra_id: $('#comboBoxObras').val(),
        vinculado: obj.checked
    }

    
    var form = $('#frmVincularColaborador')
    var formData = new FormData(form.get(0));
    formData.append('comandoVincularObra', JSON.stringify(vinculo));

    $.ajax({
        type: 'POST',
        url: '/Empresa/Vincular',
        dataType: "json",
        processData: false,
        contentType: false,
        data: formData
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
                html: '<i class="material-icons white-text">cancel</i>' + msg, classes: 'red darken-2 rounded'
            });
        });
}

var getFuncionario = (obj) => {

    let action = 'Empresas/FuncionarioByObra/' + (obj.pagina == undefined ? 1 : obj.pagina);
    $.ajax({
        type: 'Get',
        url: action,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: { id_Empresa: obj.id_Empresa, id_Obra: obj.id_Obra }
    })
        .done(function (data) {

            $("#DivRecordColaborador").empty().html(data);
        })
        .fail(function (data) {
            $("#DivRecordColaborador").empty().html('Ocorreu um erro ao tentar executar a conslta');
        });
}

var bindObras = (obj) => {

    let action = '/Empresa/Obras/' + (obj.pagina == undefined ? 1 : obj.pagina);
    $.ajax({
        type: 'Get',
        url: action,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        data: { id: obj.cd_empresa_id }
    })
        .done(function (data) {
            $("#DivRecordObra").empty().html(data);
        })
        .fail(function (data) {
            $("#DivRecordObra").empty().html('Ocorreu um erro ao tentar executar a conslta');
        });
}

var getObras = (obj) => {

    let action = '/Empresa/GetObras/'
    $.ajax({
        type: 'Get',
        url: action,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        data: { id: obj.cd_empresa_id }
    })
        .done(function (data) {

            obra.innerHTML = null;
            obra.append(new Option('Selecione uma obra...', '0', true));

            if (data.success == true) {

                M.FormSelect.getInstance(obra);
                data.obras.forEach(s => {
                    obra.append(new Option(s.nome, s.cd_Obra))
                })

                M.FormSelect.init(obra);
            }
            else {

                M.FormSelect.init(obra);
                M.toast({
                    html: '<i class="material-icons white-text">cancel</i> Não existe Obra ativa para essa empresa!', classes: 'red darken-2 rounded'
                });
            }
        })
        .fail(function (data) {
            M.toast({
                html: '<i class="material-icons white-text">cancel</i> ocorreu um erro ao tentar listar as obras', classes: 'red darken-2 rounded'
            });
        });
}

