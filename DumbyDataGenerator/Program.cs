//DEVELOPER NOTES
//needs to be a function like loop that automatically stays within bounds.
//needs to be a function for constant
//function for dates

using System;
using System.Data;
using System.IO;

class Program
{
    //rand
    static Random rand = new Random();

    static void Main()
    {
        // === READ ME ===

        // Input files should be under "InputText" folder under DumbyDataGenerator
        // Output files will appear or be modified under DumbyDataGenerator

        // WriteData() Parameter Format (ignore things in brackets when entering):
        // "OutputFileName",
        // "TableName",
        // ["ColumnName1", "ColumnName2", "ColumnName3", "ColumnName4"],
        // ["InputFileName" (gets a random line from the file), "!R<RandMin>-<RandMax>" (gets a random number), "!L<LoopAmount>" (repeats the id), "!A" (auto increments)],
        // NumberOfLines

        // Input Types
        // !R<min>-<max> = Random number between <min> and <max>

        //write SQL
        // CHANGE / ADD THESE FUNCTIONS TO GENERATE STATEMENTS
        //WriteData("range_approved_by_staff", "range_approved_by_staff", ["staff_id", "range_shot_id"], ["!L10", "!A"], 500);
        WriteData("staff", "staff", ["firstname", "lastname", "username", "password"], ["FirstName", "LastName", "Username", "!SPassword"], 200);

        //===============
        //Local Functions
        //===============

        //writes data to output SQL
        void WriteData(string _outputFileName, string _tableName, string[] _columnNames, string[] _inputPaths, int _lines)
        {
            //add rest of path on
            string path = @"..\..\..\" + _outputFileName + ".txt";

            //assert
            if (_columnNames.Length == 0 || _columnNames.Length != _inputPaths.Length)
            {
                Console.WriteLine("Error: Please input a correct WriteData instruction!");
                return;
            }

            //write everthing into arrays
            string[][] inputFiles = new string[_inputPaths.Length][];
            for (int i = 0; i < _inputPaths.Count(); i++)
            {
                //make filename string
                string fileName = "";

                //not if it's not a file
                if (_inputPaths[i][0] == '!')
                {
                    if (_inputPaths[i][1] == 'S')
                    {
                        //loop
                        int k = 2;
                        while (k < _inputPaths[i].Length)
                        {
                            fileName = fileName + _inputPaths[i][k];
                            k++;
                        }
                    }
                    else
                        continue;
                }
                else
                {
                    fileName = _inputPaths[i];
                }
                
                //set array
                inputFiles[i] = File.ReadAllLines(@"..\..\..\InputText\" + fileName + ".txt");
            }

            //clear output file
            File.WriteAllText(path, "");

            //streamwriter
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                //for every line
                for (int i = 0; i < _lines; i++)
                {
                    //everything is appended to line
                    string line = "";

                    //initial
                    line += "INSERT INTO ";
                    line += _tableName;
                    line += " (";

                    //column names
                    for (int j = 0; j < _columnNames.Length; j++)
                    {
                        //column name
                        line += _columnNames[j];

                        //comma
                        if (j < _columnNames.Length - 1)
                            line += ", ";
                    }

                    //values
                    line += ") VALUES (";

                    //values
                    for (int j = 0; j < _inputPaths.Count(); j++)
                    {
                        //value
                        if (_inputPaths[j][0] == '!')
                        {
                            if (_inputPaths[j][1] == 'A')
                            {
                                //auto increment
                                line += (i + 1 + 1015).ToString();
                            }
                            else if (_inputPaths[j][1] == 'L')
                            {
                                //loop
                                int k = 2;
                                string num = "";
                                while (k < _inputPaths[j].Length)
                                {
                                    num = num + _inputPaths[j][k];
                                    k++;
                                }

                                line += (Math.Floor((double)i / Convert.ToInt32(num)) + 1).ToString();
                            }
                            else if (_inputPaths[j][1] == 'R')
                            {
                                //write a random number
                                int k = 2;
                                string min = "";
                                while (_inputPaths[j][k] != '-')
                                {
                                    min = min + _inputPaths[j][k];
                                    k++;
                                }
                                k++;
                                string max = "";
                                while (k < _inputPaths[j].Length)
                                {
                                    max = max + _inputPaths[j][k];
                                    k++;
                                }
                                line += rand.Next(Convert.ToInt32(min), Convert.ToInt32(max) + 1).ToString();
                            }
                            else if (_inputPaths[j][1] == 'S')
                            {
                                //sha2 encryption
                                line += "SHA2(";

                                //loop
                                int k = 2;
                                string filename = "";
                                while (k < _inputPaths[j].Length)
                                {
                                    filename = filename + _inputPaths[j][k];
                                    k++;
                                }

                                //MUST FIX ABOVE, WHEN READING FILES INTO ARRAYS!!!
                                //write a rand line based off rest
                                line += '"';
                                string[] inputLines = inputFiles[j];
                                line += inputLines[rand.Next(0, inputLines.Count())];
                                line += '"';

                                line += ", 256)";
                            }
                        }
                        else
                        {
                            //write a normal, rand line
                            line += '"';
                            string[] inputLines = inputFiles[j];
                            line += inputLines[rand.Next(0, inputLines.Count())];
                            line += '"';
                        }

                        //comma
                        if (j < inputFiles.Count() - 1)
                            line += ", ";
                    }

                    //ending ine
                    line += ");";

                    //writer
                    writer.WriteLine(line);
                }
            }
            
            Console.WriteLine("Data written to: " + _outputFileName);
        }
    }
}
