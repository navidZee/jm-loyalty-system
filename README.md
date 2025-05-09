🚀 Step 1: Getting Started

Step 1 : Run the following command:
```
docker compose up
```
🌐 Step 2: System Overview
This system exposes 3 main endpoints under the CustomerPoints API group:

1. GET /api/customers/balance
Description: Returns the current points balance for the authenticated customer.

Method: GET

2. POST /api/customers/earn
Description: Apply earned points to a customer account based on activity.

Method: POST

3. POST /api/customers/spent
Description: Deduct points from the customer balance when an offer is redeemed.

Method: POST

All endpoints are protected and require authentication via Keycloak.

📝 Notes

Ensure Docker and Docker Compose are installed before starting.

Register a user via Keycloak before using the endpoints. => OAuth2, implicit . client_id: my-public-client

ActivityId: 1 , OfferId: 1 , ReferenceId : AnyNumber
