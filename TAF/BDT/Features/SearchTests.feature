Feature: SearchTests
As a user
I want to use search
In order to find appropriate articles on given topics

Background: Setup before tests
Given setup page objects for search tests
And navigation to epam.com page
And accept all cookies

@Smoke
Scenario: Search result contain search query words
	Given I click to search field
	And I enter search query ("Automation")
	When I click find result button
	Then first 5 search results contains search query in any place

@Smoke
Scenario: The title of the opened page is equal to the title of the first article on the search results page
	Given I click to search field
	And I enter search query ("Business Analysis")
	When I click find result button
	And I click to the title of the first article
	Then the title of the article coincide with first search result title

@Somke
Scenario: Search result page contain 20 search results
	Given I click to search field
	And I enter search query ("Automation")
	When I click find result button
	And I scroll to the 9 search result
	And I scroll to the bottom of the page
	Then the number of search results is 20