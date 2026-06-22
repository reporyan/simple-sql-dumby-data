# Simple SQL Dumby Data Generator
Insert SQL statement generator developed as an assisting tool for the Database Design Project unit.<br>

## Usage (also commented inside the program)
Input files should be under "InputText" folder under DumbyDataGenerator<br>
Output files will appear or be modified under DumbyDataGenerator<br>

### WriteData() Parameter Format (ignore things in brackets when entering):<br>
"OutputFileName",<br>
"TableName",<br>
["ColumnName1", "ColumnName2", "ColumnName3", "ColumnName4", "ColumnName5"],<br>
["<InputFileName>" (gets a random line from the file), "!R<RandMin>-<RandMax>" (gets a random number), "!L<LoopAmount>" (repeats the id), "!A" (auto increments), "!S<InputFileName> (encrypted)],<br>
NumberOfLines<br>

### Examples
WriteData("range_approved_by_staff", "range_approved_by_staff", ["staff_id", "range_shot_id"], ["!L10", "!A"], 500);
WriteData("staff", "staff", ["firstname", "lastname", "username", "password"], ["FirstName", "LastName", "Username", "!SPassword"], 200);
