// Cinema Auth JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Initialize auth forms
    initializeAuthForms();
    initializeGenderSelection();
    initializeFormValidation();
    initializePasswordToggle();
});

function initializeAuthForms() {
    const authForms = document.querySelectorAll('.auth-form');
    
    authForms.forEach(form => {
        form.addEventListener('submit', function(e) {
            e.preventDefault();
            handleFormSubmit(this);
        });
    });
}

function initializeGenderSelection() {
    const genderOptions = document.querySelectorAll('.gender-option');
    
    genderOptions.forEach(option => {
        option.addEventListener('click', function() {
            // Remove selected class from all options
            genderOptions.forEach(opt => opt.classList.remove('selected'));
            
            // Add selected class to clicked option
            this.classList.add('selected');
            
            // Check the radio button
            const radio = this.querySelector('input[type="radio"]');
            if (radio) {
                radio.checked = true;
            }
        });
    });
}

function initializeFormValidation() {
    const inputs = document.querySelectorAll('.form-control');
    
    inputs.forEach(input => {
        input.addEventListener('blur', function() {
            validateField(this);
        });
        
        input.addEventListener('input', function() {
            clearFieldError(this);
        });
    });
}

function initializePasswordToggle() {
    // Add password toggle buttons
    const passwordFields = document.querySelectorAll('input[type="password"]');
    
    passwordFields.forEach(field => {
        const wrapper = document.createElement('div');
        wrapper.className = 'password-wrapper';
        wrapper.style.position = 'relative';
        
        field.parentNode.insertBefore(wrapper, field);
        wrapper.appendChild(field);
        
        const toggleBtn = document.createElement('button');
        toggleBtn.type = 'button';
        toggleBtn.className = 'password-toggle';
        toggleBtn.innerHTML = '👁️';
        toggleBtn.style.cssText = `
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            background: none;
            border: none;
            cursor: pointer;
            font-size: 1.2rem;
            opacity: 0.6;
            transition: opacity 0.3s ease;
        `;
        
        toggleBtn.addEventListener('click', function() {
            if (field.type === 'password') {
                field.type = 'text';
                this.innerHTML = '🙈';
            } else {
                field.type = 'password';
                this.innerHTML = '👁️';
            }
        });
        
        toggleBtn.addEventListener('mouseenter', function() {
            this.style.opacity = '1';
        });
        
        toggleBtn.addEventListener('mouseleave', function() {
            this.style.opacity = '0.6';
        });
        
        wrapper.appendChild(toggleBtn);
    });
}

function handleFormSubmit(form) {
    const submitBtn = form.querySelector('.btn-cinema');
    const formData = new FormData(form);
    
    // Add loading state
    submitBtn.classList.add('loading');
    submitBtn.disabled = true;
    
    // Validate form
    if (!validateForm(form)) {
        submitBtn.classList.remove('loading');
        submitBtn.disabled = false;
        return;
    }
    
    // Simulate form submission (replace with actual AJAX call)
    setTimeout(() => {
        submitBtn.classList.remove('loading');
        submitBtn.disabled = false;
        
        // Show success message
        showMessage('success', 'Form submitted successfully!');
        
        // Here you would normally send the data to your backend
        console.log('Form data:', Object.fromEntries(formData));
    }, 2000);
}

function validateForm(form) {
    let isValid = true;
    const requiredFields = form.querySelectorAll('[required]');
    
    requiredFields.forEach(field => {
        if (!validateField(field)) {
            isValid = false;
        }
    });
    
    // Password confirmation validation
    const password = form.querySelector('input[name="Password"]');
    const confirmPassword = form.querySelector('input[name="ConfirmPassword"]');
    
    if (password && confirmPassword && password.value !== confirmPassword.value) {
        showFieldError(confirmPassword, 'Passwords do not match');
        isValid = false;
    }
    
    return isValid;
}

function validateField(field) {
    const value = field.value.trim();
    const fieldName = field.name;
    
    // Clear previous errors
    clearFieldError(field);
    
    // Required validation
    if (field.hasAttribute('required') && !value) {
        showFieldError(field, `${getFieldLabel(field)} is required`);
        return false;
    }
    
    // Email validation
    if (field.type === 'email' && value) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(value)) {
            showFieldError(field, 'Please enter a valid email address');
            return false;
        }
    }
    
    // Phone validation
    if (field.type === 'tel' && value) {
        const phoneRegex = /^[\d\s\-\+\(\)]+$/;
        if (!phoneRegex.test(value)) {
            showFieldError(field, 'Please enter a valid phone number');
            return false;
        }
    }
    
    // Password validation
    if (field.type === 'password' && fieldName === 'Password' && value) {
        if (value.length < 6) {
            showFieldError(field, 'Password must be at least 6 characters long');
            return false;
        }
    }
    
    return true;
}

function showFieldError(field, message) {
    field.classList.add('is-invalid');
    
    let feedback = field.parentNode.querySelector('.invalid-feedback');
    if (!feedback) {
        feedback = document.createElement('div');
        feedback.className = 'invalid-feedback';
        field.parentNode.appendChild(feedback);
    }
    
    feedback.textContent = message;
}

function clearFieldError(field) {
    field.classList.remove('is-invalid');
    const feedback = field.parentNode.querySelector('.invalid-feedback');
    if (feedback) {
        feedback.remove();
    }
}

function getFieldLabel(field) {
    const label = field.parentNode.querySelector('.form-label');
    return label ? label.textContent.replace('*', '').trim() : field.name;
}

function showMessage(type, message) {
    // Remove existing messages
    const existingMessages = document.querySelectorAll('.alert-success, .alert-error');
    existingMessages.forEach(msg => msg.remove());
    
    // Create new message
    const messageDiv = document.createElement('div');
    messageDiv.className = type === 'success' ? 'alert-success' : 'alert-error';
    messageDiv.textContent = message;
    
    // Insert message at the top of the form
    const form = document.querySelector('.auth-form');
    form.insertBefore(messageDiv, form.firstChild);
    
    // Auto-remove message after 5 seconds
    setTimeout(() => {
        messageDiv.remove();
    }, 5000);
}

// Date picker enhancement
function initializeDatePicker() {
    const dateInputs = document.querySelectorAll('input[type="date"]');
    
    dateInputs.forEach(input => {
        // Set max date to today (18+ years for cinema)
        const today = new Date();
        const maxDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
        input.max = maxDate.toISOString().split('T')[0];
        
        // Set min date to reasonable range
        const minDate = new Date(today.getFullYear() - 100, today.getMonth(), today.getDate());
        input.min = minDate.toISOString().split('T')[0];
    });
}

// Call date picker initialization
initializeDatePicker();

// Form animations
function animateFormElements() {
    const formElements = document.querySelectorAll('.form-group');
    
    formElements.forEach((element, index) => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(20px)';
        element.style.animation = `slideUp 0.6s ease forwards ${index * 0.1}s`;
    });
}

// Add CSS animation for form elements
const style = document.createElement('style');
style.textContent = `
    @keyframes slideUp {
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
`;
document.head.appendChild(style);

// Initialize animations
animateFormElements();
