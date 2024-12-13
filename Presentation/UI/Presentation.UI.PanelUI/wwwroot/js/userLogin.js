$(document).ready(function () {
    $("#login-form").submit(function (event) {
        event.preventDefault();

        
        const formData = {
            Email: $("#signin-email").val(),
            Password: $("#signin-password").val(),
            RememberMe: $("#RememberMe").is(":checked")
        };
               
        ApiModule.postJson(
            '/User/UserLogin/', 
            formData,           
            function (data) { 
                if (data && data.token) {
                    ToastifyModule.success("Login successful! Redirecting...");

                    setTimeout(function () {
                        window.location.href = '/Home/index';
                    }, 3000);
                } else {
                    ToastifyModule.error("Invalid login attempt. Please try again.");
                }
            },
            function (errorMessage) {
                $("#signin-password").val(""); 
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });
});
