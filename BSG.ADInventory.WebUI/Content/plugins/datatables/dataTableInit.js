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
            stateSave: true, // Enable state saving
            stateDuration: -1, // Keep state save forever (until browser closes)
            fnCreatedRow: function (nRow, aData, iDataIndex) {
                $(nRow).attr('id', 'row-' + entity + entityPostfix + '-' + aData['Id']);
            },
            aoColumns: config.columns,
            columnDefs: config.columnDefs,
            buttons: [
                // New "Clear All Filters" button
                {
                    text: '<i class="fal fa-filter-slash fa-md text-red" data-toggle="tooltip" data-placement="bottom" title="حذف فیلتر"></i>',
                    action: function (e, dt, node, config) {

                        // Clear filter inputs
                        $('#data-table-' + entity + entityPostfix + '_wrapper .filters input').each(function () {
                            $(this).val('');
                        });

                        // Clear filters from localStorage
                        dt.columns().eq(0).each(function (colIdx) {
                            localStorage.removeItem('filter_' + entity + entityPostfix + '_' + colIdx);
                        });

                        // Clear filter in DataTable
                        dt.columns().search('');
                        dt.search('');


                        // Reload the DataTable to load all data from the server
                        dt.ajax.reload(null, false); // Set to 'false' to retain pagination position
                    }
                },
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
                        $('#data-table-' + entity + entityPostfix).dataTable().api().ajax.reload(null, false);
                    }
                },
                {
                    text: '<i class="fas fa-plus fa-md text-success" data-toggle="tooltip" data-placement="bottom" title="ایجاد"></i>',
                    action: function (e, dt, node, config) {
                        if (customCrudAction === '') {
                            crudAction(entity, entity + '/CrudAction', 0, ('ایجاد' + entityTitle), modalSize);
                        } else {
                            var customFunction = window[customCrudAction];
                            if (typeof customFunction === 'function') {
                                customFunction(entity,  entity + '/CrudAction', 0, entityParentId, 'ایجاد ' + entityTitle, modalSize);
                            }
                        }
                    }
                },              
                
            ],
            initComplete: function () {
                var api = this.api();
                
                // Restore filters from localStorage
                api.columns().eq(0).each(function (colIdx) {
                    var cell = $('#data-table-' + entity + entityPostfix + '_wrapper .filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );

                    var title = $(cell).text();
                    $(cell).html('<input type="text" class="form-control" placeholder=" " />');

                    // Retrieve saved filter
                    var savedFilter = localStorage.getItem('filter_' + entity + '_' + colIdx);
                    if (savedFilter !== null) {
                        $(cell).find('input').val(savedFilter);
                        api.column(colIdx).search(savedFilter).draw();
                        $(cell).find('input').addClass('input-filled'); // Add class if there is a saved filter
                    }

                    function updateInputBackground() {
                        if ($(this).val() !== '') {
                            $(this).addClass('input-filled');
                        } else {
                            $(this).removeClass('input-filled');
                        }
                    }

                    $('input', $('#data-table-' + entity + entityPostfix + '_wrapper .filters th').eq($(api.column(colIdx).header()).index()))
                        .off('keyup change')
                        .on('change', function (e) {
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})';
                            var cursorPosition = this.selectionStart;
                            var value = this.value;

                            // Save filter to localStorage
                            localStorage.setItem('filter_' + entity + '_' + colIdx, value);
                            updateInputBackground.call(this);
                            api
                                .column(colIdx)
                                .search(
                                    value != ''
                                        ? regexr.replace('{search}', value).replace('(', '').replace(')', '')
                                        : '',
                                    value != '',
                                    value == ''
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
        var childEntity = config.childEntity;
        var childEntityParentId = config.childEntityParentId;
        var childEntityPostfix = config.childEntityPostfix;
        var childEntityTitle = config.childEntityTitle;
        var customCrudAction = config.customCrudAction;
        var modalSize = config.modalSize;
        var isCustomCrudActionOverly = config.isCustomCrudActionOverly;

        $('#data-table-' + childEntity + config.childEntityPostfix + ' thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#data-table-' + childEntity + childEntityPostfix + ' thead');

        table = $('#data-table-' + childEntity + childEntityPostfix).DataTable({
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
            stateSave: true,
            stateDuration: -1,
            fnCreatedRow: function (nRow, aData, iDataIndex) {
                $(nRow).attr('id', 'row-' + childEntity + childEntityPostfix + '-' + aData['Id']);
            },
            aoColumns: config.columns,
            columnDefs: config.columnDefs,
            buttons: [
                // New "Clear All Filters" button
                {
                    text: '<i class="fal fa-filter-slash fa-md text-red" data-toggle="tooltip" data-placement="bottom" title="حذف فیلتر"></i>',
                    action: function (e, dt, node, config) {

                        // Clear filter inputs
                        $('#data-table-' + childEntity + childEntityPostfix + '_wrapper .filters input').each(function () {
                            $(this).val('');
                        });

                        // Clear filters from localStorage
                        dt.columns().eq(0).each(function (colIdx) {
                            localStorage.removeItem('filter_' + childEntity + '_' + colIdx);
                        });

                        // Clear filter in DataTable
                        dt.columns().search('');
                        dt.search('');


                        // Reload the DataTable to load all data from the server
                        dt.ajax.reload(null, false); // Set to 'false' to retain pagination position
                    }
                },
                { extend: 'copy', exportOptions: { columns: ':visible' } },
                { extend: 'excel', exportOptions: { columns: ':visible' } },
                {
                    extend: 'print',
                    exportOptions: { columns: ':visible' },
                    title: '<div><p class="text-center">' + ('فهرست ' + childEntityTitle) + '</p><hr /></div>',
                },
                'colvis',
                {
                    text: '<i class="fal fa-refresh fa-md" data-toggle="tooltip" data-placement="bottom" title="به روز رسانی"></i>',
                    action: function (e, dt, node, config) {
                        $('#data-table-' + childEntity + childEntityPostfix).dataTable().api().ajax.reload(null, false);
                    }
                },
                {
                    text: '<i class="fas fa-plus fa-md text-success" data-toggle="tooltip" data-placement="bottom" title="ایجاد"></i>',
                    action: function (e, dt, node, config) {
                        if (customCrudAction === '') {
                            if (isCustomCrudActionOverly) {
                                crudAction(childEntity, childEntity + '/CrudAction', 0, ('ایجاد' + childEntityTitle), modalSize, true, childEntityParentId);
                            }
                            else {
                                crudAction(childEntity, childEntity + '/CrudAction', 0, ('ایجاد' + childEntityTitle), modalSize);
                            }
                        }
                        else {
                            var customFunction = window[customCrudAction];
                            if (typeof customFunction === 'function') {
                                customFunction(childEntity,  childEntity + '/CrudAction', 0, childEntityParentId, 'ایجاد ' + childEntityTitle, modalSize);
                            }
                        }
                    }
                }                
            ],
            initComplete: function () {                                
                var api = this.api();
                // Restore filters from localStorage
                api.columns().eq(0).each(function (colIdx) {
                    var cell = $('#data-table-' + childEntity + childEntityPostfix + '_wrapper .filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" class="form-control" placeholder=" " />');

                    // Retrieve saved filter
                    var savedFilter = localStorage.getItem('filter_' + childEntity + '_' + colIdx);
                    if (savedFilter !== null) {
                        $(cell).find('input').val(savedFilter);
                        api.column(colIdx).search(savedFilter).draw();
                        $(cell).find('input').addClass('input-filled'); // Add class if there is a saved filter
                    }

                    function updateModalInputBackground() {
                        if ($(this).val() !== '') {
                            $(this).addClass('input-filled');
                        } else {
                            $(this).removeClass('input-filled');
                        }
                    }

                    $('input', $('#data-table-' + childEntity + childEntityPostfix + '_wrapper .filters th').eq($(api.column(colIdx).header()).index()))
                        .off('keyup change')
                        .on('change', function (e) {
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})';
                            var cursorPosition = this.selectionStart;
                            var value = this.value;

                            // Save filter to localStorage
                            localStorage.setItem('filter_' + childEntity + '_' + colIdx, value);                          
                            updateModalInputBackground.call(this);
                            api
                                .column(colIdx)
                                .search(
                                    value != ''
                                        ? regexr.replace('{search}', value).replace('(', '').replace(')', '')
                                        : '',
                                    value != '',
                                    value == ''
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

                $('#data-table-' + childEntity + '_wrapper > .dt-buttons').appendTo('#modal-dialog-data-table-buttons');
                $('.dt-buttons').addClass('float-left dt-buttons-show');
                $('.dt-buttons button').addClass('btn btn-sm btn-light');
                $('#data-table-' + childEntity + '_wrapper tr.filters').fadeIn(1000);
                table.columns.adjust();
            }
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
                                customFunction(entity,  entity + '/CrudAction', 0, entityParentId, 'ایجاد ' + entityTitle, modalSize);
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