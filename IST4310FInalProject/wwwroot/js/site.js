

//function Delete(url, projectId) {
//    Swal.fire({
//        title: "Are you sure?",
//        text: "You won't be able to revert this!",
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#0d6efd",
//        cancelButtonColor: "#dc3545",
//        confirmButtonText: "Yes, delete it!"
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    if (data.success) {
//                        // Remove the project row from the table
//                        $("#project-" + projectId).hide();

//                        Swal.fire(
//                            "Deleted!",
//                            "",
//                            "success"
//                        );
//                    } else {
//                        Swal.fire(
//                            "Error!",
//                            data.message,
//                            "error"
//                        );
//                    }
//                }
//            });
//        }
//    });
//}

let deleteProjectId = null;  // Variable to store the projectId of the project to delete

function Delete(url, projectId) {
    // Store the projectId in the global variable
    deleteProjectId = projectId;

    // Show the Bootstrap modal
    $('#deleteModal').modal('show');

    // Handle the click event for the confirm delete button
    $('#confirmDeleteBtn').off('click').on('click', function () {
        // Perform the delete request
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (data) {
                if (data.success) {
                    // Hide the modal
                    $('#deleteModal').modal('hide');

                    // Remove the project row from the table
                    $("#project-" + deleteProjectId).hide();

                    
                } else {
                    // Optionally, show an error message
                    alert('Error: ' + data.message);
                }
            }
        });
    });
}
