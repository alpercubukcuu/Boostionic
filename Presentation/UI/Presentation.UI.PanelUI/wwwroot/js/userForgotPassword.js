$(document).ready(function () {
    $("#forgot-password-form").submit(function (event) {
        event.preventDefault();

        const email = $("#Email").val();
        const emailType = 1;

   
        ApiModule.post(
            '/User/CheckUserEmail',
            { email: email, emailType: emailType },
            function (resultData) { 
                if (resultData) {
                    ToastifyModule.success("A reset password code has been sent to your email.");

                    setTimeout(function () {
                        window.location.href = '/User/CodePage?userId=' + resultData;
                    }, 4000);
                } else {
                    ToastifyModule.error(resultData.message || "Error: Email is not valid.");
                }
            },
            function (errorMessage) { 
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );
    });
});
