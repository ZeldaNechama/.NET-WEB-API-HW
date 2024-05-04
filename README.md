# .NET-WEB-API-HW
# Task Management System

This project is a  Task Management System built using ASP.NET Core for the backend and HTML, CSS, and JavaScript for the frontend.

## Features

- **User Authentication**: Users can authenticate themselves using a username and password. Authentication is handled using JSON Web Tokens (JWT).
- **Task Management**: Authenticated users can view, add, edit, and delete tasks.
- **Role-based Authorization**: Different levels of access are provided based on user roles (e.g., admin, regular user).

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine
- Code editor such as Visual Studio Code or Visual Studio

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/ZeldaNechama/.NET-WEB-API-HW.git
    ```

2. Navigate to the project directory:

    ```bash
    cd .NET-WEB-API-HW
    ```

3. Restore dependencies:

    ```bash
    dotnet restore
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

5. Open your web browser and navigate to `https://localhost:7138` to access the application.

## Usage

- **Login**: Navigate to the login page (`/login.html`) and enter your username and password to authenticate.
- **Task Management**: Once authenticated, you can access the task management features on the main page (`/index.html`). Here, you can view, add, edit, and delete tasks.

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests for any bugs or feature requests you may have.

## License

This project is licensed under the [MIT License](LICENSE).


