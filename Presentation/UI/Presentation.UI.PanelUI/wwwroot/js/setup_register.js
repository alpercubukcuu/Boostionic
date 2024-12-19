let currentStep = 0;
let totalSteps = 3;

function openModal() {
    const wrapper = document.getElementById('wrapper');
    const modal = document.getElementById('customModal');
    
    modal.style.display = 'block';

    setTimeout(() => {
        wrapper.classList.add('blurred');
    }, 10);
    
    updateButtons();
}

function updateProgress(customProgress = null) {
    const progressBar = document.getElementById('progressBar');
    const rocket = document.getElementById('rocket');
    const progress = customProgress !== null ? customProgress : (currentStep / totalSteps) * 100;
    
      
    progressBar.style.width = `${progress}%`;
    
    rocket.style.left = `calc(${progress}% + 5px)`; 
}

function updateButtons() {
    const backButton = document.querySelector('.backbutton');
    const nextButton = document.querySelector('.submitSelections');
    const finishButton = document.querySelector('.finishSetup');

    backButton.classList.add('d-none');
    nextButton.classList.add('d-none');
    finishButton.classList.add('d-none');

    if (currentStep === 0) {
    } else if (currentStep === 1) {
        backButton.classList.remove('d-none');
        nextButton.classList.remove('d-none');
    } else if (currentStep === 2) {
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
    updateProgress(100);   
     
    setTimeout(() => {
        closeModal();
    }, 500);

    setTimeout(() => {
        launchConfetti();
    }, 500);
    
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
    const selectedElements = document.querySelectorAll('.option1.selected');
    selectedOptions = Array.from(selectedElements).map(el => el.id);

    if (selectedOptions.length === 0) {
        ToastifyModule.error("Please select at least one option.");
        return;
    }

    console.log('Selected Options:', selectedOptions);
    nextStep();
}



document.addEventListener('DOMContentLoaded', () => {
    openModal();
    updateProgress();
});


function launchConfetti() {
    const duration = 3 * 1000; // Konfeti süresi (3 saniye)
    const animationEnd = Date.now() + duration;
    const defaults = { startVelocity: 30, spread: 360, ticks: 60, zIndex: 1000 };

    function randomInRange(min, max) {
        return Math.random() * (max - min) + min;
    }

    const interval = setInterval(() => {
        const timeLeft = animationEnd - Date.now();

        if (timeLeft <= 0) {
            clearInterval(interval);
            return;
        }

        const particleCount = 50 * (timeLeft / duration);      
        confetti(
            Object.assign({}, defaults, {
                particleCount,
                origin: { x: randomInRange(0.1, 0.9), y: Math.random() - 0.2 },
            })
        );
    }, 250);
}
