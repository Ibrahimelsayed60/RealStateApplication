var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblBookings').DataTable({
        "ajax": {
            url: '/booking/getall'
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'bookingName', "width": "15%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'bookingEmail', "width": "15%" },
            { data: 'status', "width": "10%" },
            { data: 'checkInDate', "width": "10%" },
            { data: 'numberOfNights', "width": "10%" },
            { data: 'totalCost', render: $.fn.dataTable.render.number(',', '.', 2), "width": "10%" },
            {
                data: 'Id',
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                        <a href="/booking/bookingDetails?bookingId=${data}" class="btn btn-outline-warning mx-2">
                            <i class="bi bi-pencil-square"></i> Details
                        </a>
                    </div>`
                }
            }
        ]
    });
}
