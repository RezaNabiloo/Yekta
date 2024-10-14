function initializeDataTable(config) {

    $(document).ready(function () {

        var entity = config.entity;
        var entityParentId = config.entityParentId;
        var entityPostfix = config.entityPostfix;
        var entityTitle = config.entityTitle;
        var customCrudAction = config.customCrudAction;
        var modalSize = config.modalSize;        
        $('#data-table-' + entity + config.entityPostfix + ' thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#data-table-' + entity + entityPostfix + ' thead');

        table = $('#data-table-' + entity + entityPostfix).DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            scrollX: true,
            response: true,
            lengthMenu: [[10, 25, 50, 100, 1000], [10, 25, 50, 100, 1000]],
            dom: 'lfBrtip',
            buttons: config.buttons,
            retrieve: true,
            Response: true,
            order: [0, 'asc'],
            bProcessing: true,
            language: { url: '/Content/plugins/datatables/fa.json' },
            bFilter: true,
            bServerSide: true,
            sAjaxSource: config.sAjaxSource,
            //----------------------
            stateSave: true, // Enable state saving
            stateDuration: -1, // Keep state save forever (until browser close)
            // -----------------------
            fnCreatedRow: function (nRow, aData, iDataIndex) { $(nRow).attr('id', 'row-' + entity + entityPostfix + '-' + aData['Id']); },
            aoColumns: config.columns,
            columnDefs: config.columnDefs,
            //----------------------
            buttons: [
                { extend: 'copy', exportOptions: { columns: ':visible' } },
                { extend: 'excel', exportOptions: { columns: ':visible' } },
                {
                    extend: 'print',
                    exportOptions: { columns: ':visible' },
                    title: '<div><p class="text-center">' + ('فهرست ' + entityTitle) + '</p><hr /></div>',
                },
                'colvis',
                {
                    text: '<i class="fal fa-refresh fa-md" data-toggle="tooltip" data-placement="bottom" title="به روز رسانی"></i>',
                    action: function (e, dt, node, config) {
                        $('#data-table-' + entity + entityPostfix).dataTable().api().ajax.reload(null,false);
                    }
                },
                {
                    text: '<i class="fas fa-plus fa-md text-success" data-toggle="tooltip" data-placement="bottom" title="ایجاد"></i>',
                    action: function (e, dt, node, config) {    
                        if (customCrudAction === '')
                        {
                            crudAction(entity, entity + '/CrudAction', 0, ('ایجاد' + entityTitle), modalSize);
                        }
                        else {
                            var customFunction = window[customCrudAction];
                            if (typeof customFunction === 'function') {
                                customFunction(entity, 'Admin/' + entity + '/CrudAction', 0, entityParentId, 'ایجاد ' + entityTitle, modalSize);
                            }
                        }
                    }
                },
            ],
            //----------------------
            initComplete: function () {
                var api = this.api();
                api
                    .columns()
                    .eq(0)
                    .each(function (colIdx) {
                        var cell = $('#data-table-' + entity + entityPostfix + '_wrapper .filters th').eq(
                            $(api.column(colIdx).header()).index()
                        );
                        var title = $(cell).text();
                        $(cell).html('<input type="text" class="form-control" placeholder=" " />');

                        $('input', $('#data-table-' + entity + entityPostfix + '_wrapper .filters th').eq($(api.column(colIdx).header()).index()))
                            .off('keyup change')
                            .on('change', function (e) {
                                $(this).attr('title', $(this).val());
                                var regexr = '({search})';
                                var cursorPosition = this.selectionStart;
                                api
                                    .column(colIdx)
                                    .search(
                                        this.value != ''
                                            ? regexr.replace('{search}', this.value).replace('(', '').replace(')', '')
                                            : '',
                                        this.value != '',
                                        this.value == ''
                                    )
                                    .draw();
                            })
                            .on('keydown', function (e) {
                                if (e.key == '13') {
                                    e.stopPropagation();
                                    $(this).trigger('change');
                                    $(this)
                                        .focus()[0]
                                        .setSelectionRange(cursorPosition, cursorPosition);
                                }
                            });
                    });
                $('#data-table-' + entity + entityPostfix + '_wrapper > .dt-buttons').appendTo('div.card-header #card-buttons-' + entity + entityPostfix);
                $('.dt-buttons').addClass('float-left dt-buttons-show');
                $('.dt-buttons button').addClass('btn btn-sm btn-light');
                $('#data-table-' + entity + entityPostfix + '_wrapper tr.filters').fadeIn(1000);
                table.columns.adjust();
            },
        });
    });
}

