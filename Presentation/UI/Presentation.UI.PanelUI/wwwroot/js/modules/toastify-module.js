const ToastifyModule = {
    success: function (message, duration = 5000, position = "right") {
        this.showToast(message, "success", duration, position);
    },
    error: function (message, duration = 5000, position = "right") {
        this.showToast(message, "error", duration, position);
    },
    showToast: function (message, type, duration, position) {
        const colors = {
            success: "linear-gradient(to right, #00b09b, #96c93d)",
            error: "linear-gradient(to right, #ff5f6d, #ffc371)"
        };

        Toastify({
            text: message,
            duration: duration,
            gravity: "top",
            position: position,
            style: {
                background: colors[type] || colors.error
            }
        }).showToast();
    }
};

window.ToastifyModule = ToastifyModule;
