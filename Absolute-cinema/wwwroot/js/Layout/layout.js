// Enhanced JavaScript with Tailwind
document.addEventListener('DOMContentLoaded', function () {

    // Mobile menu toggle with Tailwind animations
    const mobileMenuBtn = document.getElementById('mobile-menu-btn');
    const mobileMenu = document.getElementById('mobile-menu');
    const mobileMenuIcon = document.getElementById('mobile-menu-icon');

    if (mobileMenuBtn && mobileMenu) {
        mobileMenuBtn.addEventListener('click', () => {
            mobileMenu.classList.toggle('hidden');

            // Animate hamburger icon with Tailwind classes
            if (mobileMenu.classList.contains('hidden')) {
                mobileMenuIcon.className = 'fas fa-bars text-xl transition-transform duration-300';
            } else {
                mobileMenuIcon.className = 'fas fa-times text-xl transition-transform duration-300 rotate-90';
            }
        });
    }

    // Enhanced navbar scroll effect with Tailwind
    window.addEventListener('scroll', () => {
        const navbar = document.getElementById('navbar');
        if (navbar) {
            if (window.scrollY > 100) {
                navbar.classList.add('backdrop-blur-md', 'bg-black/90', 'shadow-2xl');
            } else {
                navbar.classList.remove('backdrop-blur-md', 'bg-black/90', 'shadow-2xl');
            }
        }
    });

    // Add smooth scroll behavior
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Add loading animation for search with Tailwind
    document.querySelectorAll('form').forEach(form => {
        form.addEventListener('submit', function () {
            const submitBtn = this.querySelector('button[type="submit"]');
            if (submitBtn) {
                const icon = submitBtn.querySelector('i');
                icon.className = 'fas fa-spinner animate-spin';
            }
        });
    });

    // Add intersection observer for animations
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-fade-in');
            }
        });
    }, observerOptions);

    // Observe footer elements
    document.querySelectorAll('footer div').forEach(el => {
        observer.observe(el);
    });

    // Add ripple effect to buttons
    document.querySelectorAll('.login-btn, .btn-primary-custom').forEach(button => {
        button.addEventListener('click', function (e) {
            const ripple = document.createElement('span');
            const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);
            const x = e.clientX - rect.left - size / 2;
            const y = e.clientY - rect.top - size / 2;

            ripple.style.width = ripple.style.height = size + 'px';
            ripple.style.left = x + 'px';
            ripple.style.top = y + 'px';
            ripple.style.position = 'absolute';
            ripple.style.borderRadius = '50%';
            ripple.style.background = 'rgba(255, 255, 255, 0.3)';
            ripple.style.transform = 'scale(0)';
            ripple.style.animation = 'ripple 0.6s linear';
            ripple.style.pointerEvents = 'none';

            this.appendChild(ripple);

            setTimeout(() => {
                ripple.remove();
            }, 600);
        });
    });

    // Add hover sound effects (optional)
    const addHoverSounds = () => {
        document.querySelectorAll('.nav-link-hover, .dropdown-item, .footer-link').forEach(element => {
            element.addEventListener('mouseenter', () => {
                // You can add sound effects here if needed
                // new Audio('/sounds/hover.mp3').play();
            });
        });
    };

    // Initialize hover sounds
    // addHoverSounds();

    // Add keyboard navigation support
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            // Close mobile menu on Escape
            if (mobileMenu && !mobileMenu.classList.contains('hidden')) {
                mobileMenu.classList.add('hidden');
                mobileMenuIcon.className = 'fas fa-bars text-xl transition-transform duration-300';
            }
        }
    });

    // Add focus management for accessibility
    const focusableElements = 'button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])';

    document.querySelectorAll('.dropdown-menu, .user-menu').forEach(dropdown => {
        const focusableContent = dropdown.querySelectorAll(focusableElements);
        const firstFocusableElement = focusableContent[0];
        const lastFocusableElement = focusableContent[focusableContent.length - 1];

        dropdown.addEventListener('keydown', (e) => {
            if (e.key === 'Tab') {
                if (e.shiftKey) {
                    if (document.activeElement === firstFocusableElement) {
                        lastFocusableElement.focus();
                        e.preventDefault();
                    }
                } else {
                    if (document.activeElement === lastFocusableElement) {
                        firstFocusableElement.focus();
                        e.preventDefault();
                    }
                }
            }
        });
    });

    // Add loading state management
    const showLoading = (element) => {
        element.classList.add('opacity-50', 'pointer-events-none');
        const spinner = element.querySelector('.loading-spinner');
        if (spinner) {
            spinner.classList.remove('hidden');
        }
    };

    const hideLoading = (element) => {
        element.classList.remove('opacity-50', 'pointer-events-none');
        const spinner = element.querySelector('.loading-spinner');
        if (spinner) {
            spinner.classList.add('hidden');
        }
    };

    // Add performance monitoring
    const performanceObserver = new PerformanceObserver((list) => {
        for (const entry of list.getEntries()) {
            if (entry.entryType === 'navigation') {
                console.log('Page load time:', entry.loadEventEnd - entry.loadEventStart);
            }
        }
    });

    performanceObserver.observe({ entryTypes: ['navigation'] });
});

// Add CSS animation keyframes for ripple effect
const style = document.createElement('style');
style.textContent = `
    @keyframes ripple {
        to {
            transform: scale(4);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);