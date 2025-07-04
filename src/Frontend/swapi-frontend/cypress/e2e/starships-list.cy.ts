describe('Starships List Tests', () => {
  beforeEach(() => {
    cy.visit('/starships')
  })

  it('should display the starships list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Starships')
    cy.get('mat-card-subtitle').should('contain', 'Explore the most advanced vessels')
  })

  it('should load and display starship cards', () => {
    cy.waitForApiResponse()
    cy.checkListItems()
    
    // Check that starship cards have required content
    cy.get('app-starship-card').first().within(() => {
      cy.get('mat-card-title').should('not.be.empty')
      cy.get('mat-card-subtitle').should('contain', 'Starship')
      cy.get('button').should('contain', 'View Details')
    })
  })

  it('should navigate to starship detail page when clicking a card', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Should be on starship detail page
    cy.url().should('match', /\/starship\/\d+/)
    cy.get('mat-card-title').should('not.be.empty')
    cy.get('button').should('contain', 'Back to Starships')
  })

  it('should show detailed starship information on detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Check for detailed starship information
    cy.contains('Technical Specifications').should('be.visible')
    cy.get('mat-list-item').should('have.length.at.least', 8)
    cy.contains('Model').should('be.visible')
    cy.contains('Manufacturer').should('be.visible')
    cy.contains('Hyperdrive Rating').should('be.visible')
  })

  it('should navigate back from starship detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Go back to list
    cy.get('button').contains('Back to Starships').click()
    cy.url().should('include', '/starships')
    cy.checkListItems()
  })
})