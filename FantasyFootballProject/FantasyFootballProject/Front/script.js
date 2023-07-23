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

var login = document.getElementById('Login');
login.addEventListener('click', function (e) {
    e.preventDefault();
    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;
    console.log(username);
    console.log(password);
    var url = `http://localhost:7096/user/login/?password=${password}&username=${username}`;
    fetch(url).then((response) => response.json())
        .then((data) => {
            console.log(data);
            alert(data.error);

        })
});