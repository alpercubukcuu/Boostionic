let currentStep = 0;
let totalSteps = 3;


let workspaceData = {
    usage: "",          
    features: [],       
    workspaceName: "",
    userId: ""
};

function openModal() {
    const wrapper = document.getElementById('wrapper');
    const modal = document.getElementById('customModal');

    const userIdInput = document.getElementById('setup_user_id');
    if (userIdInput) {
        workspaceData.userId = userIdInput.value;
        console.log("User ID:", workspaceData.userId);
    } else {
        console.error("User ID input not found.");
    }

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

async function finishSetup() {
    try {

        const workspace_name = document.getElementById('work_space_id');
        if (workspace_name) {
            workspaceData.workspaceName = workspace_name.value;
            console.log("User ID:", workspace_name.userId);
        } else {
            console.error("User ID input not found.");
        }

        console.log("Final Workspace Data:", workspaceData);
       
        const response = await fetch('/SetupRegister/SetupSettingSave', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(workspaceData),
        });

        if (!response.ok) {
            ToastifyModule.error(result.message || "Failed to save workspace data.");
            throw new Error("Failed to save workspace data.");
        }

        const result = await response.json();

        // Gelen cevaba göre modal'ı kapat ve konfeti patlat
        if (result.success) {
            console.log("Workspace saved successfully:", result.message);

            
            updateProgress(100);           
            closeModal();
            setTimeout(() => {
                launchConfetti();
            }, 500);

        } else {
            console.error("Error from server:", result.message);
            ToastifyModule.error(result.message || "An error occurred.");
        }
    } catch (error) {
        console.error("Error in finishSetup:", error);
        ToastifyModule.error("An error occurred while saving your data. Please try again.");
    }
}


function selectOption(option) {
    workspaceData.usage = option;
    console.log(option + " choosed!");
    nextStep();
}

let selectedOptions = [];

function toggleOption(element, option) {
    if (selectedOptions.includes(option)) {
        workspaceData.features = workspaceData.features.filter(item => item !== option);
        element.classList.remove('selected');
    } else {
        workspaceData.features.push(option);
        element.classList.add('selected');
    }
    console.log('Step 2 - Features:', workspaceData.features);
}

function submitSelections() {
    const selectedElements = document.querySelectorAll('.option1.selected');
    selectedOptions = Array.from(selectedElements).map(el => el.id);

    if (selectedOptions.length === 0) {
        ToastifyModule.error("Please select at least one option.");
        return;
    }

    workspaceData.features = selectedOptions;

    console.log('Selected Options:', selectedOptions);
    nextStep();
}





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



document.addEventListener('DOMContentLoaded', () => {
    openModal();
    updateProgress();
});