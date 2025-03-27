$('input#video_descricao, input#titulo, input#urlLink').characterCounter();

$('#postButton').on('click', (event) => {

    debugger

    if ($('#titulo').val().length <= 20 ) {

        M.toast({
            html: '<i class="material-icons white-text">cancel</i>  &nbsp;' + 'informe um titulo para o vídeo com pelo menos 30 caracteres', classes: 'red darken-2 rounded'
        });

        return;
    }

    if ($('#video_descricao').val() <= 20) {

        M.toast({
            html: '<i class="material-icons white-text">cancel</i>  &nbsp;' + 'informe uma descrição para esse video com pelo menos 30 caracteres', classes: 'red darken-2 rounded'
        });

        return;
    }

    if ($('#urlLink').val() <= 20) {

        M.toast({
            html: '<i class="material-icons white-text">cancel</i>  &nbsp;' + 'informe o link da plataforma que o video está hospedado.', classes: 'red darken-2 rounded'
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
            }
            else {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'red darken-2 rounded'
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

var removeVideo = (id) => {
    debugger
    loading(true);
    $.ajax({
        type: 'DELETE',
        url: '/Ajuda/remove/' + id,
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
    })
        .done(function (data) {
            loading(false);
            if (data.success == true) {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
                });

                $('#frmAjudaPesquisa').trigger("reset");
                $('#btnSearch').trigger('click');
            }
            else {
                M.toast({
                    html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'red darken-2 rounded'
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
};