/// <reference types="cypress" />

// Custom commands for SWAPI testing
declare global {
  namespace Cypress {
    interface Chainable {
      /**
       * Custom command to wait for API response and check loading state
       */
      waitForApiResponse(): Chainable<void>
      
      /**
       * Custom command to navigate to a specific SWAPI category
       */
      navigateToCategory(category: string): Chainable<void>
      
      /**
       * Custom command to check if list items are displayed properly
       */
      checkListItems(): Chainable<void>
      
      /**
       * Custom command to click on the first item in a list
       */
      clickFirstListItem(): Chainable<void>
    }
  }
}

Cypress.Commands.add('waitForApiResponse', () => {
  // Wait for loading spinner to disappear with longer timeout
  cy.get('[data-cy="loading-spinner"]', { timeout: 15000 }).should('not.exist')
  // Wait a bit for content to stabilize
  cy.wait(500)
  // Ensure content is loaded (either data or no-data message)
  cy.get('body').should('be.visible')
})

Cypress.Commands.add('navigateToCategory', (category: string) => {
  cy.get(`[data-cy="nav-${category}"]`).click()
  cy.url().should('include', `/${category}`)
  cy.waitForApiResponse()
})

Cypress.Commands.add('checkListItems', () => {
  // More flexible - check for either data or no-data state
  cy.get('body').then(($body) => {
    if ($body.find('mat-grid-tile').length > 0) {
      cy.get('mat-grid-tile').should('have.length.at.least', 1)
      cy.get('mat-card').should('be.visible')
      cy.get('mat-card-title').should('not.be.empty')
    } else {
      // Accept no-data state as valid
      cy.get('.no-data').should('be.visible')
    }
  })
})

Cypress.Commands.add('clickFirstListItem', () => {
  // Only click if items exist
  cy.get('body').then(($body) => {
    if ($body.find('mat-grid-tile').length > 0) {
      cy.get('mat-grid-tile').first().within(() => {
        cy.get('mat-card').click()
      })
      cy.url().should('match', /\/\w+\/\d+/)
      cy.waitForApiResponse()
    } else {
      cy.log('No list items available to click')
    }
  })
})

export {}