function initializeDataTableModal(config) {

    $(document).ready(function () {

        var entity = config.entity;
        var entityParentId = config.entityParentId;
        var entityPostfix = config.entityPostfix;
        var entityTitle = config.entityTitle;
        var customCrudAction = config.customCrudAction;
        var modalSize = config.modalSize;
        var isCustomCrudActionOverly = config.isCustomCrudActionOverly;
        $('#data-table-' + entity + config.entityPostfix + ' thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#data-table-' + entity + entityPostfix + ' thead');

        table = $('#data-table-' + entity + entityPostfix).DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            scrollX: true,
            response: true,
            lengthMenu: [[10, 25, 50, 100, 1000], [10, 25, 50, 100, 1000]],
            dom: 'lfBrtip',
            buttons: config.buttons,
            retrieve: true,
            Response: true,
            order: [0, 'asc'],
            bProcessing: true,
            language: { url: '/Content/plugins/datatables/fa.json' },
            bFilter: true,
            bServerSide: true,
            sAjaxSource: config.sAjaxSource,
            //----------------------
            stateSave: true, // Enable state saving
            stateDuration: -1, // Keep state save forever (until browser close)
            // -----------------------
            fnCreatedRow: function (nRow, aData, iDataIndex) { $(nRow).attr('id', 'row-' + entity + entityPostfix + '-' + aData['Id']); },
            aoColumns: config.columns,
            columnDefs: config.columnDefs,            
            buttons: [
                { extend: 'copy', exportOptions: { columns: ':visible' } },
                { extend: 'excel', exportOptions: { columns: ':visible' } },
                {
                    extend: 'print',
                    exportOptions: { columns: ':visible' },
                    title: '<div><p class="text-center">' + ('فهرست ' + entityTitle) + '</p><hr /></div>',
                },
                'colvis',
                {
                    text: '<i class="fal fa-refresh fa-md" data-toggle="tooltip" data-placement="bottom" title="به روز رسانی"></i>',
                    action: function (e, dt, node, config) {                        
                        $('#data-table-' + entity + entityPostfix).dataTable().api().ajax.reload(null,false);
                    }
                },
                {
                    text: '<i class="fas fa-plus fa-md text-success" data-toggle="tooltip" data-placement="bottom" title="ایجاد"></i>',
                    action: function (e, dt, node, config) {
                        if (customCrudAction === '') {
                            if (isCustomCrudActionOverly) {
                                crudAction(entity, entity + '/CrudAction', 0, ('ایجاد' + entityTitle), modalSize, true, entityParentId);
                            }
                            else { 
                                crudAction(entity, entity + '/CrudAction', 0, ('ایجاد' + entityTitle), modalSize);
                            }
                        }
                        else {
                            var customFunction = window[customCrudAction];
                            if (typeof customFunction === 'function') {
                                customFunction(entity, 'Admin/' + entity + '/CrudAction', 0, entityParentId, 'ایجاد ' + entityTitle, modalSize);
                            }
                        }
                    }
                },
            ],
            //----------------------
            initComplete: function () {
                var api = this.api();
                api
                    .columns()
                    .eq(0)
                    .each(function (colIdx) {
                        var cell = $('#data-table-' + entity + entityPostfix + '_wrapper .filters th').eq(
                            $(api.column(colIdx).header()).index()
                        );
                        var title = $(cell).text();
                        $(cell).html('<input type="text" class="form-control" placeholder=" " />');

                        $('input', $('#data-table-' + entity + entityPostfix + '_wrapper .filters th').eq($(api.column(colIdx).header()).index()))
                            .off('keyup change')
                            .on('change', function (e) {
                                $(this).attr('title', $(this).val());
                                var regexr = '({search})';
                                var cursorPosition = this.selectionStart;
                                api
                                    .column(colIdx)
                                    .search(
                                        this.value != ''
                                            ? regexr.replace('{search}', this.value).replace('(', '').replace(')', '')
                                            : '',
                                        this.value != '',
                                        this.value == ''
                                    )
                                    .draw();
                            })
                            .on('keydown', function (e) {
                                if (e.key == '13') {
                                    e.stopPropagation();
                                    $(this).trigger('change');
                                    $(this)
                                        .focus()[0]
                                        .setSelectionRange(cursorPosition, cursorPosition);
                                }
                            });
                    });
                $('#data-table-' + entity + '_wrapper > .dt-buttons').appendTo('#modal-dialog-data-table-buttons');
                $('.dt-buttons').addClass('float-left dt-buttons-show');
                $('.dt-buttons button').addClass('btn btn-sm btn-light');
                $('#data-table-' + entity + '_wrapper tr.filters').fadeIn(1000);
                table.columns.adjust();

            },
        });
    });
}



