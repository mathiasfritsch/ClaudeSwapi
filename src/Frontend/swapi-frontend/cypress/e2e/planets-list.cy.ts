describe('Planets List Tests', () => {
  beforeEach(() => {
    cy.visit('/planets')
  })

  it('should display the planets list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Planets')
    cy.get('mat-card-subtitle').should('contain', 'Explore worlds across the galaxy')
  })

  it('should load and display planet cards', () => {
    cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
    
    cy.get('body').then(($body) => {
      if ($body.find('.no-data').length > 0) {
        cy.get('.no-data').should('be.visible')
        cy.get('.no-data').should('contain', 'No planets found')
      } else {
        cy.get('mat-grid-tile').should('have.length.at.least', 1)
        
        cy.get('app-planet-card').first().within(() => {
          cy.get('mat-card-title').should('not.be.empty')
          cy.get('mat-card-subtitle').should('contain', 'Planet')
          cy.get('button').should('contain', 'View Details')
        })
      }
    })
  })

  it('should navigate to planet detail page when clicking a card', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Should be on planet detail page
    cy.url().should('match', /\/planet\/\d+/)
    cy.get('mat-card-title').should('not.be.empty')
    cy.get('button').should('contain', 'Back to Planets')
  })

  it('should show detailed planet information on detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Check for detailed planet information
    cy.contains('Physical Characteristics').should('be.visible')
    cy.get('mat-list-item').should('have.length.at.least', 5)
    cy.contains('Climate').should('be.visible')
    cy.contains('Terrain').should('be.visible')
  })

  it('should navigate back from planet detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Go back to list
    cy.get('button').contains('Back to Planets').click()
    cy.url().should('include', '/planets')
    cy.checkListItems()
  })
})