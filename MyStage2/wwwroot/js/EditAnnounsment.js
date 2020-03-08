$(document).ready(function() {

    document.getElementById("UpdateAnnounsment").addEventListener("submit",
        function(e) {
            e.preventDefault();
            sumbitForm();
        });

});


function sumbitForm() {
    $("#UpdateAnnounsment").ajaxSubmit({
        success: () => {
            toastr.success("Запись успешно сохранена", "Успех");
            modalHide();
        },
        error: () => {
            toastr.error("Внутреняя ошибка", "Ошибка");
            modalHide();
        }
    });

}

function modalHide() {
    var placeholderElement = $("#modal-placeholder");

    placeholderElement.find(".modal").modal("hide");
}

function removeAnnounsment(id) {

    if (confirm(" Удалить записи  ")) {

        $.ajax({
            url: "/Announsments/Delete",
            type: "POST",
            data: { ids: id },
            success: () => { toastr.success("Запись успешно удалена", "Успех"); },
            error: () => { toastr.error("Внутреняя ошибка", "Ошибка"); }

        });
        $("#table").bootstrapTable("remove", { field: "id", values: id });
        $("#table").bootstrapTable("refresh",
            {
                url: "/Announsments/GetAnnounsmentsJson"
            });
    }

}

