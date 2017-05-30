function postAndUpdate(url, form, target, modalName) {
    //if ($(form).valid())
    //{
    //$(modalName).modal('hide');


    $(modalName).modal('hide');
    var formdata = $(form).serialize();
    $.post(url, formdata,
        function (data) {
            $(target).html('').append(data);
            adjustCss();
        });
    //}
}

function adjustCss() {

    $('#fundList').on("change", reloadGrid);
    $('#reserveList').on("change", reloadGrid);

    $('#suggestionList').on("change", loadSuggestion);
    
    $('.value-mask').mask('###########0,00', { reverse: true });
    $('.datetime-mask').mask('00/00/0000');
    $('.datepicker').datepicker({
        language: 'pt-BR',
        format: 'dd/mm/yyyy'
    });
    $('input[type=checkbox]').checkbox();
    
    $('.input-group').on('focus', '.form-control', function () {
        $(this).closest('.input-group, .form-group').addClass('focus');
    }).on('blur', '.form-control', function () {
        $(this).closest('.input-group, .form-group').removeClass('focus');
    });

}

function loadSuggestion() {
    selVal = $('#suggestionList').val();
    if (selVal != -1)
    {
        $('#res-name').val(document.getElementsByName('res-name-' + selVal)[0].value);
        $('#res-abb').val(document.getElementsByName('res-abb-' + selVal)[0].value);
        $('#res-val').val(document.getElementsByName('res-val-' + selVal)[0].value);
    }
}

function postAndUpdateManager(url, form, target, modalName) {
    if (modalName != '') {
        $(modalName).modal('hide');
    }
    var formdata;
    if (form != '')
        formdata = $(form).serialize();
    $.post(url, formdata,
        function (data) {
            $(target).html('').append(data);
            adjustCss();
        });
}

function adjustScrollArea() {
    $('#rollArea').slimScroll({
        height: 'auto'
    });
}

function loadPartial(url, d, placeToLoad) {
    $.post(url, d,
        function (data) {
            $(placeToLoad).html('').append(data);
            adjustCss();
        });
}


function loadChart() {

    var checkedRes = [];
    $.each($('.chkres'), function (index, value) {
        if ($(value).hasClass('checked')) {
            checkedRes.push($(value).attr('id').split('-')[1]);
        }
    });

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: 'Simulator/GetProjection',
        data: JSON.stringify(checkedRes),
        success: function (chartData) {
            var ctx = document.getElementById("canvas").getContext("2d");
            var myLine = new Chart(ctx);
            for (var i = chartData.datasets.length - 1; i >= 0; i--) {
                dataset = chartData.datasets[i];
                $('#val-' + dataset.reserveId).html(dataset.monthlyDep);

                if ($('#include-' + dataset.reserveId).hasClass('checked') == false)
                    chartData.datasets.splice(i, 1);
            }

            $('#monthlyDep').val(chartData.totalMonthlyDep);

            options = {
                pointDot: false,
                bezierCurve: false
            }

            myLine.Line(chartData, options);
        }
    });
}