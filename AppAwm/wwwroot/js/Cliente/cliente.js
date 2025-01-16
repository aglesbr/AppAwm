$('#postButton').on('click', (event) => {


    if ($('#isPeriodoTeste').is(':checked'))
    {
        if (new Date($('#dtVencimentoPeriodoTeste').val()) <= new Date()) {
            M.toast({
                html: '<i class="material-icons white-text">cancel</i>  &nbsp;' + ' O Venciento não pode ser menor que a data atual', classes: 'red darken-2 rounded'
            });

            return;
        }
    }

    if ($('#plaoPacoteVida').val() == null) {

        M.toast({
            html: '<i class="material-icons white-text">cancel</i>  &nbsp;' + 'Selecione é um plano de serviço', classes: 'red darken-2 rounded'
        });

        return;
    }

    if ($('#planoVidas').val() <= 0) {

        M.toast({
            html: '<i class="material-icons white-text">cancel</i>  &nbsp;' + 'O número de vidas, deve ser maior que zero.', classes: 'red darken-2 rounded'
        });

        return;
    }

    $('#planoVidas').prop('disabled', false);

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

            $('#planoVidas').prop('disabled', true);

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

$('#planoVidas').on('keydown', (event) => {

    return false;
})

$('#isPeriodoTeste').change(() => {
    $('#dtVencimentoPeriodoTeste').prop('disabled', !$('#isPeriodoTeste').is(':checked'));
    if ($('#isPeriodoTeste').is(':checked')) {
        var d1 = new Date().setDate(new Date().getDate() + 15);
        $('#dtVencimentoPeriodoTeste').val(new Date(d1).toISOString().split('T')[0]);
    }
});

$('#plaoPacoteVida').change((event) => {
    $('#planoVidas').val(event.currentTarget.value);
    $('#planoVidas').prop('disabled', $('#plaoPacoteVida').val() != 500);
});