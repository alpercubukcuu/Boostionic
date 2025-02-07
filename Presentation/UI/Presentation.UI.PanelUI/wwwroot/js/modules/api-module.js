const ApiModule = {
    post: function (url, data, successCallback, errorCallback) {
        this.ajaxRequest("POST", url, data, "application/x-www-form-urlencoded", successCallback, errorCallback);
    },
    postJson: function (url, data, successCallback, errorCallback) {
        this.ajaxRequest("POST", url, JSON.stringify(data), "application/json", successCallback, errorCallback);
    },
    get: function (url, successCallback, errorCallback) {
        this.ajaxRequest("GET", url, null, "application/x-www-form-urlencoded", successCallback, errorCallback);
    },
    deleteJson: function (url, successCallback, errorCallback) {
        this.ajaxRequest("DELETE", url, null, "application/json", successCallback, errorCallback);
    },
    ajaxRequest: function (type, url, data, contentType, successCallback, errorCallback) {
        $("#loading-spinner").show();

        $.ajax({
            type: type,
            url: url,
            data: data,
            contentType: contentType,
            success: function (resultData) {
                $("#loading-spinner").hide();
                if (successCallback) successCallback(resultData);
            },
            error: function (xhr) {
                $("#loading-spinner").hide();
                let errorMessage = xhr.responseText && xhr.responseText.trim() !== ""
                    ? xhr.responseText
                    : "An error occurred while processing your request.";
                if (errorCallback) {
                    errorCallback(errorMessage);
                } else {
                    console.error(errorMessage);
                }
            }
        });
    }
};

window.ApiModule = ApiModule;
