$(document).ready(function () {
    $("#register-form").submit(function (event) {
        event.preventDefault();

        const formData = {
            Name: $("#register-name").val(),
            Surname: $("#register-surname").val(),
            Email: $("#register-email").val(),
            PasswordHash: $("#register-password").val(),
        };

        if (!formData.Name || !formData.Surname || !formData.Email || !formData.PasswordHash) {
            ToastifyModule.error("All fields are required.");
            return;
        }

        const buttonText = document.getElementById("button-text");
        const loadingText = document.getElementById("loading-text");

        buttonText.style.display = "none";
        loadingText.style.display = "inline-block";

        const text = "Processing...";
        let index = 0;
        const interval = setInterval(() => {
            loadingText.textContent = text.substring(0, index + 1);
            index++;
            if (index === text.length) {
                index = 0;
            }
        }, 200);

        ApiModule.postJson(
            '/User/Register/',
            formData,
            function (data) {
                if (data) {
                    stopLoading();
                    setTimeout(function () {
                        window.location.href = '/User/EmailRegisterCodePage?userId=' + data;
                    }, 4000);
                } else {
                    stopLoading();
                    ToastifyModule.error("Invalid login attempt. Please try again.");
                }
            },
            function (errorMessage) {
                stopLoading();
                ToastifyModule.error(errorMessage || "An error occurred while processing your request.");
            }
        );

    });

    function stopLoading(interval) {
        clearInterval(interval);
        const buttonText = document.getElementById("button-text");
        const loadingText = document.getElementById("loading-text");

        loadingText.style.display = "none";
        buttonText.style.display = "inline";
    }

});
