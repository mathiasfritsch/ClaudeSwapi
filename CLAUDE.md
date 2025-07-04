# Claude Development Assistant Configuration

## Project Overview
This is a .NET API project with optional Angular frontend, PostgreSQL database, and Azure deployment. The project uses Refit for strongly-typed HTTP clients generated from OpenAPI specifications.

## Architecture
- **Backend**: ASP.NET Core Web API - latest version but only LTS and STS no preview
- **Frontend**: Angular (optional - when present, backend serves as BFF)
- **Database**: PostgreSQL with Entity Framework Core
- **HTTP Clients**: Refit generated from OpenAPI specifications
- **API Documentation**: OpenAPI 3.0 with Scalar UI
- **Containerization**: Docker Compose for local development
- **Deployment**: Azure Web App

## Project Structure
```
src/
├── API/                              # Web API project
│   ├── Controllers/                  # API controllers
│   ├── Program.cs                    # Application entry point
│   └── appsettings*.json            # Configuration files
├── Application/                      # Application layer
│   ├── Services/                     # Business logic services
│   ├── DTOs/                        # Data transfer objects
│   └── Interfaces/                  # Service contracts
├── Domain/                          # Domain models
│   ├── Entities/                    # Domain entities
│   └── Enums/                       # Domain enumerations
├── Infrastructure/                   # Infrastructure layer
│   ├── Data/                        # Entity Framework context
│   ├── HttpClients/                 # HTTP client configurations
│   │   ├── Generated/               # Generated Refit clients
│   │   └── Configuration/           # Client registration
│   └── Migrations/                  # EF Core migrations
├── Tests/                           # Test projects
│   ├── Unit/                        # Unit tests
│   ├── Integration/                 # Integration tests
│   └── E2E/                         # End-to-end tests
├── Frontend/                        # Angular frontend (optional)
│   ├── src/                         # Angular source code
│   └── package.json                 # NPM dependencies
└── openapi-specs/                   # OpenAPI specification files
    ├── external-api-1.json          # External API specs
    └── external-api-2.yaml          # External API specs
```

## Git Workflow
This project uses **Git Flow** with the following branches:
- `main` - Production-ready code
- `develop` - Integration branch for features
- `feature/*` - Feature development branches
- `release/*` - Release preparation branches
- `hotfix/*` - Emergency fixes for production

## Package Management

### Central Package Management
This project uses **Central Package Management** for consistent NuGet package versioning across all projects:

- **Directory.Packages.props** - Defines all package versions centrally
- **Directory.Build.props** - Common MSBuild properties for all projects
- **ManagePackageVersionsCentrally** - Enabled for version consistency

### Package Management Files
```
├── Directory.Packages.props          # Central package version management
├── Directory.Build.props             # Common build properties
├── nuget.config                      # NuGet feed configuration
└── global.json                       # .NET SDK version pinning
```

### Adding New Packages
1. Add package reference to project: `<PackageReference Include="PackageName" />`
2. Define version in `Directory.Packages.props`: `<PackageVersion Include="PackageName" Version="x.x.x" />`
3. Run `dotnet restore` to apply changes

## Local Development Setup

### Prerequisites
- latest .net sdk
- Docker and Docker Compose
- Node.js and npm (if Angular frontend exists), latest angular sdk
- Git

### Initial Setup
1. Clone the repository
2. Run `docker-compose up -d postgres` to start PostgreSQL
3. Run `dotnet restore` to restore NuGet packages (managed centrally)
4. Run `dotnet ef database update` to create database schema
5. If Angular frontend exists, run `npm install` in the Frontend directory

### Development Commands
- `docker-compose up -d` - Start all services
- `docker-compose down` - Stop all services
- `dotnet run` - Start the API
- `dotnet test` - Run tests
- `ng serve` - Start Angular frontend (if applicable)

## HTTP Client Generation with Refit

### Overview
This project uses Refit for strongly-typed HTTP clients, generated from OpenAPI specifications using the Refitter tool.

### Adding New External API Clients

1. **Add OpenAPI Specification**
   - Place the OpenAPI spec file in `openapi-specs/` directory
   - Supported formats: JSON, YAML, Swagger 2.0

2. **Configure Refitter**
   - Update `refitter.json` with the new API configuration
   - Set output paths and generation options

3. **Generate Refit Client**
   ```bash
   dotnet tool run refitter --openapi-file openapi-specs/your-api.json --output src/Infrastructure/HttpClients/Generated/YourApi
   ```

4. **Register HTTP Client**
   - Add client registration in `Program.cs` or service configuration
   - Configure base URL, authentication, and policies

### Example Refitter Configuration
```json
{
  "openApiPath": "openapi-specs/external-api.json",
  "outputPath": "src/Infrastructure/HttpClients/Generated",
  "namespace": "YourProject.Infrastructure.HttpClients",
  "naming": {
    "useOpenApiTitle": true,
    "interfacePrefix": "I",
    "interfaceSuffix": "Api"
  },
  "codeGeneratorSettings": {
    "generateContracts": true,
    "generateXmlDocCodeReferences": true,
    "useApiResponse": true,
    "useObservableResponse": false
  }
}
```

### HTTP Client Registration Example
```csharp
// In Program.cs
builder.Services.AddRefitClient<IExternalApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ExternalApi:BaseUrl"]))
    .AddHttpMessageHandler<AuthenticationHandler>()
    .AddPolicyHandler(GetRetryPolicy());
```

