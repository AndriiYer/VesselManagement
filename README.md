### **Objective:**

Build a RESTful API in ASP.NET Core that manages vessel information using the CQRS pattern.

### **Scenario:**

A maritime logistics company needs an API to manage its fleet of vessels. The API should allow for registering new vessels, updating vessel details, and retrieving vessel information.

### **Requirements:**

**API Endpoints:**

- POST /api/vessels: Register a new vessel.
- PUT /api/vessels/{id}: Update an existing vessel.
- GET /api/vessels: Retrieve a list of all vessels.
- GET /api/vessels/{id}: Retrieve a specific vessel by ID.

**Vessel Model:**

- Id (Guid, primary key)
- Name (string, required)
- IMO (string, required, unique) - International Maritime Organization number
- Type (string, required) - e.g., "Cargo", "Tanker", "Passenger"
- Capacity (decimal, required) - e.g., tonnage

**CQRS Implementation:**

- **Commands:**
    - RegisterVesselCommand: Represents the action of registering a new vessel.
    - UpdateVesselCommand: Represents the action of updating an existing vessel.

- **Queries:**
    - GetAllVesselsQuery: Represents the action of retrieving all vessels.
    - GetVesselByIdQuery: Represents the action of retrieving a vessel by its ID.
    
- **Handlers:** Implement handlers for each command and query.

- **Data Store:** Use Entity Framework Core with an in-memory database (for simplicity).

### **Data Validation:**

- Implement data validation for required fields, data types, and uniqueness (IMO).

### **Error Handling:**

- Return appropriate HTTP status codes and error messages for invalid requests and database errors.

**MediatR (Recommended):**

- Use MediatR or a similar library to simplify the implementation of the CQRS pattern.

### **Clean Code Practices:**

- Write clean, readable, and maintainable code.
- Use meaningful variable and method names.
- Follow SOLID principles where applicable.

### **Testing (Optional but Recommended):**

- Write basic unit tests for the command and query handlers
