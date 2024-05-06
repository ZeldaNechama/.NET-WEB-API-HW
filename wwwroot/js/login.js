// // const uri = '/MyTask/Login';
// const uri = '/Login';
// const uri_user = '/User';

// function findUser() {
//     console.log('in find user');
//     const password = document.getElementById('password').value.trim();
//     const name = document.getElementById('name').value.trim();
//     if (password != "zndbvkwk@#vhvbj" || name != "zn") {
//         const user = { name: name, password: password, isAdmin: false, id: 0 ,tasksList:null};
//         fetch(uri, {
//             method: 'POST',
//             headers: {
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json'
//             },
//             body: JSON.stringify(user)
//         })
//             .then(response => {
//                 console.log('in res of find user');
//                 if (!response.ok) {
//                     throw new Error('Authentication failed');
//                 }
//                 return response.json();
//             })
//             .then(data => {
//                 localStorage.setItem('authToken', data.token);
//                 redirectToTaskCRUDPage();
//             })
//             .catch(error => {
//                 console.error('Unable to authenticate user.', error);
//                 document.getElementById('error-message').innerText = 'Authentication failed. Please try again.';
//             });
//     }
//     else {
//         //tasksList:null
//         const user = { name: name, password: password, isAdmin: true, id: 0 ,tasksList:null};
//         fetch(uri, {
//             method: 'POST',
//             headers: {
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json'
//             },
//             body: JSON.stringify(user)
//         })
//             .then(response => {
//                 if (!response.ok) {
//                     throw new Error('Authentication failed');
//                 }
//                 return response.json();
//             })
//             .then(data => {
//                 localStorage.setItem('authToken', data.token);
//                 redirectToTaskCRUDPage();
//             })
//             .catch(error => {
//                 console.error('Unable to authenticate user.', error);
//                 document.getElementById('error-message').innerText = 'Authentication failed. Please try again.';
//             });
//     }
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

function findUser() {
    const password = document.getElementById('password').value.trim();
    const name = document.getElementById('name').value.trim();
    const user = { name: name, password: password, isAdmin: false, id: 0 };

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
