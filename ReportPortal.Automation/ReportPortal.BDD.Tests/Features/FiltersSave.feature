Feature: FiltersSave

Background: 
	Given default user is logged in
	And filters page is opened

@regression @filters @save @ignore
Scenario Outline: User is able to save a filter
	Given user clicks add filter button
	And user clicks more button  
	And user select <name> filter 
	And user provides for filter <name> value <value>
	And the row count should be <result>
	When user clicks save filter button in modal
	And add filter modal is displayed
	And filter name is set to <name>
	And user clicks add filter button
	Then new filter <name> is saved

	Examples:
	| name           | value | result |
	| Passed         | 30    | 1      |
	| System issue   | 3     | 2      |
	| Description    | Demo  | 5      |
	| Failed         | 1     | 4      |
	| To investigate | 1     | 4      |