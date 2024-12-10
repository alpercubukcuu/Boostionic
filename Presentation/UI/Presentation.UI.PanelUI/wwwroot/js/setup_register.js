let currentStep = 0;
let totalSteps = 3;

function openModal() {
    const wrapper = document.getElementById('wrapper');
    const modal = document.getElementById('customModal');

    // Önce modal'ı görünür yap
    modal.style.display = 'block';

    setTimeout(() => {
        wrapper.classList.add('blurred');
    }, 10);
}

function updateProgress() {
    const progressBar = document.getElementById('progressBar');
    const rocket = document.getElementById('rocket');
    const progress = (currentStep / totalSteps) * 100;

    // Progress bar genişliğini güncelle
    progressBar.style.width = `${progress}%`;

    // Roket pozisyonunu güncelle (progress bar'ın tam üstünde ortalanmış şekilde)
    rocket.style.left = `calc(${progress}% + 5px)`; // Roketi tam ortaya hizala
}

function nextStep() {
    if (currentStep < totalSteps) {
        document.getElementById(`step${currentStep}`).classList.remove('active');
        currentStep++;
        document.getElementById(`step${currentStep}`).classList.add('active');
        updateProgress(); // Adım değiştikçe progress bar ve roket hareketi
    }
}

function prevStep() {
    if (currentStep > 0) {
        document.getElementById(`step${currentStep}`).classList.remove('active');
        currentStep--;
        document.getElementById(`step${currentStep}`).classList.add('active');
        updateProgress(); // Adım değiştikçe progress bar ve roket hareketi
    }
}

function finishSetup() {
    alert('Setup Complete!');
    closeModal();
}

document.addEventListener('DOMContentLoaded', () => {
    openModal();
    updateProgress();
});
