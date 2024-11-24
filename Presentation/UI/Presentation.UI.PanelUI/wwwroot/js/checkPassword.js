$(document).ready(function () {
    Toastify({
        text: "Please check your email! If you don’t see the code, kindly check your spam or junk folder.",
        duration: 15000,
        gravity: "top", // Toastify ayarlarında zorunlu, ancak daha sonra konumlandırmayı değiştireceğiz
        position: "center",
        style: {
            background: "linear-gradient(to right, #00b09b, #96c93d)"
        }
    }).showToast();

    $("#check-code-form").submit(function (event) {
        event.preventDefault();

        var code = $("#Code").val();
        var userId = $("#UserId").val();

        $.ajax({
            type: "POST",
            url: '/User/CheckCode',
            data: {resetCode: code, userId: userId},
            contentType: "application/x-www-form-urlencoded",
            success: function (resultData) {
                if (resultData) {
                    window.location.href = '/User/ResetPassword?userId=' + userId;
                } else {
                    alert(resultData.message || "Error: Email is not valid.");
                }
            },
            error: function () {
                alert("An error occurred while processing your request.");
            }
        });
    });

    $('#resend-code-btn').click(function () {
        var userId = $("#UserId").val();
        var emailType = 1; // Adjust or set dynamically as needed

        $.ajax({
            type: "POST",
            url: '/User/ResendCode',
            data: {userId: userId, emailType: emailType},
            success: function (response) {
                // Success notification
                Toastify({
                    text: "A new code has been sent to your email address.",
                    duration: 4000,
                    gravity: "top",
                    position: "right",
                    style: {
                        background: "linear-gradient(to right, #00b09b, #96c93d)"
                    }
                }).showToast();
            },
            error: function () {
                // Error notification
                Toastify({
                    text: "An error occurred while sending the code.",
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
