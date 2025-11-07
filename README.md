# Contact Catalogue

A C# console application that demonstrates how to manage a simple contact list using object-oriented programming, collections, and LINQ.
This project was built as part of my software development studies to practice clean architecture and data handling in .NET.

## Features
- Add new contacts with name, email and tags
- Display all stored contacts with or without filter
- Search contacts by name or email
- Validate user input (empty fields, duplicates, etc.)
- Store data in memory using C# collections
- Export stored data as .CSV
  
## How to Run
1. Clone the repository 
    - git clone https://github.com/yourusername/contact-catalogue.git
2. Navigate to the folder
    - cd contact-catalogue
3. Run the program
    - dotnet run

## Example Run
### Start
<img width="466" height="342" alt="image" src="https://github.com/user-attachments/assets/8ed276a6-2786-42e3-8cce-27559f98625e" />

### [1] Add
1. Enter name: ex *Alice Johnson*
3. Enter email: ex. *alice@email.com*
5. Enter tags: ex. *Friend,Work*
<img width="466" height="322" alt="image" src="https://github.com/user-attachments/assets/1b4ddb8d-30dc-4209-9524-1e1d032a3821" />
      
### [2] List
1. Lists all added contact
<img width="466" height="324" alt="image" src="https://github.com/user-attachments/assets/6c4ee270-b50d-418a-aafc-3d1f0b70ec91" />

### [3] Search by name or email
1. Enter name or email: ex *Alice*
2. Lists all found contacts
<img width="466" height="324" alt="image" src="https://github.com/user-attachments/assets/9aff2785-c570-42e2-97dd-8b46066690a3" />

### [4] Search by name
1. Enter name: ex *Alice*
2. Lists all found contacts with name
<img width="466" height="324" alt="image" src="https://github.com/user-attachments/assets/273dd845-83a2-4595-b21d-a0780389364f" />

### [5] Search by email
1. Enter email: ex *Alice*
2. Lists all found contacts with email
<img width="466" height="324" alt="image" src="https://github.com/user-attachments/assets/c075d178-d137-4f8e-90e0-79b970a644be" />

### [6] Filter by tag
1. Enter tag: ex *Alice*
2. Lists all found contacts with tag
<img width="466" height="324" alt="image" src="https://github.com/user-attachments/assets/284ef384-1185-450e-83ce-7c5c037e85af" />

### [7] Export CSV
1. Enter name of .csv to create: ex *contacts.csv*
2. Exports a csv file into folder instructed
<img width="466" height="324" alt="image" src="https://github.com/user-attachments/assets/e7214d81-5a08-467a-8515-523e22e0b11d" />

### [0] Exit
1. Closes the program

## Purpose
The goal of this project is to:
- Strengthen my understanding of classes, objects, and properties
- Work with generic collections such as List and Dictionary
- Implement basic validation, data filtering using LINQ and testing with xUnit and Moq
- Apply the Repository and Service pattern for clean code organization

## Project Structure
- Exceptions: Custom exceptions for validation (Duplicate and Invalid email)
- Models: Defines the Contact class
- Repositories: Handles contact storage, retrieval and filewriter interface
- Services: Contains business logic (add/search/delete/filewrite)
- Validators: Contains validations (email)
- Menu.cs: Contains console user interface
- Program.cs: Entry point

## Technologies Used
- C#
- .NET 8 Console Application
- Generic Collections (List, Dictionary)
- LINQ for filtering and searching
- xUnit, Moq and logger testing

## Possible Improvements
- Add file or database storage (JSON, SQLite, etc.)
- Create a menu-driven undo/redo system
- Validate Phone number formats and names with regular expressions
- Build a simple GUI or web front-end

## About the Developer
*Hi! I’m Adchariya Changtam, a software development student passionate about game dev, backend programming, C#, and system design.
This project is part of gaining experience to become a great full-stack developer.*

GitHub: https://github.com/MJA0
