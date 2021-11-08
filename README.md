# Siccar Code Test

We want you to build a very basic ASP.NETCore API. The API will take in Vehicles in JSON and calculate the total amout of tax to pay per year.
We have given you a partial solution where you fill in the blanks. Do not change the tests. We recommend using Visual Studio as your IDE.

## Requirements

Given the client wishes to register their vehicle
When the vehicle is passed as a JSON object
Then the vehicle is stored and the totalTax is added to the vehicle
And any additional properties for that vehicle are returned

Given the client wishes to register multiple vehicles of different types
When a list of vehicles are sent as a request to the API (JSON)
Then each vehicle is stored and the total tax is added to each vehicle
And any additional properties for that vehicle are returned

Given the client wishes to get all currently registered vehicles
When they call get all registered vehicles
Then a list of registered vehicles is returned

See the postman tests for what properties each type of vehicle has.

- Search for the methods with TODO comments and implement functionality which allow the tests to pass
- You should ensure all the unit tests pass.
- You must also ensure that the Postman request tests run.
- Make sure Disable SSL certificate verification in postman.

## Additional Notes

- You can make any changes you wish in the code but do not change the tests. Please add your name as a comment against any changes you make.
- Ideally we want to see how you deal with a list of vehicles that can have different properties depending on the type.
- If you are struggling please email us any questions

## Submission

- Make sure all your tests pass in Visual Studio and pass in the Postman requests
- Please submit your solution via a personal Github repository and send us the link

## Bonus/Opitonal Requirements

- Run the service in Docker
