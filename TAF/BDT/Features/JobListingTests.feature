Feature: JobListingTests
As a applicant
In order to find suitable vacancies
I want to use filters on the Job listings page

Background: Setup before tests
Given setup page objects for job listing tests
And navigation to epam.com page
And accept all cookies

@tag1
Scenario: Check vacancies with relocation
	Given I navigate to Careers button
	And I click to Join Our Team button
	When I click to Open to Relocate button
	And I scroll for more results
	Then All 20 vacancies contains Open to Relocate icon