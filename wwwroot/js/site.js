const uri = '/MyTask';
let tasks = [];

const uri_user = '/User';
const authToken = localStorage.getItem('authToken');

if (!authToken) {
    window.location.href = '/login.html';
}
else {

    function getItems() {
        fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
        })
            .then(response => response.json())
            .then(data => _displayItems(data))
            .catch(error => console.error('Unable to get items.', error));
    }
    
    function addItem() {
        
        const addNameTextbox = document.getElementById('add-name');
        const addIDUserTextbox=document.getElementById('add-user_Id');
        console.log(addIDUserTextbox.value);
        //const userId=getUser(addIDUserTextbox.value).id;
       //console.log( users.find(u=>u.user_Id==addIDUserTextbox));
        const item = {
            isDone: false,
            name: addNameTextbox.value.trim(),
            id:0,
            user_Id:0
        };

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
            body: JSON.stringify(item)
        })
            .then(response =>
                response.json())
            .then(() => {
                getItems();
                addNameTextbox.value = '';
            })
            .catch(error => console.error('Unable to add item.', error));
    }

    function deleteItem(id) {
        fetch(`${uri}/${id}`, {
            method: 'DELETE',
              headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
        })
            .then(() => getItems())
            .catch(error => console.error('Unable to delete item.', error));
    }

    function displayEditForm(id) {
        const item = tasks.find(item => item.id === id);
        document.getElementById('edit-name').value = item.name;
        document.getElementById('edit-id').value = item.id;
        document.getElementById('edit-user_Id').value=item.user_Id;
        document.getElementById('edit-isDone').checked = item.isDone;
        document.getElementById('editForm').style.display = 'block';
    }

    function updateItem() {
        const itemId = document.getElementById('edit-id').value;
        const task=tasks.filter(t=>t.user_Id==itemId);
        console.log(task);
        const item = {
            id: parseInt(itemId, 10),
            isDone: document.getElementById('edit-isDone').checked,
            name: document.getElementById('edit-name').value.trim(),
            user_Id:0
        };

        fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
            body: JSON.stringify(item)
        })
            .then(() => getItems())
            .catch(error => console.error('Unable to update item.', error));

         closeInput();

        return false;
    }

    function closeInput() {
        document.getElementById('editForm').style.display = 'none';
    }

    function _displayCount(itemCount) {
        const name = (itemCount === 1) ? 'task' : 'task kinds';

        document.getElementById('counter').innerText = `${itemCount} ${name}`;
    }

    function _displayItems(data) {
        const tBody = document.getElementById('tasks');
        tBody.innerHTML = '';

        _displayCount(data.length);

        const button = document.createElement('button');

        data.forEach(item => {
            let isDoneCheckbox = document.createElement('input');
            isDoneCheckbox.type = 'checkbox';
            isDoneCheckbox.disabled = true;
            isDoneCheckbox.checked = item.isDone;

            let editButton = button.cloneNode(false);
            editButton.innerText = 'Edit';
            editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

            let deleteButton = button.cloneNode(false);
            deleteButton.innerText = 'Delete';
            deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

            let tr = tBody.insertRow();

            let td1 = tr.insertCell(0);
            td1.appendChild(isDoneCheckbox);

            let td2 = tr.insertCell(1);
            let textNode = document.createTextNode(item.name);
            td2.appendChild(textNode);

            let td3 = tr.insertCell(2);
            td3.appendChild(editButton);

            let td4 = tr.insertCell(3);
            td4.appendChild(deleteButton);
        });

        tasks = data;
    }

    


getUser();

    function getUser(){
        const b=document.getElementById("toUsers");
   

        fetch(`${uri_user}/GetUser`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
    
        })
            .then(response => {
                if (response.status != 200) {
                    throw new Error('Failed to fetch data');
                }
                console.log('res'+response);
                return response.json();
            })
            .then(data =>{
                console.log('data',data);
                if(data.isAdmin){
                    b.style.display='inline';
                }
            })
            .catch(error => {
                console.error('Unable to get my user.', error);
            });
    }

    getItems();

    
}


