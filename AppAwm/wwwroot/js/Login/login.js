$('#btnSolicitaPwd').on('click', (event) => {

    loading(true);

    var act = location.pathname + 'solicitaNovapwd';
    var body = { documento: JSON.stringify($('#Cpf').val()) }

    $.ajax({
        type: 'Get',
        url: act,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: true,
        data: body
    })
        .done(function (data) {

            loading(false);

            $('#btnSolicitaPwd').prop('disabled', true);

            let clss = data.success == true ? 'blue' : 'red';

            M.toast({
                html: `<i class="material-icons white-text">check_circle</i>  ${data.message}`, classes: `${clss} darken-2 rounded`
                });
            
        })
        .fail(function (data) {

            M.toast({
                html: '<i class="material-icons white-text">cancel</i>' + data.message, classes: 'red darken-2 rounded'
            });
        });
});


$('#Cpf').on('keyup', (event) => {

    $('#btnSolicitaPwd').prop('disabled', event.target.value.length != 14)
    
});

$('#btnLogin').on('click', () => {
    $('#divControleLogin').css('display', 'none');
    $('#divloginIntermitente').css('display', '');
});