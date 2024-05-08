const uri = '/Login';

function findUser() {
    const password = document.getElementById('password').value.trim();
    const name = document.getElementById('name').value.trim();
    const user = { name: name, password: password, isAdmin: false, id: 0 };
    console.log('in find user',user);
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
    .then(response => {
        console.log('in 2nd find user');
        if (!response.ok) {
            throw new Error('Authentication failed');
        }
        return response.json();
    })
    .then(data => {
        console.log('data of all',data);
        localStorage.setItem('authToken', data);
        redirectToTaskCRUDPage();
    })
    .catch(error => {
        console.error('Unable to authenticate user.', error);
        document.getElementById('error-message').innerText = 'Authentication failed. Please try again.';
    });
}

function redirectToTaskCRUDPage() {
    console.log('in func to nav tasks-crud ');
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
        window.location.href = '/index.html';
    } else {
        console.error('Authentication token not found.');
    }
}

function handleCredentialResponse(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userName = decodedToken.name;
        var userPassword = decodedToken.sub;
        login(userName, userPassword);
    } else {
        alert('Google Sign-In was cancelled.');
    }
}


//Parses JWT token from Google Sign-In
function parseJwt(authToken) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}