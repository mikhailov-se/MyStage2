    $(document).ready(function() {

        document.getElementById("AddAnnounsmentForm").addEventListener('submit',
            function (e) {
                e.preventDefault();
                sumbitForm();
            });

});


    function sumbitForm() {

        $('#AddAnnounsmentForm').ajaxSubmit({
            success: () => {
                toastr.success('Запись успешно сохранена', "Успех");
                modalHide();
            },
            error: () => {
                toastr.error('Внутреняя ошибка', "Ошибка");
                modalHide();
            }
        });

}

    function modalHide() {
        var placeholderElement = $('#modal-placeholder');

    placeholderElement.find('.modal').modal('hide');
}
