document.addEventListener("DOMContentLoaded", function () {
    document.addEventListener("click", function (event) {
        if (event.target.classList.contains("remove-workspace")) {
            event.preventDefault();

            let workspaceId = event.target.getAttribute("data-id");

            if (!workspaceId) {
                console.error("Workspace ID not found!");
                return;
            }

            ApiModule.deleteJson(
                `/Workspace/DeleteWorkspace/${workspaceId}`,
                null, 
                function (data) {
                    if (data) {
                        console.log("Workspace deleted successfully:");

                        event.target.closest("li").remove();

                        ToastifyModule.success("Workspace deleted successfully!");
                    } else {
                        console.error("Deletion failed:", result.message);
                        ToastifyModule.error(result.message || "Failed to delete workspace.");
                    }
                },
                function (errorMessage) {
                    console.error("An error occurred:", errorMessage);
                    ToastifyModule.error(errorMessage || "Connection error! Please try again.");
                }
            );
        }
    });
});
