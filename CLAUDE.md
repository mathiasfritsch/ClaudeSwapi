# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.
It assumes a more general claude.md exists in the parent or home directory.

## Project Overview
This is a SWAPI (Star Wars API) integration project with a .NET Web API backend and Angular frontend. The backend integrates with the external SWAPI API to retrieve Star Wars character data.

## Architecture
- **External API**: SWAPI (Star Wars API) integration via custom HTTP client


## SWAPI Integration

### Architecture Overview
The project integrates with the external SWAPI (Star Wars API) 

### Key Integration Points
- API Base URL: `https://www.swapi.tech/api/`
- Main endpoints: `/people` (list) and `/people/{id}` (detail)
- Real-time data retrieval (no caching implemented)

## API Endpoints

### Available Endpoints
- `GET /api/people` - Get paginated list of Star Wars characters
  - Query parameters: `page` (optional), `limit` (optional)
- `GET /api/people/{id}` - Get detailed information for a specific character

## Frontend Architecture

### Angular Application Structure
- **Key Components**:
  - `people-list`: Displays paginated list of characters with search
  - `person-card`: Reusable character card component
  - `person-detail`: Detailed character information view
- **Services**: `people.service.ts` handles API integration
- 
## Testing Strategy

### Planned Testing Approach
- **Integration Tests**: API endpoint testing with real SWAPI integration
- **Frontend Tests**: 
  - Add frontend tests with Cypress for comprehensive UI and interaction testing

## Common Development Tasks

### Adding New API Endpoints
1. Create feature branch from `develop`
2. Add new methods to `ISwapiClient` interface (Application layer)
3. Implement API calls in `SwapiClient` (Infrastructure layer)
4. Add controller endpoints in `PeopleController` (API layer)
5. Update frontend service and components as needed
6. Add appropriate error handling and logging

### Extending SWAPI Integration
1. Add new models to `SwapiModels.cs` for additional SWAPI endpoints
2. Extend `ISwapiApiClient` with new Refit interface methods
3. Update mapping logic in `SwapiClient`
4. Add corresponding domain models if needed

## Troubleshooting

### Common Issues
1. **SWAPI API Issues**
   - Check if `https://www.swapi.tech/api/` is accessible
   - Verify HTTP client configuration in `DependencyInjectionExtensions.cs`
   - Check application logs for API response errors

2  **Frontend Issues**
   - Run `npm install` in `src/Frontend/swapi-frontend/` directory
   - Check for CORS issues between frontend (port 4200) and API (port 5001)
   - Verify Angular service is calling correct API endpoints

## Frontend Enhancements

### Navigation and Detail Pages
- Implement comprehensive navigation to detail pages for all Star Wars universe lists
  - Ensure all list components have options to navigate to respective detail pages
  - Cover all Star Wars universe categories (people, planets, starships, vehicles, species, films)