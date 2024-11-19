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


$('#cpf').on('focusout',(event) => {
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


$('input[id="cpf"]').mask('000.000.000-00');

$('#Email').on('focusout',() => {

    if ($('#Cd_Usuario').val() != '')
        return;

    if ($('#Email').val().trim().length == 0)
        return;

    var emailRegex = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (!emailRegex.test($('#Email').val())) {
        M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;formato de email inválido', classes: 'red rounded' });
        document.getElementById('postButton').disabled = true;
        $('#Login').val(null);
        $('#Email').focus();
    }
    else {

        if ($('#cpf').val().length > 11) {
            document.getElementById('postButton').disabled = false;
            var _login = 'wm' + parseInt(Math.random() * 100) + $('#Email').val().split('@')[0].substring(0,2) + $('#cpf').val().split('.')[1];
            $('#Login').val(_login);
        }
    }

});