# MVVM_App

How to set database Confirguration :
Change your postgres database configuration in this file : Repositories>RepositoryBase.
Go yo DBScripts folder and run all the queries present to create the respective tables needed.

Admin Setup:
After registering a new user, go to the userdetails table and edit the superuser column to 1. This will make that particular user as a Admin user.
Admin user can only be set through database.

Steps to follow (from admin side):
1 --> create a Department for the doctor
2 --> Go to add doctor and add the doctor name interlinking their respective department.
3 --> Update timings for adding doctors work hours per day.

Note : These steps are needed to be followed to book an appointment as the User side is dependent on these updates.
