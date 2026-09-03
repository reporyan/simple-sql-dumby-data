# Simple SQL Dumby Data Generator
Insert SQL statement generator developed as an assisting tool for the Database Design Project unit.<br>
<img width="1153" height="575" alt="image" src="https://github.com/user-attachments/assets/456c4c47-4ddc-4450-afab-ebe734edd257" />

## Instalation
Clone or download
Open DumbyDataGenerator.sln using VisualStudio
Use solution explorer to navigate input and output files

## Usage
Input files should be under "InputText" folder under DumbyDataGenerator<br>
Output files will appear or be modified under DumbyDataGenerator<br>
View in-code comments for more guidance

### WriteData() Parameter Format:<br>
"OutputFileName",<br>
"TableName",<br>
["ColumnName1", "ColumnName2", "ColumnName3", "ColumnName4", "ColumnName5"],<br>
["<InputFileName>" (gets a random line from the file), "!R<RandMin>-<RandMax>" (gets a random number), "!L<LoopAmount>" (repeats the id), "!A" (auto increments), "!S<InputFileName> (encrypted)],<br>
NumberOfLines<br>

### Examples
WriteData("range_approved_by_staff", "range_approved_by_staff", ["staff_id", "range_shot_id"], ["!L10", "!A"], 500);
WriteData("staff", "staff", ["firstname", "lastname", "username", "password"], ["FirstName", "LastName", "Username", "!SPassword"], 200);
