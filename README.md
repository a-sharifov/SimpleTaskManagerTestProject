

# Simple Task Manager Test Project

## how to run
1. Clone the repository
1. Navigate to the project directory
1. Run the following command to execute the tests:
   ```bash
   dotnet test
   ```
1. Ensure the Docker containers are running:
1.   ```bash
   docker compose -f docker-compose.test.yml up -d --build
   ```