using DLL_Library_Lab2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Lab5
{
    internal class Program
    {
        static Random r = new Random();

        static void Main(string[] args)
        {
            List<User> users = new List<User>();

            try
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Add user? y/n");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        Console.WriteLine();
                        Console.Write("Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Username: ");
                        string username = Console.ReadLine();

                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();

                        users.Add(new User(id, username, password, email, phone, GetRandomMeasuringChannel()));
                    }
                    else flag = false;
                }
                Console.WriteLine();
                PrintAll(users);
            }
            catch (Exception)
            {
                throw;
            }
        }
        static public MeasuringChannel GetRandomMeasuringChannel() => new MeasuringChannel(GetSomeRandomDevices());
        static public List<Device> GetSomeRandomDevices()
        {
            List<Device> devices = new List<Device>();

            for (int i = 0; i < r.Next(1, 5); i++)
                devices.Add(new Device(GetRandomSensor(), r.Next(4, 10), new DateTime(r.Next(2014, 2023), r.Next(1, 13), r.Next(1, 28))));

            return devices;
        }
        static public Sensor GetRandomSensor() => new Sensor((QuantityType)r.Next(1, 5), r.Next(0, 6), r.Next(10, 500), r.Next(0, 500));
        static public void PrintAll(List<User> users)
        {
            foreach (User user in users)
            {
                Console.WriteLine($"{user.Id}\t{user.Username}\t{user.Password}\t{user.Email}\t{user.Phone}\t{user.MC}\n");
                foreach (Device device in user.MC.MChannel)
                {
                    Console.WriteLine($"{device.Location}\t{device.CalibrationDate}");
                    Console.WriteLine($"{device.Sensor.Type}\t{device.Sensor.StartRangeMeasure}\t{device.Sensor.EndRangeMeasure}\t{device.Sensor.Value}\n");
                }
            }
        }
    }
}
