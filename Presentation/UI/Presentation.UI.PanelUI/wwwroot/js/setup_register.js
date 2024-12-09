$(document).ready(function () {
    const $steps = $(".step");
    const $formSteps = $(".form-step");
    let currentStep = 0;

    const updateSteps = () => {
        $steps.each(function (index) {
            $(this).toggleClass("active", index === currentStep);
        });

        $formSteps.each(function (index) {
            $(this).toggleClass("active", index === currentStep);
        });
    };

    $(".next").on("click", function () {
        currentStep = Math.min(currentStep + 1, $steps.length - 1);
        updateSteps();
    });

    $(".prev").on("click", function () {
        currentStep = Math.max(currentStep - 1, 0);
        updateSteps();
    });

    updateSteps();
});
