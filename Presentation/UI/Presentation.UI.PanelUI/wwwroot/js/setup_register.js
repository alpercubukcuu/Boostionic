let currentStep = 1;

function nextStep() {
    document.getElementById(`step${currentStep}`).classList.remove("active");
    document.getElementById(`step${currentStep}`).classList.add("d-none");
    currentStep++;
    document.getElementById(`step${currentStep}`).classList.add("active");
    document.getElementById(`step${currentStep}`).classList.remove("d-none");
}

function prevStep() {
    document.getElementById(`step${currentStep}`).classList.remove("active");
    document.getElementById(`step${currentStep}`).classList.add("d-none");
    currentStep--;
    document.getElementById(`step${currentStep}`).classList.add("active");
    document.getElementById(`step${currentStep}`).classList.remove("d-none");
}

function finishSetup() {
    alert("Setup Complete!");
    // Burada backend'e istekte bulunabilir ve setup durumunu güncelleyebilirsiniz
    location.reload(); // Sayfayı yeniler
}

// Modal otomatik açılır
$(document).ready(function () {
    $('#setupWizardModal').modal({
        backdrop: 'static', // Kullanıcının modal'ı kapatmasını engeller
        keyboard: false // Escape tuşuyla kapatmayı engeller
    });
    $('#setupWizardModal').modal('show'); // Modal'ı otomatik gösterir
});