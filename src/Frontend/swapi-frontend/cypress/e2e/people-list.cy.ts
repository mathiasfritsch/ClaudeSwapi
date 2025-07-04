describe('People List Tests', () => {
  beforeEach(() => {
    cy.visit('/people')
  })

  it('should display the people list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Characters')
    cy.get('mat-card-subtitle').should('contain', 'Browse through the galaxy\'s most famous characters')
  })

  it('should load and display people cards', () => {
    cy.waitForApiResponse()
    cy.checkListItems()
    
    // Check that person cards have required content
    cy.get('mat-grid-tile').first().within(() => {
      cy.get('app-person-card').should('be.visible')
      cy.get('mat-card-title').should('not.be.empty')
      cy.get('button').should('contain', 'View Details')
    })
  })

  it('should navigate to person detail page when clicking a card', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Should be on person detail page
    cy.url().should('match', /\/person\/\d+/)
    cy.get('mat-card-title').should('not.be.empty')
    cy.get('button').should('contain', 'Back to Characters')
  })

  it('should have working pagination', () => {
    cy.waitForApiResponse()
    
    cy.get('mat-paginator').should('be.visible')
    
    // Check if there are pagination buttons (if more than one page)
    cy.get('mat-paginator').within(() => {
      cy.get('[aria-label="Next page"]').should('exist')
    })
  })

  it('should handle loading states properly', () => {
    cy.visit('/people')
    
    // Should show loading initially (might be fast)
    cy.get('[data-cy="loading-spinner"]').should('exist')
    
    // Should hide loading when content loads
    cy.waitForApiResponse()
    cy.get('[data-cy="loading-spinner"]').should('not.exist')
  })

  it('should navigate back from detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Go back to list
    cy.get('button').contains('Back to Characters').click()
    cy.url().should('include', '/people')
    cy.checkListItems()
  })
})