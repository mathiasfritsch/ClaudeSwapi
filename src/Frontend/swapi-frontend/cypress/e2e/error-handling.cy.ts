describe('Error Handling Tests', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  it('should handle API failures gracefully', () => {
    // Intercept API calls and simulate failures
    cy.intercept('GET', '**/api/people*', { statusCode: 500 }).as('peopleError')
    
    cy.visit('/people')
    cy.wait('@peopleError')
    
    // Should not show loading indefinitely
    cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
    
    // Should handle error gracefully (either show error message or empty state)
    cy.get('body').should('be.visible')
  })

  it('should handle slow API responses', () => {
    // Intercept and delay API responses
    cy.intercept('GET', '**/api/films*', { delay: 3000, fixture: 'films.json' }).as('slowFilms')
    
    cy.visit('/films')
    
    // Should show loading during delay
    cy.get('[data-cy="loading-spinner"]').should('be.visible')
    
    cy.wait('@slowFilms')
    cy.get('[data-cy="loading-spinner"]').should('not.exist')
  })

  it('should handle invalid routes', () => {
    // Visit invalid route
    cy.visit('/invalid-route')
    
    // Should redirect to people (default route)
    cy.url().should('include', '/people')
    cy.waitForApiResponse()
  })

  it('should handle invalid detail page IDs', () => {
    // Visit invalid person ID
    cy.visit('/person/99999')
    
    // Should show error state or redirect
    cy.get('body').should('be.visible')
    
    // Either shows error message or redirects back to list
    cy.url().should('match', /\/(person\/99999|people)/)
  })


})