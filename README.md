# SmartStock-Control - SmartInventory

## Overview
SmartStock-Control is a comprehensive inventory management system designed to streamline and optimize inventory-related processes. The project encompasses three main components: Access Management, Product and Category Management, and Inventory Transaction Tracking.

### Access Management
The Access module facilitates user authentication and access control within the system. Users must provide essential information, including their name, email, mobile number, and password, to create an account. Email validation and password confirmation are implemented to ensure data integrity and security.

#### Access Model
- `Id`: Unique identifier for each user.
- `Name`: Full name of the user.
- `EmailId`: Email address used for account registration and communication.
- `Mobile`: Mobile number of the user.
- `Password`: Securely stored password for account access.
- `ConfirmPassword`: Field to confirm the entered password.

### Category and Product Management
This module allows users to categorize and manage products efficiently. Categories help organize products, and each product is associated with a specific category. Product creation includes details such as product name, description, category assignment, and creation date.

#### Category Model
- `Id`: Unique identifier for each category.
- `Name`: Name of the category.
- `Description`: Description of the category.
- `Products`: List of products associated with the category.

#### Product Model
- `Id`: Unique identifier for each product.
- `Name`: Name of the product.
- `Description`: Description of the product.
- `CategoryId`: Foreign key referencing the associated category.
- `Category`: Navigation property to access the associated category.
- `CreationDate`: Date when the product was created.

### Inventory Transaction Tracking
This module keeps track of inventory transactions, including additions, subtractions, rejections, and related details. Each transaction involves a specific product, quantity, price, transaction date, type, rejection status, rejection quantity, reason for rejection, and associated purchase and sales order numbers.

#### InventoryItem Model
- `Id`: Unique identifier for each inventory transaction.
- `ProductId`: Foreign key referencing the associated product.
- `Product`: Navigation property to access the associated product.
- `Quantity`: Quantity of items involved in the transaction.
- `Price`: Price per unit for the transaction.
- `TransactionDate`: Date when the transaction occurred.
- `TransactionType`: Type of transaction (e.g., Stock In, Stock Out).
- `isRejected`: Boolean indicating whether the transaction was rejected.
- `RejectedQty`: Quantity rejected in case of rejection.
- `ReasonForRejection`: Reason provided for rejecting the transaction.
- `PO`: Purchase order number associated with the transaction.
- `SO`: Sales order number associated with the transaction.

## Usage
SmartStock-Control provides a user-friendly interface for managing access, categories, products, and inventory transactions. Users can navigate through the system seamlessly to perform tasks such as adding products, categorizing items, and tracking inventory movements.

Feel free to explore and contribute to the SmartStock-Control project on GitHub to enhance its features and capabilities. Your feedback and collaboration are highly appreciated!

