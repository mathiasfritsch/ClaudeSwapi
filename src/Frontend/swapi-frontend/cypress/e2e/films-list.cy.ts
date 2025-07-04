describe('Films List Tests', () => {
  beforeEach(() => {
    cy.visit('/films')
  })

  it('should display the films list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Films')
    cy.get('mat-card-subtitle').should('contain', 'Explore the galaxy\'s greatest stories')
  })

  it('should load and display film cards', () => {
    cy.waitForApiResponse()
    cy.checkListItems()
    
    // Check that film cards have required content if data exists
    cy.get('body').then(($body) => {
      if ($body.find('mat-grid-tile').length > 0) {
        cy.get('mat-grid-tile').first().within(() => {
          cy.get('app-film-card').should('be.visible')
          cy.get('mat-card-title').should('not.be.empty')
          cy.get('button').should('contain', 'View Details')
        })
      }
    })
  })

  it('should navigate to film detail page when clicking a card', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Should be on film detail page
    cy.url().should('match', /\/film\/\d+/)
    cy.get('mat-card-title').should('not.be.empty')
    cy.get('button').should('contain', 'Back to Films')
  })

  it('should display film information in cards', () => {
    cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
    
    cy.get('body').then(($body) => {
      if ($body.find('app-film-card').length > 0) {
        cy.get('app-film-card').first().within(() => {
          cy.get('mat-card-content').within(() => {
            cy.contains('Director:').should('be.visible')
            cy.contains('Producer:').should('be.visible')
            cy.contains('Release Date:').should('be.visible')
          })
        })
      } else {
        cy.log('No films data available for card content test')
      }
    })
  })

  it('should show detailed film information on detail page', () => {
    cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
    
    cy.get('body').then(($body) => {
      if ($body.find('mat-grid-tile').length > 0) {
        cy.get('mat-grid-tile').first().click()
        cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
        
        // Check for detailed film information
        cy.get('mat-list-item').should('have.length.at.least', 3)
        cy.contains('Director').should('be.visible')
        cy.contains('Producer').should('be.visible')
        cy.contains('Release Date').should('be.visible')
      } else {
        cy.log('No films data available for detail test')
      }
    })
  })

  it('should navigate back from film detail page', () => {
    cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
    
    cy.get('body').then(($body) => {
      if ($body.find('mat-grid-tile').length > 0) {
        cy.get('mat-grid-tile').first().click()
        cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
        
        // Go back to list
        cy.get('button').contains('Back to Films').click()
        cy.url().should('include', '/films')
        cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
      } else {
        cy.log('No films data available for navigation back test')
      }
    })
  })
})