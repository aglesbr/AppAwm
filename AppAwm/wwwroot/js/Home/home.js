﻿var grafico = (origem, perfil) => {


    $.ajax({
        type: 'Get',
        url: '/getChart?origem=' + origem,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false
    })
        .done(function (data) {
            setValueChart(data.chart, origem, perfil);
        })
        .fail(function () {
            M.toast({ html: '<i class="material-icons white-text">highlight_off</i>&nbsp;Ocorreu um erro ao tentar gerar o grafico', classes: 'red rounded' });
        });
}

var setValueChart = (objData, origem, perfil) => {

    var rotulo = (origem == 1 ? 'Colaborador' : 'Empresa') + ' sem documentação';

    if (perfil == 'terceiro' && origem == 2) {

        rotulo = 'Documentos não enviado'
    }

    const data = {
        labels: [
            rotulo,
            'Aprovados',
            'Em Analise',
        ],
        datasets: [{
            label: 'Total',
            data: [objData.totalSemDoc, objData.totalDocAprovado, objData.totalDocAnalise],
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(0,255,0)',
                'rgb(54, 162, 235)'
                //'rgb(255, 205, 86)'
            ],
            hoverOffset: 4
        }]
    };

    const config = {
        type: 'pie',
        data: data,
    };

    var ctx = origem == 1 ? $("#homeChartColaborador") : $("#homeChartEmpresa");
    new Chart(ctx, config);
}