## Database Management

### Entity Framework Commands
- `dotnet ef migrations add MigrationName` - Add new migration
- `dotnet ef database update` - Apply migrations
- `dotnet ef migrations remove` - Remove last migration
- `dotnet ef database drop` - Drop database

### Connection String
Local development uses PostgreSQL in Docker:
```
"DefaultConnection": "Host=localhost;Database=YourProjectDb;Username=postgres;Password=yourpassword"
```

## API Documentation
The API uses OpenAPI 3.0 with Scalar UI for documentation.

Access documentation at: `https://localhost:5001/scalar/v1`

## Testing Strategy

### Unit Tests
- Test business logic in isolation
- Mock external dependencies
- Use NUnit, Shouldly, and NSubstitute for mocking

### Integration Tests
- Test API endpoints end-to-end
- Use TestContainers for database
- Test with real HTTP clients

### E2E Tests
- Test critical user workflows
- Use  Cypress, Cypress should not access backed but have api mocks

## Environment Configuration

### Required Environment Variables
- `POSTGRES_CONNECTION_STRING` - Database connection
- `JWT_SECRET_KEY` - Authentication secret
- `AZURE_CLIENT_ID` - Azure AD client ID
- `AZURE_CLIENT_SECRET` - Azure AD client secret
- `AZURE_TENANT_ID` - Azure AD tenant ID

### Optional Environment Variables
- `REDIS_CONNECTION_STRING` - Redis cache connection
- `APPLICATIONINSIGHTS_CONNECTION_STRING` - Application Insights
- `CORS_ALLOWED_ORIGINS` - CORS configuration
- `EXTERNAL_API_BASE_URLS` - External API endpoints
- `HTTP_CLIENT_TIMEOUT` - HTTP client timeout settings

## Deployment

### Azure Web App Deployment
1. **Development Environment**
   - Deploys automatically from `develop` branch
   - Uses development configuration

2. **Staging Environment**
   - Deploys from `release/*` branches
   - Manual deployment approval required

3. **Production Environment**
   - Deploys from `main` branch
   - Manual deployment approval required
   - Includes health checks and rollback capabilities

### Docker Support
The project includes Docker support for containerized deployment:
- Multi-stage Dockerfile for optimized production builds
- Docker Compose for local development
- Health checks and monitoring

## Best Practices

### Code Quality
- Follow Clean Architecture principles
- Use dependency injection throughout
- Implement proper error handling
- Write comprehensive tests

### Code Organization
- **One Type Per File**: Always create separate files for classes, interfaces, and enums
- **File Naming**: File names must match the type name exactly (e.g., `IPeopleService.cs` for interface `IPeopleService`)
- **Namespace Structure**: Organize namespaces to reflect the folder structure
- **Type Separation**: Never define multiple public types in a single file

### API Design
- Follow RESTful conventions
- Use appropriate HTTP status codes
- Implement proper versioning
- Generate comprehensive OpenAPI documentation

### Security
- Implement authentication and authorization
- Validate all input data
- Use HTTPS in production
- Implement rate limiting

### Performance
- Use async/await patterns
- Implement caching strategies
- Optimize database queries
- Use connection pooling

## Monitoring and Logging

### Application Insights
- Performance monitoring
- Error tracking
- Custom metrics and telemetry

### Structured Logging
- Use Serilog for structured logging
- Include correlation IDs
- Log important business events

### Health Checks
- Database connectivity
- External API availability
- Application health endpoints

## Common Tasks

### Adding a New Feature
1. Create feature branch from `develop`
2. Update OpenAPI specs if API changes
3. Generate/update Refit clients if needed
4. Implement feature with tests
5. Update documentation
6. Create pull request to `develop`

### Updating External API Clients
1. Update OpenAPI specification file
2. Run refitter to regenerate clients
3. Update client registration if needed
4. Test integration with new client
5. Update documentation

### Database Schema Changes
1. Create Entity Framework migration
2. Review generated migration code
3. Test migration on development database
4. Deploy migration with application update

## Troubleshooting

### Common Issues
1. **Database Connection Issues**
   - Check PostgreSQL container status
   - Verify connection string configuration
   - Ensure database migrations are applied

2. **HTTP Client Issues**
   - Verify external API specifications are current
   - Check client registration in DI container
   - Validate authentication configuration

3. **Build Issues**
   - Ensure all NuGet packages are restored via central package management
   - Check .NET SDK version compatibility in `global.json`
   - Verify package versions in `Directory.Packages.props`
   - Run `dotnet restore --force` to reset package cache
   - Verify Docker containers are running

4. **Package Management Issues**
   - Ensure `Directory.Packages.props` contains all required package versions
   - Check for version conflicts between projects
   - Verify `ManagePackageVersionsCentrally` is enabled in all projects
   - Use `dotnet list package --include-transitive` to check package tree

### Debug Tips
- Use Application Insights for production debugging
- Check Docker container logs: `docker-compose logs [service]`
- Use EF Core logging for database issues
- Enable detailed HTTP client logging for API issues

## Contributing
1. Follow the established Git Flow workflow
2. Write tests for new features
3. Update documentation as needed
4. Follow code style guidelines
5. Ensure all tests pass before submitting PR