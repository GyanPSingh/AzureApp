# Integration Plan

## Backend
- Project folder: weather-function
- Build command: dotnet build "weather-function/weather-function.csproj"
- Run command: func start --port 7071
- Port: 7071
- Health endpoint: GET /api/health
- App service: Azure Functions v4 isolated worker (.NET)

## Frontend
- Frontend: none (API-only project)

## API routes
- GET /api/health
- GET /api/weather?location={city}
- GET /api/forecast?location={city}&days=5

## Database
- Type: none
- Migration tool: n/a
- Migration directory: n/a
- Connection env vars: n/a
- Note: No seed data is to be created.

## Shared types
- Shared package: n/a

## Services
- Essential: Azure Functions runtime
- Enhancement: Application Insights

## Integration results
- Backend build succeeded: `dotnet build "weather-function/weather-function.csproj"`
- Live endpoint verification succeeded for `GET /api/health`, `GET /api/weather?location=Seattle`, and `GET /api/forecast?location=Seattle&days=3`
- Validation check passed for missing location input: `GET /api/weather` returned `400` with `invalid_location`
- This scaffold is API-only; there is no frontend, no mock-data layer, and no relational database configured, so no frontend live-data swap or SQL/PostgreSQL migration was required by the actual project structure
- No seed data was created or run
