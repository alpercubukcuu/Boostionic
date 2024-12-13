$(document).ready(function () {
    ToastifyModule.success(
        "Please check your email! If you don’t see the code, kindly check your spam or junk folder.",
        15000,
        "center"
    );

    $("#check-code-form").submit(function (event) {
        event.preventDefault();

        const resetCode = $("#Code").val();
        const userId = $("#UserId").val();

        ApiModule.post(
            '/User/CheckCode',
            { resetCode: resetCode, userId: userId }, 
            function (data) { 
                if (data) {
                    window.location.href = '/User/ResetPassword?userId=' + userId;
                } else {
                    ToastifyModule.error("Invalid code. Please try again.");
                }
            },
            function (errorMessage) { 
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });

    
    $('#resend-code-btn').click(function () {
        const userId = $("#UserId").val();

        ApiModule.post(
            '/User/ResendCode',
            { userId: userId, emailType: 1 }, 
            function (response) { 
                ToastifyModule.success("A new code has been sent to your email address.");
            },
            function (errorMessage) { 
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });
});
