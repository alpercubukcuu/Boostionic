$(document).ready(function () {
    $("#logoutButton").on("click", function (event) {
        event.preventDefault(); 
        console.log("clicked");
        if (confirm("Are you sure you want to logout?")) {
            $.ajax({
                url: "/User/UserLogout",
                type: "POST",
                success: function (response) {
                    window.location.href = '/User/LoginPage';
                },
                error: function (xhr, status, error) {
                    console.error("Logout failed:", error);
                    alert("An error occurred while logging out. Please try again.");
                }
            });
        }
    });
});
