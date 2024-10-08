# Ink Goose

**Yet another note taking app**

> WIP
> 
> Project is in early development and not intended for regular use.

## Backend

- Asp .NET Core Web API
- PostgreSQL

## Frontend

### Web

- Vue.js
- Tailwind CSS + DaisyUI

## Build and run

### Create `.env` file at repository root and adjust environment variables to your configuration.

**Example:**

```bash
POSTGRES_USER="postgres"
POSTGRES_PASSWORD="password"
POSTGRES_HOST="inkgoose-db" # do not change
POSTGRES_PORT="5432"
POSTGRES_DB="inkgoose"
API_PORT="3033"
WEB_PORT="3034"
VITE_API_HOST="http://localhost:$API_PORT"
```

### Build and run containers

```bash
docker compose build
docker compose up -d
```

## Contributing

Please, see [Contributing](https://github.com/obsqrbtz/InkGoose/blob/master/CONTRIBUTING.md)
