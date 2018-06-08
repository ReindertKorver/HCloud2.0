# HCloud2.0
School Project made in ASP.net
A healtcare management system.<br/>
<h3>Changelog:</h3>
. Cloud C# ASP.Net Webforms Application version 1.8.5
version 1.8.5:
<pre
*design .aspx and codebehind.cs files
site.master
	-Home 
	-SignIn
	-RegisterNew 
	-Error
	-Contact 
	-Account 
	-Portal.master (all files are in directory "PortalContent")
		-Agenda 
			users own agenda
		-AllDeseases 
			all deseases in the application<br/>
		-AllFiles 
			all files in the application sorted per user<br/>
		-AllMedications 
			all medications in the application<br/>
		-AllRapports 
			all rapports in the application [Deprecated]->Rapport<br/>
		-AllTherapies 
			all therapies in the application
		-Dashboard (currently not in use)
		-Files 
			files of the user
		-Management
			manage users, rights and roles
		-NewDesease 
			make a new desease depending on the role ShowAllDeseases you can add it to all the users or to your clients<br/>
		-NewFile 
			upload a new file to a user<br/>
		-NewMedication 
			make a new medication depending on the role ShowAllMedications you can add it to all the users or to your clients
		-NewTherapy
			make a new therapy depending on the role ShowAllTherapies you can add it to all the users or to your clients
		-OwnDesease
			users own deseases
		-OwnMedication
			users own medications
		-OwnRapport			[Deprecated]->Rapport
			users own rapports
		-OwnTherapy
			users own therapies
		-Rapport
			Displays a summary of the therapies with a filter and search option

*BLL Bussiness Logic Layer (contains most of the logic code) .cs files
	-ClientHelper
	-DeseaseHelper
	-LogInHelper
	-MedicationHelper
	-Security

*DAL  Data Access Layer (contains most of connections to databases)
	-DBAgendaConnection
	-DBConnectionString
	-DBDeseaseConnection
	-DBFileConnection
	-DBMedicationConnection
	-DBRapportConnection
	-DBRoleConnection
	-DBTestConnection
	-DBTherapyConnection
	-DBUserConnection

*Entities Objects and classes
	-User
	-AgendaClass
	-AgendaRow
	-DBCredentials (the credentials which the application uses to connect to da database)
	-Desease
	-Medication
	-File
	-Rapport
	-Role
	-Therapy

*Additional information
-H.Cloud is made with Bootstrap
</pre>
Made by Reindert Korver
