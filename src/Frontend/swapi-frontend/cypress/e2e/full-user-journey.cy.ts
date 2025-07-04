describe('Full User Journey Tests', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  it('should complete a full exploration of the Star Wars universe', () => {
    // Start with People (default page)
    cy.url().should('include', '/people')
    cy.waitForApiResponse()
    cy.checkListItems()
    
    // Navigate through all categories
    const categories = [
      { name: 'films', detailText: 'Back to Films' },
      { name: 'planets', detailText: 'Back to Planets' },
      { name: 'species', detailText: 'Back to Species' },
      { name: 'starships', detailText: 'Back to Starships' },
      { name: 'vehicles', detailText: 'Back to Vehicles' }
    ]
    
    categories.forEach(category => {
      // Navigate to category
      cy.get(`[data-cy="nav-${category.name}"]`).click()
      cy.url().should('include', `/${category.name}`)
      cy.waitForApiResponse()
      cy.checkListItems()
      
      // Click on first item to view details
      cy.clickFirstListItem()
      cy.get('button').should('contain', category.detailText)
      
      // Navigate back to list
      cy.get('button').contains(category.detailText).click()
      cy.url().should('include', `/${category.name}`)
      cy.checkListItems()
    })
    
    // Return to people to complete the journey
    cy.get('[data-cy="nav-people"]').click()
    cy.url().should('include', '/people')
    cy.waitForApiResponse()
    cy.checkListItems()
  })

  it('should handle deep linking correctly', () => {
    // Test direct navigation to specific pages
    const testRoutes = [
      '/films',
      '/planets', 
      '/species',
      '/starships',
      '/vehicles'
    ]
    
    testRoutes.forEach(route => {
      cy.visit(route)
      cy.url().should('include', route)
      cy.waitForApiResponse()
      cy.checkListItems()
    })
  })

  it('should maintain proper navigation state throughout journey', () => {
    // Check navigation highlighting
    cy.get('[data-cy="nav-people"]').should('have.class', 'active')
    
    cy.get('[data-cy="nav-films"]').click()
    cy.get('[data-cy="nav-films"]').should('have.class', 'active')
    cy.get('[data-cy="nav-people"]').should('not.have.class', 'active')
    
    cy.get('[data-cy="nav-planets"]').click()
    cy.get('[data-cy="nav-planets"]').should('have.class', 'active')
    cy.get('[data-cy="nav-films"]').should('not.have.class', 'active')
  })

  it('should handle pagination across different categories', () => {
    const categoriesWithPagination = ['people', 'films', 'planets']
    
    categoriesWithPagination.forEach(category => {
      cy.get(`[data-cy="nav-${category}"]`).click()
      cy.waitForApiResponse()
      
      // Check if pagination exists
      cy.get('mat-paginator').should('be.visible')
      
      // If there's a next page, test pagination
      cy.get('mat-paginator').within(() => {
        cy.get('[aria-label="Next page"]').then($btn => {
          if (!$btn.prop('disabled')) {
            cy.wrap($btn).click()
            cy.waitForApiResponse()
            cy.checkListItems()
          }
        })
      })
    })
  })
})