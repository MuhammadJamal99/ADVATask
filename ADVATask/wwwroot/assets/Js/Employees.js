let employeeObj = function () {
    $(".save-new-item-btn").unbind().on("click", function () {
        let Model = GetData(".new-item-data", "#add-new-item-modal-body");
        $.ajax({
            type: "POST",
            url: "/Employee/AddEmployee",
            data: { NewEmployeeVM: Model },
            cache: !0,
            datatype: "json",
            success: function (response) {
                if (response.success) {
                    LoadList();
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
    let deleteEmployee = function (_EmployeeID) {
        $.ajax({
            type: "GET",
            url: "/Employee/DeleteEmployee",
            data: { EmployeeID: _EmployeeID },
            cache: !0,
            datatype: "Json",
            success: function (response) {
                if (response.success) {
                    toastr.success(response.responseText);
                    LoadList();
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
    let editEmployee = function () {
        $(".update-employee-item-btn").unbind().on("click", function () {
            let Model = GetData(".update-item-data", "#update-item-modal-body");
            $.ajax({
                type: "POST",
                url: "/Employee/UpdateEmployee",
                data: { UpdatedEmployee: Model },
                cache: !0,
                datatype: "json",
                success: function (response) {
                    if (response.success) {
                        LoadList();
                        $("#Update-item-modal").modal("hide");
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
            let _employeeID = Number($(this).attr("employee-id")),
                _employeeName = $(this).attr("employee-name");
            $("#Update-item-modal").find(".modal-title").text(`Update Employee: ${_employeeName}`);
            $.ajax({
                type: "GET",
                url: "/Employee/_Employee",
                data: { EmployeeID: _employeeID },
                cache: !0,
                datatype: "HTML",
                success: function (response) {
                    $("#Update-item-modal").find("#update-item-modal-body").html(response);
                },
                error: function (response) {
                    console.log(response.responsetext);
                }
            });
            $("#Update-item-modal").modal("show");
            editEmployee();
        });
        $(".delete-item").unbind().on("click", function () {
            let _employeeID = Number($(this).attr("employee-id"));
            deleteEmployee(_employeeID);
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
    let LoadList = function () {
        $("#TableWrapper").html($(".ContainerDivLoader").html());
        $.ajax({
            type: "GET",
            url: "/Employee/_EmployeesList",
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
        LoadList();
    };
    return {
        Initate: function () {
            Initate();
        },
    }
};
$(function () {
    let ItemObj = employeeObj();
    ItemObj.Initate();
});