$(document).ready(function () {
    $('input[type="text"],input[type="search"],input[type="number"],input[type="time"],input[type="datetime"],input[type="password"],textarea,select').addClass("form-control");
    $('.select2').select2();
    $("a[data-widget='pushmenu']").on('click', function (e) {
        if ($("body").hasClass("sidebar-collapse")) {
            setCookie("sidebar", "on", 1000);
        } else {
            setCookie("sidebar", "off", 1000);
        };
    });
    loadUserShortcuts()

});

$(function () {
    if (getCookie('sidebar') == 'off') {
        $("body").addClass("sidebar-collapse");
    }
    else {
        $("body").removeClass("sidebar-collapse");
    }
});

function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}


var Toast = Swal.mixin({
    toast: true,
    position: 'bottom-end',
    showConfirmButton: false,
    timer: 3000
});

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-bottom-left",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function DeleteItem(id, path, tablePostfix, overlyPostfix) {    
    var result = confirm('آیا از حذف رکورد مورد نظر اطمینان دارید؟');
    if (result) {
        var url = '/' + path + '/DeleteAjax/' + id;
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            beforeSend: function () {
                $("#overly-loading-" + overlyPostfix).removeClass("hidden");
            },
            success: function (response) {
                if (response.Result == true) {
                    if (response.ErrorExists == false) {
                        toastr.success('آیتم مورد نظر با موفقیت حذف گردید.');
                        $('#data-table-' + tablePostfix).dataTable().api().ajax.reload();
                    }
                    else {
                        if (response.ErrorCode == 3) {
                            toastr.error('به دلیل وجود ارجاعات، حذف رکورد مورد نظر امکان پذیر نمی‌باشد. ابتدا ارجاعات را حذف نمائید.')
                        }
                        else {
                            toastr.error(response.ErrorMessage)
                        }
                    }
                }
                else {
                    toastr.error(response.ErrorMessage)
                }
            },
            error: function (response) {
                toastr.error(response.ErrorMessage)
            },
            complete: function () {
                $("#overly-loading-" + overlyPostfix).addClass("hidden");
            },
        });
    }
}

function DeleteItemTableRow(id, path, tablePostfix, overlyPostfix) {
    var result = confirm('@Resource.AreYouSureToDeleteItem');
    if (result) {
        var url = '/' + path + '/DeleteAjax/' + id;
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            beforeSend: function () {
                $("#overly-loading-" + overlyPostfix).removeClass("hidden");
            },
            success: function (response) {
                if (response.Result == true) {
                    if (response.ErrorExists == false) {
                        toastr.success('@Resource.ItemRemovedSuccessfully');
                        $('#data-table-' + tablePostfix + ' #row-' + tablePostfix + '-' + id).remove();
                    }
                    else {
                        if (response.ErrorCode == 3) {
                            toastr.error('@Resource.DeleteConflictErrorMessage')
                        }
                        else {
                            toastr.error(response.ErrorMessage)
                        }
                    }
                }
                else {
                    toastr.error(response.ErrorMessage)
                }
            },
            error: function (response) {
                toastr.error(response.ErrorMessage)
            },
            complete: function () {
                $("#overly-loading-" + overlyPostfix).addClass("hidden");
            },
        });
    }
}

$('#modal-content').on('hidden.bs.modal', function (e) {
    $('#modal-content-title').html('');
    $('#modal-content-body').html('');
    $('#modalBtnSave').addClass('hidden');
    $('#modalBtnOk').addClass('hidden');
    $('.modal-dialog').removeClass('modal-sm');
    $('.modal-dialog').removeClass('modal-xl');
    $('.modal-dialog').removeClass('modal-lg');
    $('#modal-dialog-data-table-buttons').html('');
    //$('#modal-overly-top-button').removeClass('hidden');
    $('#modal-overly-footer').addClass('hidden');
    $("#overly-loading-modal").addClass("hidden");
});

