﻿

function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    refreshAddNewTab($(form).attr('data-resetUrl'), true);
                    //success message
                    $.notify(response.message, "success");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    //error message
                    $.notify(response.message, "error");
                }
                
            }
        }
        //if image upload enctype is there
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);

    }
    return false;
}

function refreshAddNewTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $('#secondTab').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab) {
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
            }
        }
    });
}
function Edit(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $('#secondTab').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}
function Delete(url) {
    if (confirm('Are you sure to delete this record?') == true) {
        $.ajax({
            type: 'POST', //best practice to use post on delete actions
            url: url,
            success: function (response) {
                if (response.success) {
                    $('#firstTab').html(response.html);
                    $.notify(response.message, "warn");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    $.notify(response.message, "error");
                }
                
                
            }
        });
    }
}