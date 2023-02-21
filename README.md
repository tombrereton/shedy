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

# Comparison of Scheduling Services

## Calendly
- From small businesses to Fortune 100 companies, 
millions of people around the world rely on Calendly to close deals, 
land candidates, build relationships, and grow their business—faster.
- good pricing but limited to 1 event type, 1 calendar, and 1 calendar sync

## Honey Book
- An excellent service which targets freelancers
- They provide a calendar which you can share
- Their monthly subscription is expensive, starting at $39 US / month
- They take a fee for transactions

## TimeKit
- Targeted at Web Developers and tech savvy organisations
- Amazing API support and documentation
- Very expensive for API access, $149 US / month

## Shedy
- Targeted at web developers and software engineers!
- API first, minimalist scheduling service
- Cheaper than all the above
- Free tier with 100 event types, 2 calendar syncs, 100 users and calendars
- Shedy assumes that if you have more than 5 bookings per month you can afford to pay!
- Shedy limits free tier to 5 bookings, anything over clients will see bookings but when they
click to book it's says fully booked. You get an email asking to pay $5 to allow booking
- Next tier starts at only $10 AUD / month, with unlimited bookings
