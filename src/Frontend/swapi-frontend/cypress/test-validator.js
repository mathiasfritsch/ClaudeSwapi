#!/usr/bin/env node

/**
 * Test Validator Script
 * Validates Cypress test files for common issues without running them
 */

const fs = require('fs');
const path = require('path');

const testDir = path.join(__dirname, 'e2e');
const testFiles = fs.readdirSync(testDir).filter(file => file.endsWith('.cy.ts'));

console.log('🧪 Validating Cypress Tests...\n');

let totalTests = 0;
let issues = [];

testFiles.forEach(file => {
  const filePath = path.join(testDir, file);
  const content = fs.readFileSync(filePath, 'utf8');
  
  console.log(`📁 ${file}`);
  
  // Count describe blocks
  const describeMatches = content.match(/describe\s*\(/g) || [];
  console.log(`   📝 ${describeMatches.length} test suite(s)`);
  
  // Count it blocks
  const itMatches = content.match(/it\s*\(/g) || [];
  console.log(`   ✅ ${itMatches.length} test case(s)`);
  totalTests += itMatches.length;
  
  // Check for common patterns
  const hasBeforeEach = content.includes('beforeEach');
  const hasCyVisit = content.includes('cy.visit');
  const hasDataCy = content.includes('data-cy');
  const hasAssertions = content.includes('.should(');
  
  console.log(`   🔧 beforeEach: ${hasBeforeEach ? '✓' : '✗'}`);
  console.log(`   🌐 cy.visit: ${hasCyVisit ? '✓' : '✗'}`);
  console.log(`   🏷️  data-cy selectors: ${hasDataCy ? '✓' : '✗'}`);
  console.log(`   🎯 assertions: ${hasAssertions ? '✓' : '✗'}`);
  
  // Check for potential issues
  if (!hasBeforeEach && hasCyVisit) {
    issues.push(`${file}: cy.visit() without beforeEach setup`);
  }
  
  if (!hasDataCy && hasAssertions) {
    issues.push(`${file}: May be using unreliable selectors`);
  }
  
  // Check for specific patterns
  const hasWaitForApiResponse = content.includes('waitForApiResponse');
  const hasLoadingSpinner = content.includes('loading-spinner');
  
  console.log(`   ⏳ API waiting: ${hasWaitForApiResponse ? '✓' : '✗'}`);
  console.log(`   🔄 Loading states: ${hasLoadingSpinner ? '✓' : '✗'}`);
  
  console.log('');
});

console.log('📊 Summary:');
console.log(`   📁 ${testFiles.length} test files`);
console.log(`   ✅ ${totalTests} total test cases`);
console.log(`   ⚠️  ${issues.length} potential issues`);

if (issues.length > 0) {
  console.log('\n⚠️  Issues found:');
  issues.forEach(issue => console.log(`   - ${issue}`));
}

console.log('\n🎯 Test Categories Covered:');
const categories = ['navigation', 'people', 'films', 'planets', 'species', 'starships', 'vehicles'];
categories.forEach(category => {
  const hasTest = testFiles.some(file => file.includes(category));
  console.log(`   ${hasTest ? '✅' : '❌'} ${category}`);
});

console.log('\n🚀 To run tests when frontend is running:');
console.log('   npm run cypress:open    # Interactive mode');
console.log('   npm run cypress:run     # Headless mode');
console.log('   npm run e2e             # Quick headless run');

console.log('\n💡 Recommendations:');
console.log('   1. Start frontend: npm start');
console.log('   2. Start backend API on port 5001');
console.log('   3. Run tests: npm run cypress:open');
console.log('   4. Use mocked tests for CI/CD: *-mocked.cy.ts');