$(document).ready(function () {
    $("#forgot-password-form").submit(function (event) {
        event.preventDefault();

        var email = $("#Email").val(); // Email alanındaki değeri al
        var emailType = 1;

        $("#loading-spinner").show();
        $.ajax({
            type: "POST",
            url: '/User/CheckUserEmail',
            data: {email: email, emailType: emailType},
            contentType: "application/x-www-form-urlencoded",
            success: function (resultData) {
                $("#loading-spinner").hide();
                console.log(resultData);
                if (resultData) {
                    Toastify({
                        text: "A reset password code has been sent to your email.",
                        duration: 4000,
                        gravity: "top",
                        position: "right",
                        style: {
                            background: "linear-gradient(to right, #00b09b, #96c93d)"
                        }
                    }).showToast();

                    setTimeout(function () {
                        window.location.href = '/User/CodePage?userId=' + resultData;
                    }, 4000);
                } else {
                    Toastify({
                        text: resultData.message || "Error: Email is not valid.",
                        duration: 5000,
                        gravity: "top",
                        position: "right",
                        style: {
                            background: "linear-gradient(to right, #ff5f6d, #ffc371)"
                        }
                    }).showToast();
                }
            },
            error: function (resultData) {
                console.log(resultData);
                $("#loading-spinner").hide();               
                Toastify({
                    text: "An error occurred while processing your request.",
                    duration: 4000,
                    gravity: "top",
                    position: "right",
                    style: {
                        background: "linear-gradient(to right, #ff5f6d, #ffc371)"
                    }
                }).showToast();
            }
        });
    });
});
