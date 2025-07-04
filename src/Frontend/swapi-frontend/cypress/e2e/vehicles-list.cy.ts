describe('Vehicles List Tests', () => {
  beforeEach(() => {
    cy.visit('/vehicles')
  })

  it('should display the vehicles list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Vehicles')
    cy.get('mat-card-subtitle').should('contain', 'Discover ground and atmospheric vehicles')
  })

  it('should load and display vehicle cards', () => {
    cy.waitForApiResponse()
    cy.checkListItems()
    
    // Check that vehicle cards have required content
    cy.get('app-vehicle-card').first().within(() => {
      cy.get('mat-card-title').should('not.be.empty')
      cy.get('mat-card-subtitle').should('contain', 'Vehicle')
      cy.get('button').should('contain', 'View Details')
    })
  })

  it('should navigate to vehicle detail page when clicking a card', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Should be on vehicle detail page
    cy.url().should('match', /\/vehicle\/\d+/)
    cy.get('mat-card-title').should('not.be.empty')
    cy.get('button').should('contain', 'Back to Vehicles')
  })

  it('should show detailed vehicle information on detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Check for detailed vehicle information
    cy.contains('Technical Specifications').should('be.visible')
    cy.get('mat-list-item').should('have.length.at.least', 7)
    cy.contains('Model').should('be.visible')
    cy.contains('Manufacturer').should('be.visible')
  })

  it('should navigate back from vehicle detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Go back to list
    cy.get('button').contains('Back to Vehicles').click()
    cy.url().should('include', '/vehicles')
    cy.checkListItems()
  })
})