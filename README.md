# Resturant Management Project

## Paolo Napoli & Ludovico Sforzo

## Task
- FrontEnd Client & Admin: Ludovico&Paolo
- Unit Testing: Ludovico
- Controllers FileManager: Ludovico&Paolo
- Model Class: Ludovico&Paolo
- Methods: Ludovico&Paolo

We pretty much worked on the same general task, helping eachother and fixing issues the other couldn't handle (or didn't notice).

The program is divided by two interfaces: client and admin side. Both have different feature, depending on the task and the person.
For the Admin a login is asked to enter the application, only employee can acces the program. Meanwhile the client has to insert his personal informations to access the program, so the program can assign a order or reservation to a customer.

## Classes
Employee and Customer are extension of the class Person that contain all the personal informations.
A class Dish describe the name, price and ingredients of a dish, it's strongly bounded by the IngredientManager class which contains all the ingredients avaiable.
The class Reservation keep the customer email to make a reservation for the Class Table, every customer can make multiple orders that will be added to his check Class check.
We added a class MenuUtils in our Resturant Library that contains methods that we re-used a lot in our program. Specific methods were added in the subMenu of our frontend

## Task not implemented
We tried our best to implement all the task, however the project was more complex that expected. This are the tasks we weren't able to complete in time
- Supplies Manager.
- Print all Orders.
- Reports and Statistic.