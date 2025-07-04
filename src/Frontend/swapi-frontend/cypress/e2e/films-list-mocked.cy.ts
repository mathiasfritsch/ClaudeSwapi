describe('Films List Tests with Mocked Data', () => {
  beforeEach(() => {
    // Mock the API responses to ensure consistent test behavior
    cy.intercept('GET', '**/api/films*', { fixture: 'films.json' }).as('getFilms')
    cy.visit('/films')
  })

  it('should display the films list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Films')
    cy.get('mat-card-subtitle').should('contain', 'Explore the galaxy')
  })

  it('should load and display film cards with mocked data', () => {
    cy.wait('@getFilms')
    cy.get('[data-cy="loading-spinner"]').should('not.exist')
    
    // Should have film cards
    cy.get('mat-grid-tile').should('have.length.at.least', 1)
    
    // Check that film cards have required content
    cy.get('app-film-card').first().within(() => {
      cy.get('mat-card-title').should('not.be.empty')
      cy.get('mat-card-subtitle').should('contain', 'Episode')
      cy.get('button').should('contain', 'View Details')
    })
  })

  it('should display film information in cards with mocked data', () => {
    cy.wait('@getFilms')
    cy.get('[data-cy="loading-spinner"]').should('not.exist')
    
    cy.get('app-film-card').first().within(() => {
      cy.get('mat-card-content').within(() => {
        cy.contains('Director:').should('be.visible')
        cy.contains('Producer:').should('be.visible')
        cy.contains('Release Date:').should('be.visible')
      })
    })
  })

  it('should handle API loading states', () => {
    // Intercept with delay to test loading state
    cy.intercept('GET', '**/api/films*', { 
      fixture: 'films.json',
      delay: 1000 
    }).as('getFilmsDelayed')
    
    cy.visit('/films')
    
    // Should show loading initially
    cy.get('[data-cy="loading-spinner"]').should('be.visible')
    
    cy.wait('@getFilmsDelayed')
    
    // Should hide loading after response
    cy.get('[data-cy="loading-spinner"]').should('not.exist')
    cy.get('mat-grid-tile').should('have.length.at.least', 1)
  })

  it('should handle empty data gracefully', () => {
    // Mock empty response
    cy.intercept('GET', '**/api/films*', { 
      body: {
        message: "ok",
        totalRecords: 0,
        totalPages: 0,
        previous: null,
        next: null,
        results: []
      }
    }).as('getEmptyFilms')
    
    cy.visit('/films')
    cy.wait('@getEmptyFilms')
    
    cy.get('[data-cy="loading-spinner"]').should('not.exist')
    cy.get('.no-data').should('be.visible')
    cy.get('.no-data').should('contain', 'No films found')
  })
})