// Debug JavaScript for Register Form
console.log('=== REGISTER DEBUG JS LOADED ===');

document.addEventListener('DOMContentLoaded', function() {
    console.log('=== DOM CONTENT LOADED ===');
    
    // Find all form elements
    const form = document.querySelector('.register-form-inner');
    const submitBtn = document.querySelector('.btn-register');
    const checkbox = document.getElementById('agreeTerms');
    
    console.log('Form elements:', {
        form: !!form,
        submitBtn: !!submitBtn,
        checkbox: !!checkbox
    });
    
    if (!form || !submitBtn || !checkbox) {
        console.error('Some form elements not found!');
        return;
    }
    
    // List all inputs
    const allInputs = form.querySelectorAll('input, select, textarea');
    console.log(`Found ${allInputs.length} form inputs:`);
    allInputs.forEach((input, index) => {
        console.log(`${index}: ${input.type} - ${input.name} - value: "${input.value}"`);
    });
    
    // Simple validation function
    function checkAndEnableButton() {
        console.log('=== CHECKING FORM VALIDITY ===');
        
        // Required fields
        const requiredFields = [
            'FirstName', 'LastName', 'Username', 'Email', 
            'Password', 'ConfirmPassword', 'Phone', 'Gender', 'DateOfBirth'
        ];
        
        let allValid = true;
        requiredFields.forEach(fieldName => {
            const field = form.querySelector(`[name="${fieldName}"]`);
            if (!field) {
                console.log(`❌ Field ${fieldName} not found`);
                allValid = false;
                return;
            }
            
            const value = field.value.trim();
            if (!value) {
                console.log(`❌ Field ${fieldName} is empty`);
                allValid = false;
            } else {
                console.log(`✅ Field ${fieldName}: "${value}"`);
            }
        });
        
        // Check password match
        const password = form.querySelector('[name="Password"]');
        const confirmPassword = form.querySelector('[name="ConfirmPassword"]');
        if (password && confirmPassword) {
            if (password.value !== confirmPassword.value) {
                console.log('❌ Passwords do not match');
                allValid = false;
            } else if (password.value.length < 6) {
                console.log('❌ Password too short');
                allValid = false;
            } else {
                console.log('✅ Passwords match and valid');
            }
        }
        
        // Check checkbox
        if (!checkbox.checked) {
            console.log('❌ Terms checkbox not checked');
            allValid = false;
        } else {
            console.log('✅ Terms checkbox checked');
        }
        
        console.log(`=== FINAL RESULT: ${allValid ? 'VALID' : 'INVALID'} ===`);
        
        // Enable/disable button
        submitBtn.disabled = !allValid;
        if (allValid) {
            submitBtn.style.opacity = '1';
            submitBtn.style.background = 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)';
            console.log('✅ Button ENABLED');
        } else {
            submitBtn.style.opacity = '0.6';
            submitBtn.style.background = '#ccc';
            console.log('❌ Button DISABLED');
        }
    }
    
    // Add event listeners
    allInputs.forEach(input => {
        input.addEventListener('input', () => {
            console.log(`Input changed: ${input.name} = "${input.value}"`);
            setTimeout(checkAndEnableButton, 100);
        });
        
        input.addEventListener('change', () => {
            console.log(`Change event: ${input.name} = "${input.value}"`);
            setTimeout(checkAndEnableButton, 100);
        });
    });
    
    checkbox.addEventListener('change', () => {
        console.log(`Checkbox changed: ${checkbox.checked}`);
        checkAndEnableButton();
    });
    
    // Initial check
    setTimeout(() => {
        console.log('=== INITIAL CHECK ===');
        checkAndEnableButton();
    }, 500);
});
