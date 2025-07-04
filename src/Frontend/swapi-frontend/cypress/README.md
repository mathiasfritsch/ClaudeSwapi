# SWAPI Frontend E2E Tests

This directory contains comprehensive end-to-end tests for the SWAPI Explorer application using Cypress.

## Test Coverage

### 🧪 Test Suites

1. **Navigation Tests** (`navigation.cy.ts`)
   - Verifies navigation bar functionality
   - Tests active state management
   - Ensures proper routing between categories

2. **Category List Tests**
   - `people-list.cy.ts` - People listing and detail navigation
   - `films-list.cy.ts` - Films listing and detail navigation
   - `planets-list.cy.ts` - Planets listing and detail navigation
   - `species-list.cy.ts` - Species listing and detail navigation
   - `starships-list.cy.ts` - Starships listing and detail navigation
   - `vehicles-list.cy.ts` - Vehicles listing and detail navigation

3. **Full User Journey** (`full-user-journey.cy.ts`)
   - Complete exploration flow through all categories
   - Deep linking validation
   - Navigation state persistence
   - Cross-category pagination testing

4. **Error Handling** (`error-handling.cy.ts`)
   - API failure graceful handling
   - Slow response management
   - Invalid route handling
   - Responsive design testing

5. **Performance Tests** (`performance.cy.ts`)
   - Load time validation
   - Rapid navigation stress testing
   - Memory leak prevention
   - Pagination efficiency

## 🚀 Running Tests

### Prerequisites
- Frontend application running on `http://localhost:4200`
- Backend API running on `http://localhost:5001`

### Commands

```bash
# Install dependencies (if not already done)
npm install

# Open Cypress Test Runner (Interactive)
npm run cypress:open

# Run all tests headlessly
npm run cypress:run

# Run all e2e tests
npm run e2e

# Run specific test file
npx cypress run --spec "cypress/e2e/navigation.cy.ts"
```

### Test Development

```bash
# Open Cypress in development mode
npm run cypress:open
```

## 🎯 Test Strategy

### What We Test
- ✅ Navigation between all Star Wars categories
- ✅ List view functionality for all entity types
- ✅ Detail page navigation and content
- ✅ Loading states and API response handling
- ✅ Pagination across all categories
- ✅ Error handling and edge cases
- ✅ Responsive design
- ✅ Performance and memory management

### Custom Commands
- `cy.waitForApiResponse()` - Waits for API calls to complete
- `cy.navigateToCategory(category)` - Navigates to specific SWAPI category
- `cy.checkListItems()` - Validates list items are properly displayed
- `cy.clickFirstListItem()` - Clicks first item and navigates to detail

### Data Attributes
Tests use `data-cy` attributes for reliable element selection:
- `data-cy="nav-people"` - Navigation buttons
- `data-cy="loading-spinner"` - Loading indicators

## 📊 Test Results

Tests cover the complete Star Wars universe exploration:
- **6 Categories**: People, Films, Planets, Species, Starships, Vehicles
- **12 Page Types**: 6 list pages + 6 detail pages
- **Full Navigation**: All possible user journeys
- **Error Scenarios**: API failures, invalid routes, slow responses
- **Performance**: Load times, rapid navigation, pagination

## 🛠️ Maintenance

### Adding New Tests
1. Create new test file in `cypress/e2e/`
2. Follow existing naming convention
3. Use custom commands for common actions
4. Add `data-cy` attributes to new components

### Debugging Tests
1. Use `cy.pause()` to stop test execution
2. Use `cy.debug()` to inspect state
3. Enable video recording for headless runs
4. Check browser console for errors

### CI/CD Integration
Tests are designed to run in CI environments:
- No external dependencies beyond SWAPI API
- Configurable timeouts
- Reliable selectors using data attributes
- Comprehensive error handling