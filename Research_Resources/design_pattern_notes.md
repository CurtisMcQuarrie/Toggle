# Design Patterns
## Design Patterns
- typical solutions to common problems.
- a customizeable blueprint.
- 22 classic design patterns.
- not a specific piece of code, but a general concept.
- components:
	1. intent
		- briefly describes both the problem and the solution.
	2. motivation
		- further explains the problem and the solution the pattern makes possible.
	3. structure
		- describes classes with each part of the pattern and how they are related.
	4. code example

## Pattern Classification
1. creational patterns
	- provide object creation mechanisms that increase flexibility and resue of existing code.
2. structural patterns
	- explain how to assemble objects and classes into larger structures, while keeping the structures flexible and efficient.
3. behavioural patterns
	- take care of effective communication and the assignment of responsibilities between objects.
		
## Singleton (https://refactoring.guru/design-patterns/singleton)
a) intent
	- creational design pattern.
b) problem
	- solves 2 problems:
		1. ensure that a class has just a single instance.
		2. provide a global access point to that instance.
c) solution
	- make the default constructor private.
	- create a static creation method that acts as a constructor.
		- method calls the private constructor to create an object and saves it in a static field.
d) real-world analogy
	- the government.
	- a country can have only one official government.
		
## Factory Method (https://refactoring.guru/design-patterns/factory-method)
a) intent
	- creational design pattern.
	- provides an interface for creating objects in a superclass.
	- allows subclasses to alter the type of objects that will be created.
b) problem
	- example is a logistics management app that handles trucks.
	- imagine they want your app to handle ships too, but the code is coupled to trucks.
		- imagine now that they want your app to handle other forms of transportation too.
c) solution
	- replace direct object construction calls with calls to a special factory method.
		- e.g. Transport t = createTransport();
	- all subclasses being instantiated (the products) need to implement an interface to ensure they have similar methods.
d) structure
	- product
		- declare the interface common to all objects the Creator class and subclasses produce.
	- concrete products
		- different implementations of the product interface.
	- creator
		- declares the factory method that returns new product objects.
		- may be declared as abstract.
	- concrete creators
		- override the base factory method so it returns a specific type of product.
e) applicability
	1. use when you don't know beforehand the exact types and dependencies of the objects your code should work with.
	2. use when you want to provide users of your library or framework with a way to extend its internal components.
	3. use when you want to save system resources by reusing existing objects instead of rebuilding them each time.
f) possible connection
	- may be used in object spawners.
	
## Observer (https://refactoring.guru/design-patterns/observer)
a) intent
	- behavioural design pattern.
	- defines a subscription mechanism to notify multiple objects about any events that happen to the object they're observing.
b) problem
	- 2 types of objects (Customer and Store)
	- Customer could visit the Store every day to see if the product they want is finally in stock.
	- Store could spam emails to all customers each time a new product become available.
c) solution
	- object that has some interesting state is called subject.
		- it notifies others about changes to its state.
		- aka publisher.
	- objects that track changes to the publisher's state are called subscribers.
	- subscription mechanism is added to the publisher class.
		- provides functionality for objects subscribe or unsubscribe to it.
	- all subscribers must implement the same interface.
		- publisher communicates with them only via that interface.
d) subscription mechanism components
	1. array field for storing a list of references to subscriber objects.
	2. several public methods that allow adding subscribers  to and removing them from that list.
