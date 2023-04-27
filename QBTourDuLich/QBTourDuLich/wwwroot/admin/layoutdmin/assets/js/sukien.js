$(document).ready(function () {
    getAllEvent();
});

function getAllEvent() {
    $.ajax({
        url: `https://localhost:7079/api/APIEvent`,
        method: 'GET',
        contentType: 'json',
        dataType: 'json',
        error: function (response) {
            console.log("error");
        },
        success: function (response) {
            var count = parseInt(response.totalCount);
            const pageNumber = 1;
            const pageSize = 3;
            $.ajax({
                url: `https://localhost:7079/api/APIEvent/getPagination?pageSize=${pageSize}&pagenumber=${pageNumber}`,
                method: 'GET',
                contentType: 'json',
                dataType: 'json',
                error: function (response) {
                    console.log("error");
                },
                success: function (response) {
                    renderTable(response);
                    renderPagination(Math.ceil(count / pageSize), pageNumber);
                },
                fail: function (response) {
                    console.log("fail");
                }
            });
        }
    });
}
$("#form-event").submit(function (e) {
    e.preventDefault();
})
function renderPagination(totalPages, currentPage) {
    let pagination = '';
    for (let i = 1; i <= totalPages; i++) {
        pagination += `<button class="btn ${i === currentPage ? 'btn-primary' : 'btn-outline-primary'}" onclick="setPage(${i})">${i}</button> `;
    }
    document.getElementById('pagination_event').innerHTML = pagination;
}

function setPage(pageNumber) {
    const pageSize = 3;
    document.getElementById('page-number').innerHTML = pageNumber;
    $.ajax({
        url: `https://localhost:7079/api/APIEvent/getPagination?pageSize=${pageSize}&pagenumber=${pageNumber}`,
        method: 'GET',
        contentType: 'json',
        dataType: 'json',
        error: function (response) {
            console.log("error");
        },
        success: function (response) {
            renderTable(response);
        },
        fail: function (response) {
            console.log("fail");
        }
    });
}
function resetInput() {
    $("#MaSuKien").val("").change()
    $("#avatar").val("").change()
    $("#Mota").val("").change()
    $("#noidung").val("").change()
}
function InsertEvent() {
    var mask = $("#MaSuKien").val();
    var mota = $("#Mota").val();
    var noidung = $("#noidung").val();

    var formData = new FormData();

    formData.append("maSk", mask);
    formData.append("moTa", mota);
    formData.append("noiDung", noidung);
    formData.append("anh", $("#avatar")[0].files[0]);

    var url = 'https://localhost:7079/api/APIEvent/themEvent';
    $.ajax({
        url: url,
        method: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        error: function (error) {
            alert("Có lỗi xảy ra");
        },
        success: function (response) {
            alert("Thêm mới thành công");
            resetInput();
            getAllEvent(); //Gọi đến hàm lấy dữ liệu lên bảng
        }
    });
}
function UpdateEvent() {
    var mask = $("#MaSuKien").val();
    var mota = $("#Mota").val();
    var noidung = $("#noidung").val();

    var formData = new FormData();

    formData.append("maSk", mask);
    formData.append("moTa", mota);
    formData.append("noiDung", noidung);
    formData.append("anh", $("#avatar")[0].files[0]);

    var url = 'https://localhost:7079/api/APIEvent/capnhatEvent';
    $.ajax({
        url: url,
        method: 'PUT',
        processData: false,
        contentType: false,
        data: formData,
        error: function (error) {
            alert("Có lỗi xảy ra");
        },
        success: function (response) {
            alert("Cập nhật thành công");
            resetInput();
            getAllEvent(); //Gọi đến hàm lấy dữ liệu lên bảng
        }
    });
}

function updateEventFill(id) {
    var url = 'https://localhost:7079/api/APIEvent/getById?id=' + id;
    $.ajax({
        url: url,
        method: 'GET',
        contentType: 'json',
        dataType: 'json',
        error: function (response) {
            alert("Cập nhật không thành công");
        },
        success: function (response) {
            $("#MaSuKien").val(response.maSk.trim())
            $("#avatar").val(response.tenFileAnh.trim()).change()
            $("#Mota").val(response.moTa.trim()).change()
            $("#noidung").val(response.noiDung.trim()).change()
            /*$("#avatar").val(response.tenFileAnh.trim()).change()*/
        }
    });
}

function deleteEvent(id) {
    var url = 'https://localhost:7079/api/ApiEvent?input=' + id;
    $.ajax({
        url: url,
        method: 'DELETE',
        contentType: 'json',
        dataType: 'json',
        error: function (response) {
            /* alert("Xóa không thành công");*/
            getAllEvent();
        },
        success: function (response) {
            alert("Xóa thành công");
            getAllEvent();; //Gọi đến hàm lấy dữ liệu lên bảng
        }
    });
}
function renderTable(response) {
    const len = response.items.length;
    let table = '';
    let cls = "table-success";

    for (var i = 0; i < len; ++i) {
        if (i % 2 == 0) {
            cls = "table-primary";
        }
        else {
            cls = "table-success";
        }
        table = table + '<tr class="' + cls + '">';
        table = table + '<td>' + response.items[i].maSk.trim() + '</td>';
        table = table + '<td>' + response.items[i].tenNv.trim() + '</td>';
        table = table + `<td class="py-1">
                    <img src="../../img/anhEvent/${!!response.items[i].tenFileAnh ? response.items[i].tenFileAnh.trim() : 'default-avatar.png'}" alt="image" />
                </td>`;
        table = table + '<td>' + response.items[i].moTa.trim() + '</td>';
        table = table + '<td>' + response.items[i].noiDung.trim() + '</td>';
        /* table = table + '<td>' + response.items[i].moTa.trim() + '</td>';*/

        table = table + '<td>' + ' <button type="button" class="btn btn-gradient-info btn-rounded btn-icon" onclick="updateEventFill(\'' + response.items[i].maSk.trim() + '\')">Edit</i></button> ' + '</td>';
        table = table + '<td>' + ' <button type="button" class="btn btn-gradient-danger btn-rounded btn-icon" onclick="deleteEvent(\'' + response.items[i].maSk.trim() + '\')">Delete</button> ' + '</td>';
    }
    document.getElementById('tbody-event').innerHTML = table;
}