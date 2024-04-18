[![Motorcycle App Logo](https://private-user-images.githubusercontent.com/83993524/323592070-ce3534a6-f6a9-4db3-9dd8-0a8fd27241b1.svg?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MTM0NDE3NDMsIm5iZiI6MTcxMzQ0MTQ0MywicGF0aCI6Ii84Mzk5MzUyNC8zMjM1OTIwNzAtY2UzNTM0YTYtZjZhOS00ZGIzLTlkZDgtMGE4ZmQyNzI0MWIxLnN2Zz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNDA0MTglMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjQwNDE4VDExNTcyM1omWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTEwYThiODA4YTY3ZDRlYmMxMTU5NDhlNjg1ZGQxNjJkMjc5YjVjMWMyNDU2MjdkZDU0MTVkYjRjOGM1OTUxNzcmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0JmFjdG9yX2lkPTAma2V5X2lkPTAmcmVwb19pZD0wIn0.wmrLgHN7b_-rEJwexGgU62kgCcqD2Jf3NQ8sRcvnHMk)](https://github.com/octobrainit/motorcycle/actions)

# Motorcycle App

The Motorcycle application was created to manage motorcycle rentals and delivery drivers.

## Instructions

The challenge is valid for various levels, so don't worry if you can't solve it completely. The application will only be evaluated if it is running; if necessary, create a step-by-step guide for this.

Clone the repository to your personal git to start development and do not mention anything related to Mottu.

After completion, send an email to the recruiter informing the repository for analysis.

## Non-functional requirements

- The application must be built with .Net using C#.
- Use only the following databases (Postgress, MongoDB).
- Choose your preferred messaging system (RabbitMq, Sqs/Sns, Kafka, Google Pub/Sub, or any other).

## Application to be developed

Your goal is to create an application to manage motorcycle rentals and delivery drivers. When a delivery driver is registered and with an active rental, they can also make deliveries of available orders on the platform.

### Use cases

1. **As an admin user, I want to register a new motorcycle.**
   - The mandatory data for the motorcycle are Identifier, Year, Model, and Plate.
   - The plate is a unique data and cannot be repeated.

2. **As an admin user, I want to view existing motorcycles on the platform and filter by plate.**

3. **As an admin user, I want to modify a motorcycle by only changing its improperly registered plate.**

4. **As an admin user, I want to remove a motorcycle that was incorrectly registered, provided it has no rental records.**

5. **As a delivery driver, I want to register on the platform to rent motorcycles.**
   - The delivery driver's data is: identifier, name, CNPJ, date of birth, CNH number, type of CNH, CNH image.
   - The valid CNH types are A, B, or both A+B.
   - The CNPJ is unique and cannot be repeated.
   - The CNH number is unique and cannot be repeated.

6. **As a delivery driver, I want to send a photo of my CNH to update my registration.**
   - The file format must be PNG or BMP.
   - The photo cannot be stored in the database; you can use a storage service (local disk, Amazon S3, MinIO, or others).

7. **As a delivery driver, I want to rent a motorcycle for a period.**
   - The available rental plans are:
     - 7 days at a cost of $30.00 per day
     - 15 days at a cost of $28.00 per day
     - 30 days at a cost of $22.00 per day
     - 45 days at a cost of $20.00 per day
     - 50 days at a cost of $18.00 per day
   - The rental must have a start date, an end date, and another predicted end date.
   - The start of the rental is mandatory on the first day after the creation date.
   - Only delivery drivers qualified in category A can rent.

8. **As a delivery driver, I want to inform the date I will return the motorcycle and check the total rental value.**
   - When the date entered is earlier than the predicted end date, the daily rates and an additional fine will be charged:
     - For a 7-day plan, the fine is 20% of the value of the unused daily rates.
     - For a 15-day plan, the fine is 40% of the value of the unused daily rates.
   - When the date entered is later than the predicted end date, an additional charge of $50.00 per additional day will be charged.

### Used Libraries

The application was written in .NET using the following libraries:

- FluentValidation for facilitating fast-fail validation
- MediatR used for decoupling
- NewtonsoftJson for JSON serialization and deserialization
- Npgsql Driver for using EntityFramework and Postgres
- WindowsAzure.Storage for local storage testing purposes
- xUnit for domain unit tests

### Design Principles and Architecture

The application was inspired by the use of Rich Domains according to Domain-Driven Design, addressing the concepts of SOLID, as classes are self-contained and validated, in addition to their isolated behaviors.

We created baseErrors for handling exceptions. In the exposure context, from the outermost layer of the application, we used minimal APIs following the Clean Architecture model, where folders are separated by features.

We are using a Redis cache to temporarily store images that may be processed and saved in a blob, where all this processing is done via notifications, coming from the Mediator pattern implemented by the MediatR library.

## Running the Application

To run the application, simply execute the command `docker-compose up -d --build`, where the entire environment will be provisioned locally. There is also a development container provided if you want to develop on any platform as long as you have Docker installed on your machine, in addition to VS Code with the Remote-dev extension, containing Dev-containers that provide development within the container.

