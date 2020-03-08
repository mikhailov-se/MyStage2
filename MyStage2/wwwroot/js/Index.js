$(document).ready(function() {

    setDefaultCountItemsOnPage();

    $("#modal-placeholder").on("hidden.bs.modal", () => { filter(); });

    $(".fixed-table-pagination").before($("#removeButtonDiv"));


    $("#table").on("check.bs.table check-all.bs.table ", () => { showRemoveButton(); });

    $("#table").on("uncheck.bs.table uncheck-all.bs.table", () => { hideRemoveButton(); });

    $("#table").on("dbl-click-row.bs.table", (row, $element) => { showModalEdit($element.id); });


});


function showModalEdit(announsmentId) {
    var placeholderElement = $("#modal-placeholder");


    $.ajax("/Announsments/GetModalEditAnnounsment/" + announsmentId).done(function(data) {
        placeholderElement.html(data);
        placeholderElement.find(".modal").modal("show");
    });
}


function filter() {
    var searchString = $("#searchBox").val();
    var fromDate = $("#fromDate").val();
    var toDate = $("#toDate").val();
    var selectedUserId = $("#listUsers").val();

    var url = "/Announsments/GetAnnounsmentsJson?searchString=" +
        searchString +
        "&" +
        "fromDate=" +
        fromDate +
        "&" +
        "toDate=" +
        toDate +
        "&" +
        "selectedUserId=" +
        selectedUserId;


    $("#table").bootstrapTable("refresh",
        {
            url: url
        });
}


function setCountRowsOnPage(but, count) {

    document.getElementsByClassName("setCountOnPageButton").forEach((item) => {
        item.style.background = "white";
        item.style.color = "#4200be";


    });;

    but.style.background = "#4200be";
    but.style.color = "white";


    $("#table").bootstrapTable("refresh",
        {
            pageSize: count
        });
}


function removeAnnounsments() {

    var selectedRows = $("#table").bootstrapTable("getSelections");

    var ids = selectedRows.map(function(row) {
        return row.id;
    });

    if (confirm(" Удалить записи  " + ids.length)) {

        $.ajax({
            url: "/Announsments/Delete",
            type: "POST",
            data: { ids: ids },
            success: () => {
                $("#table").bootstrapTable("remove", { field: "id", values: ids });
                hideRemoveButton();
                toastr.success("Запись успешно удалена", "Успех");
            },

            error: () => {
                toastr.error("Внутреняя ошибка", "Ошибка");
            }
        });
    }
}

function setDefaultCountItemsOnPage() {

    var button = document.getElementsByClassName("setCountOnPageButton")[0];
    setCountRowsOnPage(button, 10);

}


function showModalAdd() {
    var placeholderElement = $("#modal-placeholder");


    $.ajax("/Announsments/GetModalAddAnnounsment/").done(function(data) {
        placeholderElement.html(data);
        placeholderElement.find(".modal").modal("show");
    });
}

function hideRemoveButton() {
    document.getElementById("removeButton").style.visibility = "hidden";

}

function showRemoveButton() {
    document.getElementById("removeButton").style.visibility = "visible";

}

