$(document).ready(function () {
    $("#reset-password-form").submit(function (event) {
        event.preventDefault();
        var formData = {
            NewPassword: $("#NewPassword").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),
            UserId: $("#UserId").val()
        };
        if (formData.NewPassword !== formData.ConfirmPassword) {
            alert("Passwords do not match. Please try again.");
            return;
        }
        $.ajax({
            type: "POST",
            url: '/User/UpdatePassword/',
            data: JSON.stringify(formData),
            contentType: "application/json",
            success: function (data) {
                if (data) {
                    Toastify({
                        text: "Password changed successfully.",
                        duration: 3000,
                        gravity: "top",
                        position: "right",
                        style: {
                            background: "linear-gradient(to right, #00b09b, #96c93d)"
                        }
                    }).showToast();
                    setTimeout(function () {
                        window.location.href = '/User/LoginPage';
                    }, 4000);
                } else {
                    alert("Invalid login attempt.");
                }
            }
            ,
            error: function (xhr) {
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
        })
        ;
    });
});