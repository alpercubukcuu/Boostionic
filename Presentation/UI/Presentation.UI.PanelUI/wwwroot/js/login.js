$(document).ready(function () {
    $("#login-form").submit(function (event) {
        event.preventDefault();

        var formData = {
            Email: $("#signin-email").val(),
            Password: $("#signin-password").val(),
            RememberMe: $("#RememberMe").is(":checked")
        };

        $.ajax({
            type: "POST",
            url: '/User/UserLogin/',
            data: JSON.stringify(formData),
            contentType: "application/json",
            success: function (data) {
                if (data && data.token) {
                    Toastify({
                        text: "Login successful! Redirecting...",
                        duration: 3000,
                        gravity: "top",
                        position: "right",
                        style: {
                            background: "linear-gradient(to right, #00b09b, #96c93d)"
                        }
                    }).showToast();

                    setTimeout(function () {
                        window.location.href = '/Home/index';
                    }, 3000);
                } else {
                    Toastify({
                        text: "Invalid login attempt. Please try again.",
                        duration: 5000,
                        gravity: "top",
                        position: "right",
                        style: {
                            background: "linear-gradient(to right, #ff5f6d, #ffc371)"
                        }
                    }).showToast();
                }
            },
            error: function (xhr) {
                $("#Password").val("");
                let errorMessage = xhr.responseText && xhr.responseText.trim() !== ""
                    ? xhr.responseText
                    : "An error occurred while processing your request.";
                Toastify({
                    text: errorMessage,
                    duration: 5000,
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