function initializeDataTableClientModal(config) {

    $(document).ready(function () {

        var entity = config.entity;        
        var entityTitle = config.entityTitle;
        var entityParentId = config.entityParentId;
        var customCrudAction = config.customCrudAction;
        var modalSize = config.modalSize;
        var addButton = config.addButton;

        $('#data-table-' + entity + ' thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#data-table-' + entity + ' thead');

        table = $('#data-table-' + entity).DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            scrollX: true,
            response: true,
            lengthMenu: [[10, 25, 50, 100, 1000], [10, 25, 50, 100, 1000]],
            dom: 'lfBrtip',
            buttons: config.buttons,
            retrieve: true,
            Response: true,
            order: [0, 'asc'],
            bProcessing: true,
            language: { url: '/Content/plugins/datatables/fa.json' },
            bFilter: true,
            bServerSide: false,
            //bServerSide: true,
            //sAjaxSource: config.sAjaxSource,
            fnCreatedRow: function (nRow, aData, iDataIndex) { $(nRow).attr('id', 'row-' + entity + '-' + aData['Id']); },
            //aoColumns: config.columns,
            columnDefs: config.columnDefs,
            //----------------------
            buttons: [
                { extend: 'copy', exportOptions: { columns: ':visible' } },
                { extend: 'excel', exportOptions: { columns: ':visible' } },
                {
                    extend: 'print',
                    exportOptions: { columns: ':visible' },
                    title: '<div><p class="text-center">' + ('فهرست ' + entityTitle) + '</p><hr /></div>',
                },
                'colvis',

                //----- modal data is local can not be refresh
                //{
                //    text: '<i class="fal fa-refresh fa-md" data-toggle="tooltip" data-placement="bottom" title="به روز رسانی"></i>',
                //    action: function (e, dt, node, config) {
                //        $('#data-table-' + entity).dataTable().api().ajax.reload();
                //    }
                //},
                {
                    text: '<i class="fas fa-plus fa-md text-success" data-toggle="tooltip" data-placement="bottom" title="ایجاد"></i>',
                    action: function (e, dt, node, config) {
                        if (customCrudAction === '') {
                            crudAction(entity, entity + '/CrudAction', 0, ('ایجاد' + entityTitle), modalSize);
                        }
                        else {
                            var customFunction = window[customCrudAction];
                            if (typeof customFunction === 'function') {
                                customFunction(entity, 'Admin/' + entity + '/CrudAction', 0, entityParentId, 'ایجاد ' + entityTitle, modalSize);
                            }
                        }
                    }
                },
            ],
            //----------------------
            initComplete: function () {
                var api = this.api();
                api
                    .columns()
                    .eq(0)
                    .each(function (colIdx) {
                        var cell = $('#data-table-' + entity + '_wrapper .filters th').eq(
                            $(api.column(colIdx).header()).index()
                        );
                        var title = $(cell).text();
                        $(cell).html('<input type="text" class="form-control" placeholder=" " />');

                        $('input', $('#data-table-' + entity + '_wrapper .filters th').eq($(api.column(colIdx).header()).index()))
                            .off('keyup change')
                            .on('change', function (e) {
                                $(this).attr('title', $(this).val());
                                var regexr = '({search})';
                                var cursorPosition = this.selectionStart;
                                api
                                    .column(colIdx)
                                    .search(
                                        this.value != ''
                                            ? regexr.replace('{search}', this.value).replace('(', '').replace(')', '')
                                            : '',
                                        this.value != '',
                                        this.value == ''
                                    )
                                    .draw();
                            })
                            .on('keydown', function (e) {
                                if (e.key == '13') {
                                    e.stopPropagation();
                                    $(this).trigger('change');
                                    $(this)
                                        .focus()[0]
                                        .setSelectionRange(cursorPosition, cursorPosition);
                                }
                            });
                    });                
                $('#data-table-' + entity + '_wrapper > .dt-buttons').appendTo('#modal-dialog-data-table-buttons');
                $('.dt-buttons').addClass('float-left dt-buttons-show');
                $('.dt-buttons button').addClass('btn btn-sm btn-light');
                $('#data-table-' + entity + '_wrapper tr.filters').fadeIn(1000);
                table.columns.adjust();
            },
        });
    });
}