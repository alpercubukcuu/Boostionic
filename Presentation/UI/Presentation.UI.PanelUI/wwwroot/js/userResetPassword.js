$(document).ready(function () {
    $("#reset-password-form").submit(function (event) {
        event.preventDefault();

        const formData = {
            NewPassword: $("#NewPassword").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),
            UserId: $("#UserId").val()
        };
               
        if (formData.NewPassword !== formData.ConfirmPassword) {
            ToastifyModule.error("Passwords do not match. Please try again.");
            return;
        }
               
        ApiModule.postJson(
            '/User/UpdatePassword/',
            formData,
            function (data) {
                if (data) {
                    ToastifyModule.success("Password changed successfully.");

                    setTimeout(function () {
                        window.location.href = '/User/LoginPage';
                    }, 4000);
                } else {
                    ToastifyModule.error("Invalid login attempt.");
                }
            },
            function (errorMessage) { 
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });
});
