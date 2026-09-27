# Project Plan

**Status**: Integrated
**Created**: 2026-09-27
**Mode**: NEW

---

## 1. Project Overview

**Goal**: Build a weather forecast Azure Function API that serves current conditions and multi-day forecasts for a provided location, with a clean, testable serverless backend. The project is designed so that every module is independently testable.

**App Type**: API only

**API Login**: No

**Mode**: NEW

**Deployment Plan**: No deployment plan found

---

## 2. Backend — Azure Functions

| Component | Technology |
|-----------|-----------|
| **Language** | C# |
| **Runtime** | .NET |
| **Package Manager** | dotnet (NuGet) |
| **Test Runner** | xUnit |
| **Mocking Library** | NSubstitute |
| **Test Command** | dotnet test |
| **Orchestration** | docker-compose |

> **Language vs Runtime**: `Language` is the source language the user picked in this service's `language` question. `Runtime` is the execution runtime — default `Node` for TypeScript/JavaScript, `CPython` for Python, `.NET` for C#. Only deviate from the default (e.g. `Bun`, `Deno`, `PyPy`) when the user explicitly asks. **Package Manager and Test Runner are language-dependent** — match them to this service's Language (e.g. C# → `dotnet (NuGet)` + `xUnit`/`NUnit`/`MSTest`). The `Orchestration` row is recorded for the scaffold step but hidden in the plan UI — always keep it set to `docker-compose`.

---

## 3. Services Required

| Azure Service | Role in App | Environment Variable | Default Value (Local) | Classification |
|---------------|------------|---------------------|----------------------|----------------|
| Azure Functions | Serverless API runtime for weather endpoints | AzureWebJobsStorage | UseDevelopmentStorage=true | Essential |
| Application Insights | Request and exception telemetry for the API | APPLICATIONINSIGHTS_CONNECTION_STRING | local default disabled | Enhancement |

---

## 4. Prerequisites

Identify the required tools, then inventory them by following [prerequisites.md](../shared-references/prerequisites.md). Always produce **both** groups — `### Run` and `### Debug` — as two sub-tables under this section. The plan webview shows the Run group always and the Debug group only when the user turns on the Autopilot toggle, so do not omit either group yourself.

### Run

| Tool | Service(s) | Installed | Version |
|------|------------|-----------|---------|
| .NET SDK | Weather Forecast API | ✅ | 10.0.201 |
| Azure Functions Core Tools | Weather Forecast API | ✅ | 4.10.0 |

### Debug

| Tool | Service(s) | Installed | Version |
|------|------------|-----------|---------|
| Docker | Weather Forecast API | ❓ | Unknown |
| Docker Compose | Weather Forecast API | ❓ | Unknown |
| ms-azuretools.vscode-azurefunctions | Weather Forecast API | ❓ | Unknown |

---

## 5. Project Structure

```
weather-function/
├─ src/
│  └─ WeatherApi/
│     ├─ Functions/
│     │  ├─ HealthFunction.cs
│     │  ├─ CurrentWeatherFunction.cs
│     │  └─ ForecastFunction.cs
│     ├─ Models/
│     │  ├─ WeatherResponse.cs
│     │  ├─ ForecastResponse.cs
│     │  └─ LocationQuery.cs
│     ├─ Services/
│     │  ├─ IWeatherClient.cs
│     │  └─ WeatherClient.cs
│     ├─ Program.cs
│     └─ WeatherApi.csproj
├─ tests/
│  └─ WeatherApi.Tests/
│     ├─ HealthFunctionTests.cs
│     ├─ CurrentWeatherFunctionTests.cs
│     └─ ForecastFunctionTests.cs
├─ host.json
├─ local.settings.json
├─ README.md
├─ .gitignore
└─ WeatherApi.sln
```

---

## 6. Route Definitions

| # | Method | Path | Description | Request Body | Response Body | Status Codes |
|---|--------|------|-------------|-------------|--------------|-------------|
| 1 | GET | `/api/health` | Health check for the function app | — | `{ status, services }` | 200, 503 |
| 2 | GET | `/api/weather?location={city}` | Returns current weather for a city or coordinate pair | — | `{ location, temperatureC, conditions, source, updatedAt }` | 200, 400, 404, 500 |
| 3 | GET | `/api/forecast?location={city}&days=5` | Returns a multi-day forecast for a location | — | `{ location, units, forecast: [{ date, highC, lowC, summary }] }` | 200, 400, 404, 500 |

---

## 7. Next Steps

1. Run **azure-project-scaffold** to execute this plan
2. Run **azure-project-integrate** to wire the frontend to live data, smoke-test the backend, and create the migrations
3. Run **azure-debug-plan** → **azure-debug-generate** for Docker emulators and VS Code debugging
4. Run the **azure-deploy** agent when ready; it uses **azure-app-onboard** for architecture, cost estimation, IaC generation, provisioning, and health verification
