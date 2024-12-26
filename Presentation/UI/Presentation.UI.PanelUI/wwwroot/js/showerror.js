
document.addEventListener("DOMContentLoaded", function () {
    
    let errorMessage = CookieModule.getCookie("ErrorMessage");
       
    if (errorMessage) {
        errorMessage = decodeURIComponent(errorMessage);
        ToastifyModule.error(errorMessage);
        CookieModule.deleteCookie("ErrorMessage");
    }
    else {
        console.log("Error message doesn't exist");
    }
});
