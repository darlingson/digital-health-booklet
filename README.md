# Digital Health Booklet

Mastering ASP.Net API's. Old project made new.

## Project Overview

In Malawi, small pocket books are used for health information. Every time you go to the hospital, you are supposed to either carry this book or be forced to buy one. This book allows the health professional to record diagnosis, recommended treatment, and current status (weight, etc.). This project aims to digitize this booklet.

### Technologies Used

- **Backend**: ASP.NET Web API
- **Frontend**: React, Tailwind CSS, TypeScript
- **Other**: Docker

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/darlingson/digital-health-booklet.git
   cd digital-health-booklet
   ```

2. Install backend dependencies:
   ```bash
   cd backend
   dotnet restore
   ```

3. Install frontend dependencies:
   ```bash
   cd frontend
   npm install
   ```

4. Run the application:
   ```bash
   # For backend
   dotnet run
   
   # For frontend
   npm start
   ```

## Usage

1. Access the frontend application at `http://localhost:3000`.
2. Use the provided interface to record health information and view records.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Open a pull request.

## License

This project is licensed under the MIT License.

## Contributors

- [darlingson](https://github.com/darlingson)
