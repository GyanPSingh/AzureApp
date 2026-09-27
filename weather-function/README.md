# Weather Forecast API

This Azure Functions project exposes a small weather API for a location-based lookup.

## Endpoints

- `GET /api/health`
- `GET /api/weather?location={city}`
- `GET /api/forecast?location={city}&days=5`

## Run locally

```bash
dotnet restore
dotnet build
func start
```
