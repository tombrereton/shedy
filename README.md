# Shedy
Shedy is an api to book meetings with a freelancer or organisation.

Api use cases:
1. Get available times for organisation
2. Create meeting with organisation
3. Update meeting with organisation
4. Delete meeting with organisation

Organisation use cases:
1. Login as Admin to account
2. Login as User to account
3. Login as Consultant to account
4. Set opening hours for meetings
5. Set time off for user
6. Get Api Key when logged in as Admin
7. Refresh Api Key when logged in as Admin

Background use cases:
1. Sync with Google Calendar
2. Sync with Microsoft Calendar

# Application Architecture
Shedy uses a combination of DDD and Clean Architecture.

The `Core` project contains the domain and application
but we will minimise external dependencies. Some basic nuget
packages will be installed to help with following
design patterns such as CQRS. For example, `Mediatr` 
`FluentValidation`.

We make the use of Aggregates to ensure business 
rules are always correct.

The `Api` project exposes the `Core` project to the web 
and is the interface for external users to interact
with the main part of the application. We keep this layer
this as possible and defer most of the logic to the `Core`.

The `Infrastructure` is where we keep the logic that 
interacts with data. This will contain the code for
persistence and any other external data calls. 