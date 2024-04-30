const uri = '/MyTask/Login';
let tasks = [];

function getInfo() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function findItem() {
    const findUser = document.getElementById('login');

    const user = {
        password: "",
        name: addNameTextbox.value.trim()
    };
    //localStorage.setItem(user.name,user.password);
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Authentication failed'); // Handle authentication failure
            }
            return response.json();
        })
        .then(data => {
            // Assuming the server responds with an authentication token
            localStorage.setItem('authToken'+user.name, data.token); // Save the token in localStorage
            getInfo();
        })
        .catch(error => console.error('Unable to authenticate user.', error));
}
