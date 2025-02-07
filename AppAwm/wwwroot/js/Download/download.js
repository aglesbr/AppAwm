$('#btnUploadArquivo').on('click', (event) => {

    $.confirm({
        title: 'Upload de arquivos do sistema HDDOC',
        boxWidth: '30%',
        useBootstrap: false,
        type: 'dark',
        content: '' +
            '<form method="post" enctype="multipart/form-data id="formUpload">' +
            '<div class="input-field">' +
            '<input type="file" name="files" id="files" required />' +
            '</div>' +
            '<div class="input-field">' +
            '<input type="text" maxlength="150" id="descricaoArquivo", name="descricaoArquivo"/> ' +
            '<label for="descricao">Descrição do Arquivo</label>'+
            '</form>',

        buttons: {
            formSubmit: {
                text: 'Iniciar Upload',
                btnClass: 'btn-blue',
                action: function () {

                    var isvalid = true;
                    var msg = '';

                    if (this.$content.find('#descricaoArquivo').val() == '') {
                        isvalid = false;
                        msg = 'Informe uma descrição sobre o arquivo selecionado.';
                    }

                    if (this.$content.find('#files').val() == '') {
                        isvalid = false;
                        msg = 'Selecione um arquivo para fazer o upload.';
                    }

                    if (this.$content.find('#files').val().length > 50) {
                        isvalid = false;
                        msg = 'O nome do arquivo selecionado, ultrapassa 50 caracteres, é nescessário renomear o arquivo.';
                    }


                    if (!isvalid) {

                        $.alert({
                            title: 'Aviso!',
                            useBootstrap: false,
                            boxWidth: '25%',
                            content: msg,
                        });

                        return false;
                    }

                    var files = $('#files')[0].files;

                    var formData = new FormData();

                    for (var i = 0; i != files.length; i++) {
                        formData.append("files", files[i]);
                    }
                    formData.append('descricao', $("#descricaoArquivo").val());

                    loading(true);

                    $.ajax({
                        type: 'POST',
                        url: 'download/upload',
                        processData: false,
                        contentType: false,
                        async: false,
                        data: formData
                    })
                        .done(function (data) {

                            if (data.success) {

                                $('#btnSearch').trigger('click');
                                M.toast({ html: '<i class="material-icons white-text">check_circle</i>&nbsp; Upload concluido com sucesso.', classes: 'blue rounded' });
                            }
                            else {

                                M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;' + data.message, classes: 'red rounded' });
                            }
                            loading(false);
                        })
                        .fail(function (data) {
                            M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;Ocorreu um erro ao tentar registrar o upload', classes: 'red rounded' });
                            loading(false);
                        });
                }
            },
            cancelar: function () {
                //close
            },
        }
    });
});

var removeFile = (obj) => {

    loading(true);

    $.ajax({
        type: 'DELETE',
        url: '/download/remove/' + obj.id,
        dataType: "json",
        async: false,
    })
    .done(function (data) {

        loading(false);

        if (data.success == true) {
            M.toast({
                html: '<i class="material-icons white-text">check_circle</i>&nbsp - ' + data.message, classes: 'blue darken-2 rounded'
            });

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

        M.toast({
            html: '<i class="material-icons white-text">cancel</i>&nbsp - ' + data.responseJSON.message, classes: 'red darken-2 rounded'
        });
    });
};

var downloadFile = (objeto) => {
    window.location.href = '/download/downloadFile/' + objeto.id;
}