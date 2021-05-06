
    var dataTable;
        $(document).ready(function () {
        loadDataTable();
        })
        function loadDataTable() {
        dataTable = $("#dataTable").DataTable({
            "ajax": {
                url: "/api/books",
                method: "GET",
                dataType: "json"
            },
            "columns": [

                { width: "30%", data: "name" },
                { width: "25%", data: "author" },
                { width: "25%", data: "isbn" },
                {
                    width: "20%",
                    data: "id", render: function (data) {
                        return `<div>
                            <button  onClick=deleteBook(${data})  class="btn btn-danger btn-sm">Delete</button>
                            &nbsp
                             <a href="/Books/Update/${data}"  class="btn btn-success btn-sm text-white">Edit</a>
                              </div>  `}
                }
            ],
            "language": {
                "emptyTable": "No data found"
            }, width: "100%"
        })
    }

        function deleteBook(id) {
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not able to recover",
            icon: "warning",
            dangerMode: true,
            confirmButtonText: "Yes, delete it!",
            confirmButtonClass: "btn-danger",
            showCancelButton: true,
            cancelButtonClass: "btn-info"
        }, ((isConfirm) => {
            if (isConfirm) {
                $.ajax({
                    url: '/api/books?id=' + id,
                    method: "DELETE",
                })
                    .done(function (data) {
                        if (data.status) {
                            dataTable.ajax.reload();
                            toastr.success(data.msg);
                        }
                        else
                            toastr.warning(data.msg);
                    })
                    .fail(() => {
                        console.info("There is some problem.")
                    })
            }
        })
        )

    }

