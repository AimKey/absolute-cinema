// Simple Register Page JavaScript
console.log('Register JS loading...');

document.addEventListener('DOMContentLoaded', function() {
    console.log('DOM loaded, initializing register form...');
    
    const submitBtn = document.querySelector('.btn-register');
    const agreeTermsCheckbox = document.getElementById('agreeTerms');
    const form = document.querySelector('.register-form-inner');
    
    console.log('Elements found:', {
        submitBtn: !!submitBtn,
        checkbox: !!agreeTermsCheckbox,
        form: !!form
    });

    // Function to check if form is valid
    function checkFormValidity() {
        console.log('Checking form validity...');
        
        // Get all required fields (exclude Address as it's optional)
        const requiredInputs = [
            document.querySelector('input[name="FirstName"]'),
            document.querySelector('input[name="LastName"]'),
            document.querySelector('input[name="Username"]'),
            document.querySelector('input[name="Email"]'),
            document.querySelector('input[name="Password"]'),
            document.querySelector('input[name="ConfirmPassword"]'),
            document.querySelector('input[name="Phone"]'),
            document.querySelector('select[name="Gender"]'),
            document.querySelector('input[name="DateOfBirth"]')
        ];
        
        // Check if all required fields are filled
        let allFieldsFilled = true;
        const fieldValidation = {};
        
        requiredInputs.forEach((input, index) => {
            const fieldName = input ? input.name : `field_${index}`;
            if (!input) {
                allFieldsFilled = false;
                fieldValidation[fieldName] = 'not found';
                console.log(`Required field ${fieldName} not found`);
            } else if (!input.value || !input.value.trim()) {
                allFieldsFilled = false;
                fieldValidation[fieldName] = 'empty';
                console.log(`Required field ${fieldName} is empty`);
            } else {
                fieldValidation[fieldName] = 'filled';
                console.log(`Required field ${fieldName}: "${input.value}"`);
            }
        });
        
        // Check passwords match
        const password = document.querySelector('input[name="Password"]');
        const confirmPassword = document.querySelector('input[name="ConfirmPassword"]');
        const passwordsMatch = password && confirmPassword && 
            password.value === confirmPassword.value && 
            password.value.length >= 6;
        
        // Check terms agreed
        const termsAgreed = agreeTermsCheckbox && agreeTermsCheckbox.checked;
        
        const isValid = allFieldsFilled && passwordsMatch && termsAgreed;
        
        console.log('Validation result:', {
            allFieldsFilled,
            passwordsMatch,
            termsAgreed,
            isValid,
            fieldValidation
        });
        
        // Always keep button enabled, but provide visual feedback
        if (submitBtn) {
            submitBtn.disabled = false; // Always enabled
            
            if (isValid) {
                submitBtn.style.opacity = '1';
                submitBtn.style.cursor = 'pointer';
                submitBtn.style.background = 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)';
                submitBtn.classList.remove('partial-filled');
                console.log('✅ Form is complete and ready');
            } else {
                // Still enable but with different visual state
                submitBtn.style.opacity = '1';
                submitBtn.style.cursor = 'pointer';
                submitBtn.style.background = 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)';
                submitBtn.classList.add('partial-filled');
                console.log('⚠️ Form incomplete but button still enabled - Missing:', Object.keys(fieldValidation).filter(k => fieldValidation[k] !== 'filled'));
            }
        } else {
            console.error('Submit button not found!');
        }
    }
    
    // Add event listeners to all form inputs
    const allInputs = document.querySelectorAll('input, select, textarea');
    console.log('Found inputs:', allInputs.length);
    
    allInputs.forEach(input => {
        input.addEventListener('input', function() {
            console.log('Input changed:', this.name, this.value);
            setTimeout(checkFormValidity, 100);
        });
        
        input.addEventListener('change', function() {
            console.log('Change event:', this.name, this.value);
            setTimeout(checkFormValidity, 100);
        });
    });
    
    // Special handler for checkbox
    if (agreeTermsCheckbox) {
        agreeTermsCheckbox.addEventListener('change', function() {
            console.log('Checkbox changed:', this.checked);
            checkFormValidity();
        });
    }
    
    // Phone number formatting
    const phoneInput = document.querySelector('input[name="Phone"]');
    if (phoneInput) {
        phoneInput.addEventListener('input', function(e) {
            // Only allow numbers
            let value = e.target.value.replace(/\D/g, '');
            if (value.length > 11) {
                value = value.substring(0, 11);
            }
            e.target.value = value;
        });
    }
    
    // Password confirmation validation
    const passwordInput = document.querySelector('input[name="Password"]');
    const confirmPasswordInput = document.querySelector('input[name="ConfirmPassword"]');
    
    function validatePasswordMatch() {
        if (passwordInput && confirmPasswordInput) {
            const errorSpan = confirmPasswordInput.parentNode.querySelector('.validation-message');
            
            if (confirmPasswordInput.value && passwordInput.value !== confirmPasswordInput.value) {
                confirmPasswordInput.classList.add('is-invalid');
                if (errorSpan) errorSpan.textContent = 'Mật khẩu xác nhận không khớp';
            } else {
                confirmPasswordInput.classList.remove('is-invalid');
                if (errorSpan) errorSpan.textContent = '';
            }
        }
    }
    
    if (passwordInput) {
        passwordInput.addEventListener('input', validatePasswordMatch);
    }
    if (confirmPasswordInput) {
        confirmPasswordInput.addEventListener('input', validatePasswordMatch);
    }
    
    // Form submission - add client-side validation
    if (form) {
        form.addEventListener('submit', function(e) {
            console.log('Form submit attempted - validating...');
            
            // Get all required fields
            const requiredFields = [
                { name: 'FirstName', label: 'Họ' },
                { name: 'LastName', label: 'Tên' },
                { name: 'Username', label: 'Tên đăng nhập' },
                { name: 'Email', label: 'Email' },
                { name: 'Password', label: 'Mật khẩu' },
                { name: 'ConfirmPassword', label: 'Xác nhận mật khẩu' },
                { name: 'Phone', label: 'Số điện thoại' },
                { name: 'Gender', label: 'Giới tính' },
                { name: 'DateOfBirth', label: 'Ngày sinh' }
            ];
            
            let hasErrors = false;
            
            // Clear previous error messages
            document.querySelectorAll('.validation-message').forEach(span => {
                span.textContent = '';
            });
            document.querySelectorAll('.form-control').forEach(input => {
                input.classList.remove('is-invalid');
            });
            
            // Validate each required field
            requiredFields.forEach(field => {
                const input = document.querySelector(`[name="${field.name}"]`);
                const errorSpan = input?.parentNode.querySelector('.validation-message');
                
                if (!input || !input.value.trim()) {
                    hasErrors = true;
                    if (input) {
                        input.classList.add('is-invalid');
                        input.focus();
                    }
                    if (errorSpan) {
                        errorSpan.textContent = `${field.label} là bắt buộc`;
                        errorSpan.style.color = '#dc3545';
                        errorSpan.style.fontSize = '0.875em';
                    }
                    console.log(`❌ ${field.label} is required`);
                }
            });
            
            // Check password match
            const password = document.querySelector('input[name="Password"]');
            const confirmPassword = document.querySelector('input[name="ConfirmPassword"]');
            if (password && confirmPassword) {
                if (password.value && confirmPassword.value && password.value !== confirmPassword.value) {
                    hasErrors = true;
                    confirmPassword.classList.add('is-invalid');
                    const errorSpan = confirmPassword.parentNode.querySelector('.validation-message');
                    if (errorSpan) {
                        errorSpan.textContent = 'Mật khẩu xác nhận không khớp';
                        errorSpan.style.color = '#dc3545';
                    }
                    console.log('❌ Passwords do not match');
                } else if (password.value && password.value.length < 6) {
                    hasErrors = true;
                    password.classList.add('is-invalid');
                    const errorSpan = password.parentNode.querySelector('.validation-message');
                    if (errorSpan) {
                        errorSpan.textContent = 'Mật khẩu phải có ít nhất 6 ký tự';
                        errorSpan.style.color = '#dc3545';
                    }
                    console.log('❌ Password too short');
                }
            }
            
            // Check email format
            const email = document.querySelector('input[name="Email"]');
            if (email && email.value.trim()) {
                const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (!emailRegex.test(email.value.trim())) {
                    hasErrors = true;
                    email.classList.add('is-invalid');
                    const errorSpan = email.parentNode.querySelector('.validation-message');
                    if (errorSpan) {
                        errorSpan.textContent = 'Email không hợp lệ';
                        errorSpan.style.color = '#dc3545';
                    }
                    console.log('❌ Invalid email format');
                }
            }
            
            // Check terms agreement
            const agreeTerms = document.getElementById('agreeTerms');
            if (!agreeTerms || !agreeTerms.checked) {
                hasErrors = true;
                if (agreeTerms) {
                    agreeTerms.parentNode.style.border = '1px solid #dc3545';
                    agreeTerms.parentNode.style.borderRadius = '4px';
                    agreeTerms.parentNode.style.padding = '5px';
                }
                
                // Show error message near checkbox
                let checkboxError = document.querySelector('.checkbox-error');
                if (!checkboxError) {
                    checkboxError = document.createElement('span');
                    checkboxError.className = 'checkbox-error';
                    checkboxError.style.color = '#dc3545';
                    checkboxError.style.fontSize = '0.875em';
                    checkboxError.style.display = 'block';
                    checkboxError.style.marginTop = '5px';
                    agreeTerms.parentNode.parentNode.appendChild(checkboxError);
                }
                checkboxError.textContent = 'Bạn phải đồng ý với điều khoản sử dụng';
                console.log('❌ Terms not agreed');
            } else {
                // Remove error styling from checkbox
                if (agreeTerms) {
                    agreeTerms.parentNode.style.border = '';
                    agreeTerms.parentNode.style.borderRadius = '';
                    agreeTerms.parentNode.style.padding = '';
                }
                const checkboxError = document.querySelector('.checkbox-error');
                if (checkboxError) {
                    checkboxError.remove();
                }
            }
            
            // If there are errors, prevent submission
            if (hasErrors) {
                e.preventDefault();
                console.log('❌ Form submission prevented due to validation errors');
                
                // Focus on first error field
                const firstError = document.querySelector('.is-invalid');
                if (firstError) {
                    firstError.focus();
                    firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
                }
                
                return false;
            }
            
            // If validation passes, submit form normally
            console.log('✅ Validation passed, submitting form...');
        });
    }
    
    // Initial check after page load
    setTimeout(function() {
        console.log('Initial form check...');
        checkFormValidity();
    }, 500);
});

// Toggle password visibility function
function togglePassword(fieldName) {
    const field = document.querySelector(`input[name="${fieldName}"]`);
    const button = field.nextElementSibling;
    const icon = button.querySelector('i');
    
    if (field.type === 'password') {
        field.type = 'text';
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    } else {
        field.type = 'password';
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    }
}
