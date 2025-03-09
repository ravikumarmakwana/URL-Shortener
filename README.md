# URL Shortener

## Overview

The URL Shortener is a system designed to create shortened versions of long URLs, facilitating easy sharing and reducing the likelihood of mistyped links. When users access these shortened URLs, they are seamlessly redirected to the original, longer URLs.

## Features

- **URL Shortening**: Generate unique, shortened aliases for long URLs.
- **Redirection**: Redirect users from the shortened URL to the original URL.
- **Link Expiry**: Set default expiration times for links to ensure they are temporary.
- **High Availability**: Ensure the service is consistently available with minimal latency.
- **Scalability**: Efficiently handle increasing numbers of users and requests.
- **Security**: Maintain a secure environment for both users and data.
- **Analytics**: Record and analyze metrics for each redirection to monitor usage patterns.

![Login SignUp](https://github.com/user-attachments/assets/2bf14cd9-c64a-4fc5-8912-1fb4bb632961)
![main](https://github.com/user-attachments/assets/254c1145-6390-4dd3-adf0-c9029c3eeca2)
![chrome_UpK95qWai0](https://github.com/user-attachments/assets/fc224ebd-1aa4-44a0-bd13-f8513b188c47)
![chrome_MDjuzX968B](https://github.com/user-attachments/assets/57dd5c4b-9b36-4a9d-bb9f-38ff01f9682b)

## System Architecture

The system employs a microservices architecture, dividing the application into smaller, manageable services to enhance availability and fault tolerance:

1. **UserService**: An ASP.NET Core v8 API responsible for user registration and authentication.
2. **URLService**: An ASP.NET Core v8 API that handles URL shortening, analytics computation and provides access to shortened URLs.
3. **APIGateway**: An ASP.NET Core v8 API serving as the central point of communication between the UI and backend services, managing authentication and routing.
4. **URL-Shortener**: An Angular 19 Single Page Application (SPA) offering a user interface for login, signup, URL shortening, and viewing previously created URLs.
5. **URL-Shortener-Access**: An Angular 19 SPA dedicated to accessing long URLs based on their shortened counterparts, handling redirection seamlessly.

## Class Diagram
![Class Diagram 2](https://github.com/user-attachments/assets/1b50618a-6727-46bf-869f-0cab589f223d)
![Class Diagram 1](https://github.com/user-attachments/assets/d5bdcada-543d-43db-a841-601c5e73bb01)

## API Design

### 1. Register User
- **Route**: `/Users/Register`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "FirstName": "Ravi",
    "LastName": "Makwana",
    "Email": "ravikumar.m@gmail.com",
    "PhoneNumber": "",
    "UserName": "URL",
    "Password": "URL@1234"
  }
  ```
- **Response Body**:
  ```json
  {
    "FirstName": "Ravi",
    "LastName": "Makwana",
    "Email": "ravikumar.m@gmail.com",
    "PhoneNumber": "",
    "Message": "User Created Successfully"
  }
  ```
- **Status Codes**: `201 Created`, `400 Bad Request`

### 2. User Login
- **Route**: `/Authenticate`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "UserName": "URL",
    "Password": "URL@1234"
  }
  ```
- **Response Body**:
  ```json
  {
    "AccessToken": "",
    "RefreshToken": "",
    "UserName": "URL",
    "FirstName": "Ravi",
    "LastName": "Makwana",
    "Email": "ravikumar.m@gmail.com",
    "PhoneNumber": ""
  }
  ```
- **Status Codes**: `200 OK`, `401 Unauthorized`

### 3. Regenerate Access Token
- **Route**: `/Authenticate/RefreshToken`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "RefreshToken": ""
  }
  ```
- **Response Body**:
  ```json
  {
    "AccessToken": "",
    "RefreshToken": ""
  }
  ```
- **Status Codes**: `200 OK`, `400 Bad Request`

### 4. Generate Shortened URL
- **Route**: `/URLs/Shorten`
- **Method**: `POST`
- **Headers**:
  ```
  Authorization: Bearer {AccessToken}
  ```
- **Request Body**:
  ```json
  {
    "LongURL": ""
  }
  ```
- **Response Body**:
  ```json
  {
    "LongURL": "",
    "ShortenURLPath": "",
    "ExpiredOn": ""
  }
  ```
- **Status Codes**: `201 Created`, `400 Bad Request`

### 5. Fetch All Shortened URLs for Logged-In User
- **Route**: `/URLs`
- **Method**: `GET`
- **Headers**:
  ```
  Authorization: Bearer {AccessToken}
  ```
- **Response Body**:
  ```json
  [
    {
      "LongURL": "",
      "ShortenURLPath": "",
      "ExpiredOn": ""
    }
  ]
  ```
- **Status Codes**: `200 OK`, `401 Unauthorized`

### 6. Access Shortened URL
- **Route**: `/URLs/Access?shorten={shorten}`
- **Method**: `GET`
- **Response**: Redirects to the original long URL.
- **Status Codes**: `200 OK`, `404 Not Found`

### 7. View URL Analytics
- **Route**: `/Analytics/{urlId}`
- **Method**: `GET`
- **Headers**:
  ```
  Authorization: Bearer {AccessToken}
  ```
- **Response Body**:
  ```json
  [
    {
      "LongURL": "",
      "ShortenURLPath": "",
      "AccessedOn": ""
    }
  ]
  ```
- **Status Codes**: `200 OK`, `401 Unauthorized`

## Database
 - Structured Database: Microsoft SQL Server (MSSQL)

## Security & Token Workflow
![AccessToken](https://github.com/user-attachments/assets/a9772234-67be-4481-95bf-640cbbbf28a7)

1. User logs in via API Gateway.
2. API Gateway sends an authentication request to `UserService`.
3. `UserService` validates credentials and issues an access token.
4. API Gateway forwards the token to the user.
5. The user application stores the token and uses it for subsequent requests.
6. API Gateway verifies tokens and issues internal microservice JWT tokens.
7. `URLService` validates the internal token and processes requests.

## High-Level Design
![HLD1](https://github.com/user-attachments/assets/145f0de3-dbc4-4891-abc7-bc74e2a8d4c8)


## Performance & Scalability Enhancement

To ensure high availability and fault tolerance:
- Deploy multiple application instances.
- Use a suitable load balancer.
- Utilize Redis caching for performance improvements.
- Implement Azure Search for optimized search capabilities.
![HLD2](https://github.com/user-attachments/assets/2ef2f484-3ce2-49df-93c8-8f7ff51cda4a)

This system provides a robust, secure, and scalable solution for URL shortening.
