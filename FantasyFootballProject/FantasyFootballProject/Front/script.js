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
    var url = `http://localhost:7171/user/login/?password=${password}&username=${username}`;
    fetch(url).then((response) => response.json())
        .then((data) => {
            console.log(data);
            alert(data.error);

        })
});

var signupForms = document.getElementById('Sign Up');
signupForms.addEventListener('click', function (e) {
    e.preventDefault();

    var name = document.getElementById('name').value;
    var family = document.getElementById('family').value;
    var email = document.getElementById('email').value;
    var username = document.getElementById('username1').value;
    var password = document.getElementById('password1').value;

    console.log(name);
    console.log(family);
    console.log(email);
    console.log(username);
    console.log(password);

    // Check if any of the fields are empty
    if (name.trim() === '' || family.trim() === '' || email.trim() === '' || username.trim() === '' || password.trim() === '') {
        alert('Please fill in all fields');
        return;
    }


    // If the input is valid, send the data to the server
    var url = `http://localhost:7171/user/signup`;

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name: name,
            family: family,
            email: email,
            username: username,
            password: password
        })
    })
        .then((response) => response.text())
        .then((data) => {
            console.log(data);
            if (data.error) {
                alert(data.error);
            } else {
                alert(data);
                if (data.includes("Member successful added") == true) {
                    window.location.href = 'b.html';
                }

            }
        })
        .catch((error) => {
            console.log(error);
            alert('An error occurred. Please try again later.');
        });
});