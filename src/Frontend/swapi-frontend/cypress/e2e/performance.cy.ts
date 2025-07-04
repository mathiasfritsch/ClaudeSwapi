describe('Performance Tests', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  it('should load pages within reasonable time limits', () => {
    const startTime = Date.now()
    
    cy.visit('/people')
    cy.waitForApiResponse()
    
    cy.then(() => {
      const loadTime = Date.now() - startTime
      expect(loadTime).to.be.lessThan(5000) // Should load within 5 seconds
    })
  })

  it('should handle rapid navigation without breaking', () => {
    const categories = ['films', 'planets', 'species', 'starships', 'vehicles', 'people']
    
    // Rapidly navigate through categories
    categories.forEach(category => {
      cy.get(`[data-cy="nav-${category}"]`).click()
      cy.url().should('include', `/${category}`)
    })
    
    // Should end up on the last category
    cy.waitForApiResponse()
    cy.checkListItems()
  })

  it('should not have memory leaks during extended navigation', () => {
    // Navigate through multiple categories multiple times
    const categories = ['people', 'films', 'planets']
    
    for (let i = 0; i < 3; i++) {
      categories.forEach(category => {
        cy.get(`[data-cy="nav-${category}"]`).click()
        cy.waitForApiResponse()
        
        // Click on first item and go back
        cy.clickFirstListItem()
        cy.get('button').contains(`Back to`).click()
      })
    }
    
    // Should still be functional after extended use
    cy.checkListItems()
  })

  it('should handle pagination efficiently', () => {
    cy.visit('/people')
    cy.waitForApiResponse()
    
    // Test pagination performance
    cy.get('mat-paginator').within(() => {
      cy.get('[aria-label="Next page"]').then($btn => {
        if (!$btn.prop('disabled')) {
          const startTime = Date.now()
          cy.wrap($btn).click()
          cy.waitForApiResponse()
          
          cy.then(() => {
            const paginationTime = Date.now() - startTime
            expect(paginationTime).to.be.lessThan(3000) // Pagination should be fast
          })
        }
      })
    })
  })
})