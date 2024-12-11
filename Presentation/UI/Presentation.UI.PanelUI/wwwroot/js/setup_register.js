let currentStep = 0;
let totalSteps = 2;

function openModal() {
    const wrapper = document.getElementById('wrapper');
    const modal = document.getElementById('customModal');

    // Önce modal'ı görünür yap
    modal.style.display = 'block';

    setTimeout(() => {
        wrapper.classList.add('blurred');
    }, 10);

    // Başlangıçta butonları güncelle
    updateButtons();
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

function updateButtons() {
    const backButton = document.querySelector('.backbutton');
    const nextButton = document.querySelector('.submitSelections');
    const finishButton = document.querySelector('.finishSetup');

    // Tüm butonları önce gizle
    backButton.classList.add('d-none');
    nextButton.classList.add('d-none');
    finishButton.classList.add('d-none');

    if (currentStep === 0) {
        // Step 0'da hiç buton gösterilmeyecek
    } else if (currentStep === 1) {
        // Step 1'de "Back" ve "Next" butonları gösterilecek
        backButton.classList.remove('d-none');
        nextButton.classList.remove('d-none');
    } else if (currentStep === 2) {
        // Step 2'de "Back" ve "Finish" butonları gösterilecek
        backButton.classList.remove('d-none');
        finishButton.classList.remove('d-none');
    }
}

function nextStep() {
    if (currentStep < totalSteps) {
        document.getElementById(`step${currentStep}`).classList.remove('active');
        currentStep++;
        document.getElementById(`step${currentStep}`).classList.add('active');
        updateProgress(); 
        updateButtons(); 
    }
}

function prevStep() {
    if (currentStep > 0) {
        document.getElementById(`step${currentStep}`).classList.remove('active');
        currentStep--;
        document.getElementById(`step${currentStep}`).classList.add('active');
        updateProgress(); 
        updateButtons(); 
    }
}

function closeModal() {
    document.getElementById('wrapper').classList.remove('blurred');
    const modal = document.getElementById('customModal');

    wrapper.style.filter = 'none';

    // Modal'ı kapat
    setTimeout(() => {
        modal.style.display = 'none';
    }, 300);
}

function finishSetup() {
    alert('Setup Complete!');
    closeModal();
}

function selectOption(option) {
    console.log(option + " choosed!");
    nextStep();
}

let selectedOptions = [];

function toggleOption(element, option) {
    if (selectedOptions.includes(option)) {
        selectedOptions = selectedOptions.filter(item => item !== option);
        element.classList.remove('selected');
    } else {
        selectedOptions.push(option);
        element.classList.add('selected');
    }
}

function submitSelections() {
    if (selectedOptions.length === 0) {
        ToastifyModule.error("Please select at least one option.");
        return;
    }


    console.log('Selected Options:', selectedOptions);
    // Burada seçili seçenekleri backend'e gönderme veya diğer adımlara geçiş yapılabilir
    nextStep();
}

// Sayfa yüklendiğinde modalı aç ve progress bar'ı başlat
document.addEventListener('DOMContentLoaded', () => {
    openModal();
    updateProgress();
});
