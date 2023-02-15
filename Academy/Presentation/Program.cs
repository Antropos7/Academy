
using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data.Contexts.Repositories.Concrete;
using System.Globalization;
using System.Xml;

namespace Presentation

{

    public static class Program
    {
        static void Main()
        {
            GroupRepository _groupRepository = new GroupRepository();
            ConsoleHelper.WriteWithColor("█▓▒▒▒▒▒░░  Welcome!  ░░▒▒▒▒▒▓█", ConsoleColor.Cyan);
            while (true)
            {

                ConsoleHelper.WriteWithColor("█▓▒░(1) Create Group  ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒░(2) Update Group   ░░░░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒░(3) Delete Group   ░░░░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒░(4) Get ALL        ░░░░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒░(5) Get Group by Id ░░░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒░(6) Get Group by Name ░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒░(0) Exit              ░▒▓█", ConsoleColor.DarkMagenta);
                ConsoleHelper.WriteWithColor("█▓▒▒▒░░  Select Option ░░▒▒▒▓█", ConsoleColor.Cyan);

                int number;
                bool isSuccseedeed = int.TryParse(Console.ReadLine(), out number);
                if (!isSuccseedeed)
                {
                    ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                }


                else
                {
                    if (!(number >= 0 && number <= 6))

                    {
                        ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                        ConsoleHelper.WriteWithColor("Inputed number does not exist!", ConsoleColor.Red);
                        ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                        ConsoleHelper.WriteWithColor("", ConsoleColor.Red);

                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:
                                ConsoleHelper.WriteWithColor("==========", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("Enter name", color: ConsoleColor.Cyan);
                                ConsoleHelper.WriteWithColor("==========", ConsoleColor.DarkCyan);
                                string name = Console.ReadLine();
                                int maxSize;
                            MaxSizeDescription:
                                ConsoleHelper.WriteWithColor("====================", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("Enter group max size", ConsoleColor.Cyan);
                                ConsoleHelper.WriteWithColor("====================", ConsoleColor.DarkCyan);
                                isSuccseedeed = int.TryParse(Console.ReadLine(), out maxSize);

                                if (!isSuccseedeed)
                                {
                                    ConsoleHelper.WriteWithColor("===============================", ConsoleColor.DarkRed);
                                    ConsoleHelper.WriteWithColor("Max size is not correct format!", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("===============================", ConsoleColor.DarkRed);
                                    goto MaxSizeDescription;
                                }

                                if (maxSize > 18)
                                {
                                    ConsoleHelper.WriteWithColor("============================================", ConsoleColor.DarkRed);
                                    ConsoleHelper.WriteWithColor("Max size should be less than or equals to 18", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("============================================", ConsoleColor.DarkRed);
                                    goto MaxSizeDescription;
                                }

                            StartDateDescription:
                                ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("Enter start date", ConsoleColor.Cyan);
                                ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);
                                DateTime startDate;
                                isSuccseedeed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                                if (!isSuccseedeed)
                                {
                                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                                    ConsoleHelper.WriteWithColor("Start date is not correct format", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                                    
                                    goto StartDateDescription;
                                }
                                DateTime boundaryDate = new DateTime(2015, 1, 1);

                                if (startDate < boundaryDate)
                                {
                                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                    ConsoleHelper.WriteWithColor("Start date is not chosen right", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                    goto StartDateDescription;
                                }

                            EndDateDescription:
                                ConsoleHelper.WriteWithColor("==============", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("Enter end date", ConsoleColor.Cyan);
                                ConsoleHelper.WriteWithColor("==============", ConsoleColor.DarkCyan);
                                DateTime endDate;
                                isSuccseedeed = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);


                                if (!isSuccseedeed)
                                {
                                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                    ConsoleHelper.WriteWithColor("End date is not correct format", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                    goto EndDateDescription;
                                }
                                if (startDate > endDate)
                                {
                                    ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                                    ConsoleHelper.WriteWithColor("End date must be bigger than start date!", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                                    goto EndDateDescription;

                                }
                                var group = new Group
                                {
                                    Name = name,
                                    MaxSize = maxSize,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                };

                                _groupRepository.Add(group);
                                ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor($"Group successfully created with: \n ****************\n Name: {group.Name} \n ****************\n Max size: {group.MaxSize} \n ****************\n Start date: {group.StartDate.ToShortTimeString()} \n ****************\n End date: {group.EndDate.ToShortTimeString()} \n ****************\n ", ConsoleColor.Green);
                                ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkCyan);
                                break;

                            case (int)GroupOptions.UpdateGroup:
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                var groupss = _groupRepository.GetAll();

                                ConsoleHelper.WriteWithColor("All groups", ConsoleColor.Cyan);
                                foreach (var group_ in groupss)
                                {
                                    ConsoleHelper.WriteWithColor("-----------------------------------------------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id}, Name: {group_.Name}, Max size: {group_.MaxSize}, Start date: {group_.StartDate}, End date: {group_.EndDate}", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("-----------------------------------------------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
                                    Console.WriteLine("");

                                }
                            IdDescription:
                                ConsoleHelper.WriteWithColor("========", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("Enter Id", ConsoleColor.Cyan);
                                ConsoleHelper.WriteWithColor("========", ConsoleColor.DarkCyan);
                                int id;
                                isSuccseedeed = int.TryParse(Console.ReadLine(), out id);
                                
                                
                                if (!isSuccseedeed)
                                {
                                    ConsoleHelper.WriteWithColor("========================", ConsoleColor.DarkCyan);
                                    ConsoleHelper.WriteWithColor("Id is not correct format", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("========================", ConsoleColor.DarkCyan);
                                    goto IdDescription;
                                }
                                
                                var dbGroup = _groupRepository.Get(id);
                                if (dbGroup == null)
                                {
                                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("There is no any groip in this id", ConsoleColor.Red);
                                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.Red);
                                }
                                else
                                {
                                    _groupRepository.Delete(dbGroup);
                                    ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkGreen);
                                    ConsoleHelper.WriteWithColor("Group successfully deleted", ConsoleColor.Green);
                                    ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkGreen);
                                }


                                break;
                            case (int)GroupOptions.GetAllGroups:

                                var groups = _groupRepository.GetAll();
                                ConsoleHelper.WriteWithColor("==========", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("All groups", ConsoleColor.Cyan);
                                ConsoleHelper.WriteWithColor("==========", ConsoleColor.DarkCyan);
                                ConsoleHelper.WriteWithColor("", ConsoleColor.White);

                                foreach (var group_ in groups)
                                {
                                    ConsoleHelper.WriteWithColor("----------------------------------------------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor($"Id: {group_.Id}, Name: {group_.Name}, Max size: {group_.MaxSize}, Start date: {group_.StartDate}, End date: {group_.EndDate}", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("----------------------------------------------------------------------------------------------------------------------------", ConsoleColor.DarkMagenta);
                                    Console.WriteLine("");
                                }
                                break;
                            case (int)GroupOptions.GetGroupById:
                                break;
                            case (int)GroupOptions.GerGroupByName:
                                break;
                            case (int)GroupOptions.Exit:
                                Console.WriteLine("*** BYE! ***");
                                return;
                                break;
                              



                        }
                    }

                }

            }

        }

    }

}