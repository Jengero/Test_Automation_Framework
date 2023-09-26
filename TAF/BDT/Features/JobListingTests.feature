Feature: JobListingTests
As a applicant
In order to find suitable vacancies
I want to use filters on the Job listings page

Background: Setup before tests
Given navigation to EPAM Landing page
And I accept all cookies

@tag1
Scenario: Job Listing - Filtering Panel - Check vacancies with relocation
	Given I open Careers dropdown on Epam Landing page
	And I click on Join Our Team button on Careers dropdown on Epam Landing page
	When I click on Open to Relocate checkbox on Join our Team page
	And I scroll for more results
	Then All 20 vacancies contains Open to Relocate icon