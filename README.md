# Exhibit (Exhibition Management System)

Introducing Exhibit, the ultimate solution for seamless event planning. Exhibit allows you to easily search for and book a wide range of events, from concerts to conferences and everything in between. With a user-friendly interface and real-time updates, you can browse upcoming events, check availability, and make reservations all in one place. Whether you're planning a night out with friends or a corporate event, Exhibit app has you covered.

# How to use
First, make sure SQL Server Management Studio (SSMS) and Microsoft Visual Studio are installed. The database must be extracted using SSMS from the "Database Backup" folder. After that, you must change the Desktop name in the cs files to reflect the name of your desktop or laptop. 

For example: 
<code>con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");</code>

replace <code>"DESKTOP-TGP1F01"</code> with the name of your desktop in specific files

## Softwares used: <img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white"> <img src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white"> <img src="https://img.shields.io/badge/Adobe%20Photoshop-31A8FF?style=for-the-badge&logo=Adobe%20Photoshop&logoColor=black">

## Exhibit (an event management system)
- Admin
- Host
- Guest


### Login Page
- Different user types can be directed to their own dashboard from the login page.
 
![image](https://github.com/tahsinhasib/Exhibition_Management_System/assets/99963332/db47668c-9016-49c1-be1f-2bdf94a77ce0)

### Create Account
- Verification of real-time username, password, email, and phone. The app checks the database and compares it with the information entered by the user each time to determine if the input is available or unavailable.

![image](https://github.com/tahsinhasib/Exhibition_Management_System/assets/99963332/42d4f9c9-9af8-4e15-8a02-e66b1beb2791)

### Reset Password
- Exhibit allows their users to reset account password if they forget.

![image](https://github.com/tahsinhasib/Exhibition_Management_System/assets/99963332/d6958ad5-c8a2-4201-9b75-06901a9b7ebf)

### Admin Dashboard
- The app's administrator has access to a number of capabilities, including accessing their own information, that of users, adding, removing, and modifying venues, as well as viewing payments and user ratings.

![image](https://github.com/tahsinhasib/Exhibition_Management_System/assets/99963332/df2e4136-0d08-437f-a048-9136338c8d28)

### Host Dashboard
- Event hosts can view their profile, plan gatherings based on particular locations. They may also decide on admission costs, the cost of food and refreshments, and the number of attendees.

![image](https://github.com/tahsinhasib/Exhibition_Management_System/assets/99963332/47a2adf7-bd78-4bdb-9dba-3a57e87c3a9d)

### User Dashboard
- Users can view information about their profiles. They can look for events to attend, rate about their experience.

![image](https://github.com/tahsinhasib/Exhibition_Management_System/assets/99963332/be6e9bc9-db88-4ad4-91c1-decc31de803e)






