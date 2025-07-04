describe('Species List Tests', () => {
  beforeEach(() => {
    cy.visit('/species')
  })

  it('should display the species list page', () => {
    cy.get('mat-card-title').should('contain', 'Star Wars Species')
    cy.get('mat-card-subtitle').should('contain', 'Discover the diverse life forms')
  })

  it('should load and display species cards', () => {
    cy.waitForApiResponse()
    cy.checkListItems()
    
    // Check that species cards have required content
    cy.get('app-species-card').first().within(() => {
      cy.get('mat-card-title').should('not.be.empty')
      cy.get('mat-card-subtitle').should('contain', 'Species')
      cy.get('button').should('contain', 'View Details')
    })
  })

  it('should navigate to species detail page when clicking a card', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Should be on species detail page
    cy.url().should('match', /\/species\/\d+/)
    cy.get('mat-card-title').should('not.be.empty')
    cy.get('button').should('contain', 'Back to Species')
  })

  it('should show detailed species information on detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Check for detailed species information
    cy.contains('Biological Characteristics').should('be.visible')
    cy.get('mat-list-item').should('have.length.at.least', 5)
    cy.contains('Classification').should('be.visible')
    cy.contains('Average Height').should('be.visible')
  })

  it('should navigate back from species detail page', () => {
    cy.waitForApiResponse()
    cy.clickFirstListItem()
    
    // Go back to list
    cy.get('button').contains('Back to Species').click()
    cy.url().should('include', '/species')
    cy.checkListItems()
  })
})