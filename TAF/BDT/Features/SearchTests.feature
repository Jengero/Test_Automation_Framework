Feature: SearchTests
As a user
I want to use search
In order to find appropriate articles on given topics

Background: Setup before tests
Given navigation to EPAM Landing page
And I accept all cookies

@Smoke
Scenario: Landing page - Search panel - Check search query words in search result
	Given I click on Search field on Search Panel on Epam Landing page
	And I enter search query ("Automation")
	When I click on Find button on Search Panel on Epam Landing page
	Then first 5 search results contains search query in any place of the search results page

@Smoke
Scenario: Landing page - Search panel - Check title of the first search result
	Given I click on Search field on Search Panel on Epam Landing page
	And I enter search query ("Business Analysis")
	When I click on Find button on Search Panel on Epam Landing page
	And I click on the title of the first article on the search results page
	Then the title of the article coincide with first search result title on the article page

@Somke
Scenario: Landing page - Search panel - Check the number of search results
	Given I click on Search field on Search Panel on Epam Landing page
	And I enter search query ("Automation")
	When I click on Find button on Search Panel on Epam Landing page
	And I scroll to the 9 search result on the search results page
	And I scroll to the bottom of the search results page
	Then the number of search results is 20