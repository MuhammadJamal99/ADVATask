let departmentObj = function () {
    $(".save-new-item-btn").unbind().on("click", function () {
        let Model = GetData(".new-item-data", "#add-new-item-modal-body");
        $.ajax({
            type: "POST",
            url: "/Department/AddDepartment",
            data: { NewDepartmentVM: Model },
            cache: !0,
            datatype: "json",
            success: function (response) {
                if (response.success) {
                    LoadList(false);
                    $("#Add-item-modal").modal("hide");
                    ClearData(".new-item-data", "#add-new-item-modal-body");
                    toastr.success(response.responseText);
                }
                else {
                    toastr.error(response.responseText);
                }
            },
            error: function (response) {
                toastr.error(response.responseText);
            }
        });
    });
    let deleteDepartment = function (_DepartmentID) {
        $.ajax({
            type: "GET",
            url: "/Department/deleteDepartment",
            data: { EmployeeID: _DepartmentID },
            cache: !0,
            datatype: "Json",
            success: function (response) {
                if (response.success) {
                    toastr.success(response.responseText);
                    LoadList(false);
                }
                else {
                    toastr.error(response.responseText);
                }
            },
            error: function (response) {
                toastr.error(response.responseText);
            }
        });
    };
    let editDepartment = function () {
        $(".update-Department-item-btn").unbind().on("click", function () {
            let Model = GetData(".update-item-data", "#update-item-modal-body");
            $.ajax({
                type: "POST",
                url: "/Employee/UpdateEmployee",
                data: { UpdatedEmployee: Model },
                cache: !0,
                datatype: "json",
                success: function (response) {
                    if (response.success) {
                        LoadList(false);
                        $("#Update-item-modal").modal("hide");
                        ClearData(".update-item-data", "#update-item-modal-body");
                        toastr.success(response.responseText);
                    }
                    else {
                        toastr.error(response.responseText);
                    }
                },
                error: function (response) {
                    toastr.error(response.responseText);
                }
            });
        });
    };
    let Operation = function () {
        $(".edit-item").unbind().on("click", function () {
            let _DepartmentID = Number($(this).attr("department-id")),
                _DepartmentName = $(this).attr("department-name");
            $("#Update-item-modal").find(".modal-title").text(`Update Department: ${_DepartmentName}`);
            $("#Update-item-modal").modal("show");
            $("#update-item-modal-body").html($(".ContainerDivLoader").html());
            $("#update-item-modal-body").find("#Name").val(_DepartmentName);
            $("#update-item-modal-body").find("#DepartmentID").val(_DepartmentID);
            $("#Update-item-modal").modal("show");
            editDepartment();
        });
        $(".delete-item").unbind().on("click", function () {
            let _DepartmentID = Number($(this).attr("employee-id"));
            deleteDepartment(_DepartmentID);
        });
    };
    let ClearData = function (_ClassName, _TargetBody) {
        $(`${_TargetBody} ${_ClassName}`).each(function () {
            $(this).is("select") ? $(this).find("option:selected").val("") : $(this).val("");
        });
    };
    let GetData = function (_ClassName, _TargetBody) {
        let Model = {};
        $(`${_TargetBody} ${_ClassName}`).each(function () {
            let Value = $(this).is("select") ? $(this).find("option:selected").val() : $(this).val();
            Model[$(this).attr("id")] = Value;
        });
        return Model;
    };
    let LoadList = function (_WithLoad = false) {
        if (_WithLoad)
            $("#TableWrapper").html($(".ContainerDivLoader").html());
        $.ajax({
            type: "GET",
            url: "/Department/_DepartmentsList",
            data: {},
            cache: !0,
            datatype: "HTML",
            success: function (response) {
                $("#TableWrapper").html(response);
                Operation();
            },
            error: function (response) {
                console.log(response.responsetext);
            }
        });
    };
    let Initate = function () {
        LoadList(true);
    };
    return {
        Initate: function () {
            Initate();
        },
    }
};
$(function () {
    let ItemObj = departmentObj();
    ItemObj.Initate();
});