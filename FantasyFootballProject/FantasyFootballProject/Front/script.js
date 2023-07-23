const loginLink = document.querySelector('.login-link');
const signupLink = document.querySelector('.signup-link');
const loginForm = document.querySelectorAll('.form-container')[0];
const signupForm = document.querySelectorAll('.form-container')[1];

loginLink.addEventListener('click', function () {
    loginForm.classList.remove('hidden');
    signupForm.classList.add('hidden');
});

signupLink.addEventListener('click', function () {
    signupForm.classList.remove('hidden');
    loginForm.classList.add('hidden');
});