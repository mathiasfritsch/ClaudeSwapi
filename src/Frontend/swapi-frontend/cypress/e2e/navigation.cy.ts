describe('SWAPI Navigation Tests', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  it('should display the main navigation bar', () => {
    cy.get('[data-cy="nav-people"]').should('be.visible').and('contain', 'People')
    cy.get('[data-cy="nav-films"]').should('be.visible').and('contain', 'Films')
    cy.get('[data-cy="nav-planets"]').should('be.visible').and('contain', 'Planets')
    cy.get('[data-cy="nav-species"]').should('be.visible').and('contain', 'Species')
    cy.get('[data-cy="nav-starships"]').should('be.visible').and('contain', 'Starships')
    cy.get('[data-cy="nav-vehicles"]').should('be.visible').and('contain', 'Vehicles')
  })

  it('should navigate to People by default', () => {
    cy.url().should('include', '/people')
    cy.get('[data-cy="nav-people"]').should('have.class', 'active')
  })

  it('should navigate to all categories from navigation bar', () => {
    const categories = ['films', 'planets', 'species', 'starships', 'vehicles']
    
    categories.forEach(category => {
      cy.get(`[data-cy="nav-${category}"]`).click()
      cy.url().should('include', `/${category}`)
      cy.get('mat-card').should('be.visible')
    })
  })

  it('should show active navigation state', () => {
    cy.get('[data-cy="nav-films"]').click()
    cy.get('[data-cy="nav-films"]').should('have.class', 'active')
    cy.get('[data-cy="nav-people"]').should('not.have.class', 'active')
  })
})