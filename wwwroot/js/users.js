const uri = '/User';
let users = [];

//const uri_user = '/User';
const authToken = localStorage.getItem('authToken');

getUsers();
    function getUsers() {
    
        fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
        })
            .then(response => response.json())
            .then(data => _displayUsers(data))
            .catch(error => console.error('Unable to get items.', error));
    }

    function addUser() {
        const addNameTextbox = document.getElementById('add-name');
        const addPasswordTextBox=document.getElementById('add-password');

        const user = {
            isAdmin: false,
            name: addNameTextbox.value.trim(),
            password:addPasswordTextBox.value.trim(),
            tasksList:null,
            id:0
        };
        

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
            body: JSON.stringify(user)
        })
            .then(response => response.json())
            .then(() => {
                getUsers();
                addNameTextbox.value = '';
                addPasswordTextBox.value='';
            })
            .catch(error => console.error('Unable to add item.', error));
    }

    function deleteUser(id) {
        fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers:{
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            }
        })
            .then(() => getUsers())
            .catch(error => console.error('Unable to delete item.', error));
    }

    function displayEditForm(id) {
        const item = tasks.find(item => item.id === id);

        document.getElementById('edit-name').value = item.name;
        document.getElementById('edit-id').value = item.id;
        document.getElementById('edit-isDone').checked = item.isDone;
        document.getElementById('editForm').style.display = 'block';
    }

    function updateUser() {
        const userId = document.getElementById('edit-id').value;
        const user = {
            id: parseInt(userId, 10),
            isAdmin: document.getElementById('edit-isAdmin').checked,
            name: document.getElementById('edit-name').value.trim(),
            password:document.getElementById('edit-isAdmin')
        };

        fetch(`${uri}/${userId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`

            },
            body: JSON.stringify(user)
        })
            .then(() => getUsers())
            .catch(error => console.error('Unable to update item.', error));

        closeInput();

        return false;
    }

    function closeInput() {
        document.getElementById('editForm').style.display = 'none';
    }

    function _displayCount(itemCount) {
        const name = (itemCount === 1) ? 'user' : 'user kinds';

        document.getElementById('counter').innerText = `${itemCount} ${name}`;
    }

    function _displayUsers(data) {
        const tBody = document.getElementById('users');
        tBody.innerHTML = '';

        _displayCount(data.length);

        const button = document.createElement('button');

        data.forEach(user => {
            let isAdminCheckbox = document.createElement('input');
            isAdminCheckbox.type = 'checkbox';
            isAdminCheckbox.disabled = true;
            isAdminCheckbox.checked = user.isAdmin;

            let editButton = button.cloneNode(false);
            editButton.innerText = 'Edit';
            editButton.setAttribute('onclick', `displayEditForm(${user.id})`);

            let deleteButton = button.cloneNode(false);
            deleteButton.innerText = 'Delete';
            deleteButton.setAttribute('onclick', `deleteUser(${user.id})`);

            let tr = tBody.insertRow();

            let td1 = tr.insertCell(0);
            td1.appendChild(isAdminCheckbox);

            let td2 = tr.insertCell(1);
            let textNode = document.createTextNode(user.name);
            td2.appendChild(textNode);

            let td3 = tr.insertCell(2);
            let textNode2 = document.createTextNode(user.password);
            td3.appendChild(textNode2);

            let td4 = tr.insertCell(3);
            td4.appendChild(editButton);

            let td5 = tr.insertCell(4);
            td5.appendChild(deleteButton);
        });

        users = data;
   

    //     fetch(uri_user, {
    //         method: 'GET',
    //         headers: {
    //             'Accept': 'application/json',
    //             'Content-Type': 'application/json'
    //         },
    //         body: JSON.stringify(item)
    //     })
    //         .then(response => response.json())
    //         .then(() => {
    //             getUsers();
    //         })
    //         .catch(error => console.error('Unable to access users.', error));

    //     function getUsers() {
    //         fetch(uri)
    //             .then(response => response.json())
    //             .then(data => displayUsersList(data))
    //             .catch(error => console.error('Unable to get items.', error));
    //     }


    // function displayUsersList(data) {
    //     const list = document.getElementById('users-list');
    //     data.forEach(user => {
    //         const li = document.createElement('li');
    //         const password = localStorage.getItem(u.id);
    //         if (user.password == password) {
    //             li.innerHTML = '/MyTask';
    //             list.appendChild(li);
    //         }
    //     });
    // }

    // if (user.isAdmim == true) {
    //     getUsers();
    // }
}

