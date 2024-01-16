Feature: FiltersDelete
Background: 
	Given default user is logged in
	And filters page is opened

@regression @filters @delete
Scenario Outline: User is able to delete a filter
	Given user clicks add filter button
	And user clicks more button  
	And user select <name> filter 
	And user provides for filter <name> value <value>
	And the row count should be <result>
	And user clicks save filter button
	And add filter modal is displayed
	And filter name is set to <name>
	And user clicks add filter button in modal
	And new filter <name> is saved
	When user clics remove active filter button
	Then no active filter is displayed

	Examples:
	| name           | value | result |
	| Passed         | 30    | 1      |
	| System issue   | 3     | 2      |
	| Description    | Demo  | 5      |
	| Failed         | 1     | 4      |
	| To investigate | 1     | 4      |