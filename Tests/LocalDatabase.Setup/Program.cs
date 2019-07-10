using System;

namespace LocalDatabase.Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }


        private static void Menu()
        {
            var db = new DatabaseAdmin();
            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("What do you wish to do?");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Setup the database connection.");
                Console.WriteLine("2. Create tables.");
                Console.WriteLine("3. Seed data into the tables.");
                Console.WriteLine("4. Delete data from tables. Requires confirmation.");
                Console.WriteLine("5. Delete tables. Requires confirmation.");
                Console.WriteLine("0. Exit.");

                var input = Console.ReadKey();
                try
                {
                    Console.WriteLine();
                    switch (input.KeyChar)
                    {
                        case '1':
                            Console.WriteLine("Executing the database connection setup...");
                            db.CheckConnection();
                            Console.WriteLine("Done. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.WriteLine("Executing the tables creation...");
                            db.AddTables();
                            Console.WriteLine("Done. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case '3':
                            Console.WriteLine("Executing the data seed into tables. It may take some time...");
                            db.SeedTestData();
                            Console.WriteLine("Done. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case '4':
                            var ch = AskConfirmation("WARNING: You are about to delete the entire data from the tables. Type {0} and then press enter to continue...");
                            if (ch)
                            {
                                Console.WriteLine("Executing the data deletion...");
                                db.DeleteData();
                                Console.WriteLine("Done. Press any key to continue...");
                            }

                            Console.ReadKey();
                            break;
                        case '5':
                            ch = AskConfirmation("WARNING: You are about to delete the whole tables from the database. Type {0} and then press enter to continue...");
                            if (ch)
                            {
                                Console.WriteLine("Executing the tables deletion...");
                                db.DeleteTables();
                                Console.WriteLine("Done. Press any key to continue...");
                            }

                            Console.ReadKey();
                            break;
                        case '0':
                            exit = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(ex);

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            while (!exit);
        }

        private static bool AskConfirmation(string message)
        {
            bool result;
            var number = new System.Random().Next(1, 9999).ToString("D4");
            Console.WriteLine(message, number);
            var text = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(text) && text.Equals(number, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            else
            {
                Console.Write("Wrong input. You typed{0}. You will back to the main menu.", string.IsNullOrWhiteSpace(text) ? " an empty value" : ": " + text);
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");

                result = false;
            }

            return result;
        }
    }
}
