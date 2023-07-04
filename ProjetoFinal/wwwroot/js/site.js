
$(document).ready(function () {
    var dataTable = $('#table-design').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Exibindo _START_-_END_ de _TOTAL_",
            "sInfoEmpty": "Exibindo 0-0 de 0",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Exibir _MENU_ por p&aacutegina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "&raquo",
                "sPrevious": "&laquo",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });

    dataTable.on('draw', function () {
        $('.cpf').mask('000.000.000-00');
        $('.cep').mask('00000-000');
        $('.rg').mask('0.000.000');
        $('.telefone').mask('(00) 00000-0000');
    });
})
