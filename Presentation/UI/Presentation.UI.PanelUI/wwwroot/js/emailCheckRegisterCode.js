$(document).ready(function () {
    ToastifyModule.success(
        "Please check your email! If you don’t see the code, kindly check your spam or junk folder.",
        15000,
        "center"
    );

    $("#check-register-code-form").submit(function (event) {
        event.preventDefault();

        const registerCode = $("#Code").val();
        const userId = $("#UserId").val();

        ApiModule.post(
            '/User/EmailVerificationCodePage',
            { registerCode: registerCode, userId: userId }, 
            function (data) { 
                if (data) {
                    window.location.href = '/Home/Index';
                } else {
                    ToastifyModule.error("Invalid code. Please try again.");
                }
            },
            function (errorMessage) { 
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });

   
});
