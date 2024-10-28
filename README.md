# Ink Goose

**Yet another note taking app**

> [WIP]  
> Project is in early development and not intended for regular use.

## Backend - .NET 9

- Asp .NET Core Web API
- Entity Framework Core
- PostgreSQL

## Frontend

### Web - Node.js

- Vue.js
- Tailwind CSS + DaisyUI

## [Preview](https://inkgoose.obsqrbtz.space)

App is accessible at [https://inkgoose.obsqrbtz.space](https://inkgoose.obsqrbtz.space). 

At the moment only raw markdown input is supported ([CommonMark spec](https://spec.commonmark.org)).

> [!IMPORTANT]  
> Notes are not encrypted and stored as plain markdown server-side.

## Self-host

### Create `.env` file at repository root and adjust environment variables to your configuration

**Example:**

```bash
POSTGRES_USER="postgres"
POSTGRES_PASSWORD="password"
POSTGRES_HOST="inkgoose-db" # do not change
POSTGRES_PORT="5432"
POSTGRES_DB="inkgoose"
API_PORT="3033"
WEB_PORT="3034"
VITE_API_HOST="<api_url>:$API_PORT"
```

`<api_url>` is the address to access `inkgoose-api` container from outside.

### Build and run containers

```bash
docker compose build
docker compose up -d
```

### Make the app accessible

Configure your server to enable forwarding from `http://localhost:API_PORT` and `http://localhost:WEB_PORT` to urls on your domain.

## Contributing

Please, see [Contributing](https://github.com/obsqrbtz/InkGoose/blob/master/CONTRIBUTING.md)
