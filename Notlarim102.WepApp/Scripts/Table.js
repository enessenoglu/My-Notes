var editor; // use a global for the submit and return data rendering in the examples

$(document).ready(function () {
    editor = new $.fn.dataTable.Editor({
        ajax: "../php/staff.php",
        table: "#example",
        display: 'envelope',
        fields: [{
            label: "First name:",
            name: "first_name"
        }, {
            label: "Last name:",
            name: "last_name"
        }, {
            label: "Position:",
            name: "position"
        }, {
            label: "Office:",
            name: "office"
        }, {
            label: "Extension:",
            name: "extn"
        }, {
            label: "Start date:",
            name: "start_date",
            type: "datetime"
        }, {
            label: "Salary:",
            name: "salary"
        }
        ]
    });

    // New record
    $('a.editor_create').on('click', function (e) {
        e.preventDefault();

        editor
            .title('Create new record')
            .buttons({ "label": "Add", "fn": function () { editor.submit() } })
            .create();
    });

    // Edit record
    $('#example').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();

        editor
            .title('Edit record')
            .buttons({ "label": "Update", "fn": function () { editor.submit() } })
            .edit($(this).closest('tr'));
    });

    // Delete a record
    $('#example').on('click', 'a.editor_remove', function (e) {
        e.preventDefault();

        editor
            .title('Edit record')
            .message("Are you sure you wish to delete this row?")
            .buttons({ "label": "Delete", "fn": function () { editor.submit() } })
            .remove($(this).closest('tr'));
    });

    $('#example').DataTable({
        ajax: "../php/staff.php",
        columns: [
            {
                data: null, render: function (data, type, row) {
                    // Combine the first and last names into a single table field
                    return data.first_name + ' ' + data.last_name;
                }
            },
            { data: "position" },
            { data: "office" },
            { data: "extn" },
            { data: "start_date" },
            { data: "salary", render: $.fn.dataTable.render.number(',', '.', 0, '$') },
            {
                data: null,
                className: "center",
                defaultContent: '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_remove">Delete</a>'
            }
        ]
    });
});