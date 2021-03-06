# QuadernoNetAPI
.Net API Wrapper for Quaderno App (https://quaderno.io)

## Install
Compile the project and add the reference to the file QuadernoNetAPI.dll

## Setup
Remember to include the library
```csharp
using QuadernoNetAPI;
```
And before any other use you must initialize the wrapper like this:
```csharp
QuadernoBase.Init(<YOUR_API_KEY>, <YOUR_API_URL>);
```

## Testing connection
```csharp
QuadernoBase.Ping();   // Returns true (success) or false (error)
```

## Usage
Almost all the calls to the API are made with the methods: '_Find()_', '_Save()_' and '_Delete()_'.


### Contacts
#### Find contacts
Returns _false_ if request fails.
```csharp
List<QContact> contacts = QContact.Find();
QContact contact = QContact.Find(<ID_TO_FIND>);
```

#### Creating a contact and saving it
```csharp
QContact contact = new QContact
                {
                    ContactName = "John Doe",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@lost-company.com",
                    Kind = QContactKind.person
                };

contact.Save();                             // Throws an exception on error
```

#### Retrieving a contact, editing and saving it
```csharp
QContact contact = QContact.Find(<ID_TO_FIND>));
contact.Email = "john.doe@company.com";
contact.Save();                             // Throws an exception on error
```

#### Retrieving a contact and deleting it
```csharp
QContact contact = QContact.Find(<ID_TO_FIND>));
contact.Delete();                             // Throws an exception on error
```

### Invoices

#### Creating, saving and deliver an invoice
```csharp
QInvoice invoice = new QInvoice
{
	Currency = "EUR",
	IssueDate = DateTime.Now,
	//DueDate = DateTime.Now,
	PaymentMethod = QPaymentMethod.credit_card //When it's set it will register a payment and the Invoice state will be paid
};

QInvoiceItem item = new QInvoiceItem
{
	Description = "Coffee",
	UnitPrice = 3.2m,
	Quantity = 2
};

invoice.Items.Add(item);

invoice.Contact = new QContact
{
	FirstName = "Bruce",
	LastName = "Wayne",
	Kind = QContactKind.person,
	ContactName = "Batman",
	Email = "ceo@wayne-enterprises.com"
};

invoice.Save();

Console.WriteLine("The invoice you just created is available here: " + invoice.Pdf);

invoice.Deliver();
```
