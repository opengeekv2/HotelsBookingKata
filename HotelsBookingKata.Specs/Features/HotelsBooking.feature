Feature: HotelsBooking
	Booking hotels for employees following company rules

@mytag
Scenario: Booking without company or employee rules
	Given an hotel with id "37750641M" and name "Hotel 1"
	And a room for hotel with id "37750641M", number 101 and room type "Double"
	And an employee of company "59500657W", and employee id "95080440G"
	When the employee "95080440G" books the room type "Double" on hotel "37750641M" from "25-5-2024" to "27-5-2024"
	Then the result should complete a booking and return confirmation for the employee "95080440G" books the room type "Double" on hotel "37750641M" from "25-5-2024" to "27-5-2024"