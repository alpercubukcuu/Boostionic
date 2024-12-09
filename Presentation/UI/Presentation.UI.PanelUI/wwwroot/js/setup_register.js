let currentStep = 1;

function openModal() {
    const wrapper = document.getElementById('wrapper');
    const modal = document.getElementById('customModal');

    // Önce modal'ı görünür yap
    modal.style.display = 'block';

    setTimeout(() => {
        wrapper.classList.add('blurred');
    }, 10);
}

function closeModal() {
    document.getElementById('wrapper').classList.remove('blurred');
    const modal = document.getElementById('customModal');

    // Blur efektini kaldır
    wrapper.style.filter = 'none';

    // Modal'ı kapat
    setTimeout(() => {
        modal.style.display = 'none';
    }, 300); // Blur kaldırma geçişinden sonra modal'ı gizle
}


function nextStep() {
    document.getElementById(`step${currentStep}`).classList.remove('active');
    currentStep++;
    document.getElementById(`step${currentStep}`).classList.add('active');
}

function prevStep() {
    document.getElementById(`step${currentStep}`).classList.remove('active');
    currentStep--;
    document.getElementById(`step${currentStep}`).classList.add('active');
}

function finishSetup() {
    alert('Setup Complete!');
    closeModal();
}

document.addEventListener('DOMContentLoaded', () => {
    openModal();
});