$('#modal-overly-content').on('hidden.bs.modal', function (e) {
    $('#modal-overly-content-title').html('');
    $('#modal-overly-content-body').html('');
    $('#modalBtnSave-overly').addClass('hidden');
    $('#modalBtnOk-overly').addClass('hidden');
    $('#modal-overly-content > .modal-dialog').removeClass('modal-sm');
    $('#modal-overly-content > .modal-dialog').removeClass('modal-xl');
    $('#modal-overly-content > .modal-dialog').removeClass('modal-lg');
    //$('#modal-overly-dialog-data-table-buttons').html('');
    //$('#modal-overly-top-button').removeClass('hidden');
    $('#modal-overly-footer').addClass('hidden');
    $("#overly-loading-modal-overly").addClass("hidden");
});

$('#modal-content').on('show.bs.modal', function (e) {
    $('input[type="text"],input[type="search"],input[type="number"],input[type="time"],input[type="datetime"],input[type="password"],textarea,select').addClass("form-control");

});
//$("body").on('DOMSubtreeModified', "#modal-content-body", function () {
//    //$.validator.unobtrusive.parse("#partialForm");
//});
function crudAction(entity, link, id, title, modalSize, isOverly = false, parentId = 0) {
    $.ajax({
        cache: false,
        type: 'GET',
        url: '/' + link + '?id=' + id + (parentId != null ? '&parentId=' + parentId : ''),
        dataType: "html",
        beforeSend: function () {
            $("#overly-loading-" + entity).removeClass("hidden");
        },
        success: function (data) {
            if (isOverly) {
                $('#modal-overly-content-title').html('<i class="fad fa-rectangle-history-circle-plus fa-lg"></i> ' + title);
                $('#modal-overly-content-body').html(data);
                if (modalSize != '') {
                    $('#modal-overly-content >.modal-dialog').addClass(modalSize);
                }
                $('.select2').select2();
                $('#modal-overly-content').modal('toggle');
                // re apply validatior
                $.validator.unobtrusive.parse('#'+entity);

            }
            else {
                $('#modal-content-title').html('<i class="fad fa-rectangle-history-circle-plus fa-lg"></i> ' + title);
                $('#modal-content-body').html(data);
                if (modalSize != '') {
                    $('#modal-content >.modal-dialog').addClass(modalSize);
                }
                $('.select2').select2();
                $('#modal-content').modal('toggle');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            toastr.error(thrownError)
        },
        complete: function responce() {
            $("#overly-loading-" + entity).addClass("hidden");
            if ($.validator.unobtrusive != undefined) {
                $.validator.unobtrusive.parse('#'+entity);
            }
            else { toastr.error('Form validator not found.') }
        }
    });
}
String.prototype.toEnglishDigits = function () {
    return this.replace(/[۰-۹]/g, function (chr) {
        var persian = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
        return persian.indexOf(chr);
    });
};


function loadModalView(link, entity, id, title, modalSize) {
    $.ajax({
        cache: false,
        type: "GET",
        url: '/' + link + (id != null ? '?id=' + id : ''),
        dataType: "html",
        beforeSend: function () {
            $("#overly-loading-" + entity).removeClass("hidden");
        },
        success: function (data) {
            $('#modal-content-title').html('<i class="fad fa-images fa-lg" ></i> ' + title);
            $('#modal-content-body').html(data);
            $('#modal-footer').removeClass('hidden');
            $('#modalBtnCancel').removeClass('hidden');            
            if (modalSize != '') {
                $('#modal-content > .modal-dialog').addClass(modalSize);
            }
            $('#modal-content').modal('toggle');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            toastr.error(xhr.ErrorMessage)
        },
        complete: function responce() {
            $("#overly-loading-" + entity).addClass("hidden");
        }
    });
}
$(init);
function init() {
    $('#tab-empty').droppable({
        drop: handleDropEvent
    });

    $('.draggable-MenuItem').draggable({
        containment: 'document',
        appendTo: 'body',
        cursor: 'move',
        snap: '#tab-empty',
        revertDuration: 200,
        helper: function () {
            var iconText = jQuery("a", this).html();
            icontext = iconText.replace('nav-icon', '')
            return $("<div id='draggableHelper' class='p-1 pb-0'></div>").append(iconText);
        }
    });
}
function handleDropEvent(event, ui) {
    var draggable = ui.draggable;
    var id = draggable.attr('id').match(/\d+/);
    $.ajax({
        cache: false,
        type: 'POST',
        url: '/UserShortcut/AddShortcut?id=' + id,
        dataType: "json",
        beforeSend: function () {
            $("#tab-loading").removeClass("hidden");
        },
        success: function (data) {
            loadUserShortcuts();
            if (data.Result == true) {
                toastr.success(data.ErrorMessage)
            }
            else
                toastr.error(data.ErrorMessage)
        },
        error: function (xhr, ajaxOptions, thrownError) {
            toastr.error(thrownError)
        },
        complete: function responce() {
            $("#tab-loading").addClass("hidden");
        }
    });
}
function loadUserShortcuts() {

    $.ajax({
        cache: false,
        type: 'POST',
        url: '/UserShortcut/GetUserShortcuts',
        dataType: "json",
        beforeSend: function () {
            $("#tab-loading").removeClass("hidden");
        },
        success: function (data) {
            $('#tab-empty').html('');
            for (var i = 0; i < data.length; i++) {
                var content = '<div class="shortcut-container p-2">' +
                    '<i class="fas fa-times shortcut-close" onclick="removeShortcut(' + data[i].Id + ')"></i>' +
                    '<a class="text-center nav-link" onclick="openMenu(' + data[i].MenuId + ')">' +
                    '<i class="' + data[i].IconClass.replace('far', 'fad').replace('fal', 'fad') + ' fa-2x d-block"></i>' +
                    '<p class="mb-0 mt-2" style="font-size:11px">' + data[i].Title + '</p></a></div>';
                $('#tab-empty').append(content);
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            toastr.error(thrownError)
        },
        complete: function responce() {
            $("#tab-loading").addClass("hidden");
        }
    });
}
function openMenu(id) {

    $("#menu-" + id + " a").trigger("click");
}
function showDesktop() {
    $('[aria-labelledby^="tab--"]').each(function () {
        $(this).removeClass('active show');
    });
    $('[aria-labelledby^="tab-"]').each(function () {
        $(this).removeClass('active show');
    });
    $('#tab-empty').css("display", "");
}
function removeShortcut(id) {
    var result = confirm('@Resource.AreYouSureToDeleteShortcut');
    if (result) {
        $.ajax({
            cache: false,
            type: 'POST',
            url: '/UserShortcut/RemoveShortcut?id=' + id,
            dataType: "json",
            beforeSend: function () {
                $("#tab-loading").removeClass("hidden");
            },
            success: function (data) {
                loadUserShortcuts();
                if (data.Result == true) {
                    toastr.success(data.ErrorMessage)
                }
                else
                    toastr.error(data.ErrorMessage)
            },
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.error(thrownError)
            },
            complete: function responce() {
                $("#tab-loading").addClass("hidden");
            }
        });
    }
}

function downloadFile(entity, filePath, fileName) {
    $.ajax({
        cache: false,
        type: 'GET',
        url: '/File/DownloadFile?filePath=' + filePath + '&fileName=' + fileName,
        contentType: 'application/json',
        xhrFields: {
            responseType: 'blob'
        },
        beforeSend: function () {
            if (entity != '') {
                $("#overly-loading-" + entity).removeClass("hidden");
            }
        },
        success: function (data, textStatus, xhr) {
            console.log(xhr.getAllResponseHeaders());
            var blob = new Blob([data], { type: xhr.getResponseHeader('MimeType') });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = fileName;
            document.body.appendChild(a);
            a.click();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //toastr.error(thrownError)
        },
        complete: function responce() {
            if (entity != '') {
                $("#overly-loading-" + entity).addClass("hidden");
            }
        }
    });
}

function duplicateEntity(id, entity) {
    var url = '/'+entity+'/Duplicate/' + id;
    $.ajax({
        url: url,
        cache: false,
        type: "POST",
        beforeSend: function () {
            $("#overly-loading-" + entity).removeClass("hidden");
        },
        success: function (response) {
            if (response.Result > 0) {
                $('#data-table-' + entity).dataTable().api().ajax.reload();
                toastr.info(response.ErrorMessage + '\n' + 'شناسه : ' + response.Result)
            }
            else {
                toastr.error(response.ErrorMessage)
            }
        },
        error: function (response) {
            toastr.error(response.ErrorMessage)
        },
        complete: function () {
            $("#overly-loading-" + entity).addClass("hidden");
        },
    });

}

