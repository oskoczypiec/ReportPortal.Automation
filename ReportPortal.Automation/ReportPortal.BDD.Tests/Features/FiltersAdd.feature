Feature: FiltersAdd

Background: 
	Given default user is logged in
	And filters page is opened

@regression @filters @add @ignore
Scenario Outline: User is able to add a filter
	Given user clicks add filter button
	And user clicks more button  
	When user select <name> filter 
	And user provides for filter <name> value <value>
	Then the row count should be <result>

	Examples:
	| name           | value | result |
	| Passed         | 30    | 1      |