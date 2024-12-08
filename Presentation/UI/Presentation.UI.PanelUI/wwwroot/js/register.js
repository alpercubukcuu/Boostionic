$(document).ready(function () {
    $("#register-form").submit(function (event) {
        event.preventDefault();
                
        const formData = {
            Name: $("#register-name").val(),
            Surname: $("#register-surname").val(),
            Email: $("#register-email").val(),
            PasswordHash: $("#register-password").val(),
        };
               
        ApiModule.postJson(
            '/User/Register/',
            formData,           
            function (data) { 
                if (data) {
                    setTimeout(function () {
                        window.location.href = '/SetupSetting/RegisterCodePage?userId=' + data;
                    }, 4000);
                } else {
                    ToastifyModule.error("Invalid login attempt. Please try again.");
                }
            },
            function (errorMessage) {                
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });
});
