// const uri = '/MyTask/Login';

// function getInfo() {
//     fetch(uri)
//         .then(response => response.json())
//         .then(data =>(console.log('in get info')))
//         .catch(error => console.error('Unable to get items.', error));
// }

// // function findItem() {
// //     const findUser = document.getElementById('login');

// //     const user = {
// //         password: "",
// //         name: addNameTextbox.value.trim()
// //     };
// //     //localStorage.setItem(user.name,user.password);
// //     fetch(uri, {
// //         method: 'POST',
// //         headers: {
// //             'Accept': 'application/json',
// //             'Content-Type': 'application/json'
// //         },
// //         body: JSON.stringify(user)
// //     })
// //         .then(response => {
// //             if (!response.ok) {
// //                 throw new Error('Authentication failed'); // Handle authentication failure
// //             }
// //             return response.json();
// //         })
// //         .then(data => {
// //             // Assuming the server responds with an authentication token
// //             localStorage.setItem('authToken'+user.name, data.token); // Save the token in localStorage
// //             getInfo();
// //         })
// //         .catch(error => console.error('Unable to authenticate user.', error));
// // }


// function findUser() {
//     console.log("in find user");
//     const user = {
//         password: "",
//         //name: addNameTextbox.value.trim()
//     };

//     fetch(uri, {
//         method: 'POST',
//         headers: {
//             'Accept': 'application/json',
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(user)
//     })
//         .then(response => {
//             if (!response.ok) {
//                 throw new Error('Authentication failed');
//             }
//             return response.json();
//         })
//         .then(data => {
//             localStorage.setItem('authToken', data); 
//             redirectToTaskCRUDPage();
//             window.location.href = '/index.html';
           
//         })
//         .catch(error => console.error('Unable to authenticate user.', error));
// }
// function redirectToTaskCRUDPage() {
//     const authToken = localStorage.getItem('authToken');
//     if (authToken) {
//         window.location.href = '/index.html'; 
//     } else {
//         console.error('Authentication token not found.');
//     }
// }

const uri = '/Login';
const uri_user='/User';

function findUser() {
    const password = document.getElementById('password').value.trim();
    const name=document.getElementById('name').value.trim();
    const user = { name: name, password: password }; //example

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
            throw new Error('Authentication failed');
        }
        return response.json();
    })
    .then(data => {
        localStorage.setItem('authToken', data.token);
        redirectToTaskCRUDPage();
    })
    .catch(error => {
        console.error('Unable to authenticate user.', error);
        // Handle authentication failure
        // For example, display an error message to the user
         document.getElementById('error-message').innerText = 'Authentication failed. Please try again.';
    });
}

function redirectToTaskCRUDPage() {
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
        window.location.href = '/index.html'; 
    } else {
        console.error('Authentication token not found.');
    }
}
