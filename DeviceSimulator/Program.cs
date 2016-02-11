using System;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using DeviceSimulator.Entities;
using Microsoft.ServiceBus.Messaging;
using System.Threading;

namespace DeviceSimulator
{
    class Program
    {
        static EventHubConfig config = new EventHubConfig();
        static EventHubClient _eventHubClient;
        static int _standDelay;
        static int _statusDelayMin;
        static int _statusDelayMax;
        static int _statusCompleteDelay;

        public enum StatusEnum
        {
            FUEL = 0,
            CLEANING = 1,
            SAFETY = 2,
            CATERING = 3
        }

        static void Main(string[] args)
        {
            config.StatusCompleteDelay = ConfigurationManager.AppSettings["StatusCompleteDelay"];
            config.StatusDelayMin = ConfigurationManager.AppSettings["StatusDelayMin"];
            config.StatusDelayMax = ConfigurationManager.AppSettings["StatusDelayMax"];
            config.StandDelay = ConfigurationManager.AppSettings["StandDelay"];
            config.ConnectionString = ConfigurationManager.AppSettings["EventHubConnectionString"];
            config.EventHubName = ConfigurationManager.AppSettings["EventHubName"];

            _eventHubClient = EventHubClient.CreateFromConnectionString(config.ConnectionString, config.EventHubName);

            _standDelay = Convert.ToInt32(config.StandDelay);
            _statusDelayMin = Convert.ToInt32(config.StatusDelayMin);
            _statusDelayMax = Convert.ToInt32(config.StatusDelayMax);
            _statusCompleteDelay = Convert.ToInt32(config.StatusCompleteDelay);


            ThreadStart threadStand1 = new ThreadStart(new Program().StandThread1);
            ThreadStart threadStand2 = new ThreadStart(new Program().StandThread2);
            ThreadStart threadStand3 = new ThreadStart(new Program().StandThread3);
            ThreadStart threadStand4 = new ThreadStart(new Program().StandThread4);
            ThreadStart threadStand5 = new ThreadStart(new Program().StandThread5);

            Thread[] standThreads = new Thread[5];
            standThreads[0] = new Thread(threadStand1);
            standThreads[1] = new Thread(threadStand2);
            standThreads[2] = new Thread(threadStand3);
            standThreads[3] = new Thread(threadStand4);
            standThreads[4] = new Thread(threadStand5);

            foreach (Thread thread in standThreads)
            {
                thread.Start();
            }

        }


        public static void GenerateRandomData(int standId, StatusEnum modelName)
        {
            Random r = new Random();
            int completePercentage = 0;

            completePercentage = r.Next(1, 30);
            var standData = new StatusModel() { StandID = standId, Status = completePercentage, TimeStamp = DateTime.Now, Description = modelName.ToString() };
            SendToEventhub(standData);
            Thread.Sleep(r.Next(_statusDelayMin, _statusDelayMax));

            completePercentage = r.Next(31, 60);
            standData = new StatusModel() { StandID = standId, Status = completePercentage, TimeStamp = DateTime.Now, Description = modelName.ToString() };
            SendToEventhub(standData);
            Thread.Sleep(r.Next(_statusDelayMin, _statusDelayMax));

            completePercentage = r.Next(61, 99);
            standData = new StatusModel() { StandID = standId, Status = completePercentage, TimeStamp = DateTime.Now, Description = modelName.ToString() };
            SendToEventhub(standData);
            Thread.Sleep(r.Next(_statusDelayMin, _statusDelayMax));

            completePercentage = 100;
            standData = new StatusModel() { StandID = standId, Status = completePercentage, TimeStamp = DateTime.Now, Description = modelName.ToString() };
            SendToEventhub(standData);

            //Pause for reset progress to 0
            Thread.Sleep(_statusCompleteDelay);

            completePercentage = 0;
            standData = new StatusModel() { StandID = standId, Status = completePercentage, TimeStamp = DateTime.Now, Description = modelName.ToString() };
            SendToEventhub(standData);

        }

        public static void SendToEventhub(StatusModel standData)
        {
            var serialisedString = JsonConvert.SerializeObject(standData);
            EventData data = new EventData(Encoding.UTF8.GetBytes(serialisedString));
            _eventHubClient.Send(data);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Sent: " + serialisedString);
        }

        #region Threads

        public void StandThread1()
        {
            ThreadStart threadFuel = new ThreadStart(new Program().FuelThread1);
            ThreadStart threadCleaning = new ThreadStart(new Program().CleaningThread1);
            ThreadStart threadSafety = new ThreadStart(new Program().SafetyThread1);
            ThreadStart threadCatering = new ThreadStart(new Program().CateringThread1);

            Thread[] statusThreads = new Thread[4];
            statusThreads[0] = new Thread(threadFuel);
            statusThreads[1] = new Thread(threadCleaning);
            statusThreads[2] = new Thread(threadSafety);
            statusThreads[3] = new Thread(threadCatering);

            foreach (Thread thread in statusThreads)
            {
                thread.Start();
            }
        }
        public void StandThread2()
        {
            ThreadStart threadFuel = new ThreadStart(new Program().FuelThread2);
            ThreadStart threadCleaning = new ThreadStart(new Program().CleaningThread2);
            ThreadStart threadSafety = new ThreadStart(new Program().SafetyThread2);
            ThreadStart threadCatering = new ThreadStart(new Program().CateringThread2);

            Thread[] statusThreads = new Thread[4];
            statusThreads[0] = new Thread(threadFuel);
            statusThreads[1] = new Thread(threadCleaning);
            statusThreads[2] = new Thread(threadSafety);
            statusThreads[3] = new Thread(threadCatering);

            foreach (Thread thread in statusThreads)
            {
                thread.Start();
            }
        }
        public void StandThread3()
        {
            ThreadStart threadFuel = new ThreadStart(new Program().FuelThread3);
            ThreadStart threadCleaning = new ThreadStart(new Program().CleaningThread3);
            ThreadStart threadSafety = new ThreadStart(new Program().SafetyThread3);
            ThreadStart threadCatering = new ThreadStart(new Program().CateringThread3);

            Thread[] statusThreads = new Thread[4];
            statusThreads[0] = new Thread(threadFuel);
            statusThreads[1] = new Thread(threadCleaning);
            statusThreads[2] = new Thread(threadSafety);
            statusThreads[3] = new Thread(threadCatering);

            foreach (Thread thread in statusThreads)
            {
                thread.Start();
            }
        }
        public void StandThread4()
        {
            ThreadStart threadFuel = new ThreadStart(new Program().FuelThread4);
            ThreadStart threadCleaning = new ThreadStart(new Program().CleaningThread4);
            ThreadStart threadSafety = new ThreadStart(new Program().SafetyThread4);
            ThreadStart threadCatering = new ThreadStart(new Program().CateringThread4);

            Thread[] statusThreads = new Thread[4];
            statusThreads[0] = new Thread(threadFuel);
            statusThreads[1] = new Thread(threadCleaning);
            statusThreads[2] = new Thread(threadSafety);
            statusThreads[3] = new Thread(threadCatering);

            foreach (Thread thread in statusThreads)
            {
                thread.Start();
            }
        }
        public void StandThread5()
        {
            ThreadStart threadFuel = new ThreadStart(new Program().FuelThread5);
            ThreadStart threadCleaning = new ThreadStart(new Program().CleaningThread5);
            ThreadStart threadSafety = new ThreadStart(new Program().SafetyThread5);
            ThreadStart threadCatering = new ThreadStart(new Program().CateringThread5);

            Thread[] statusThreads = new Thread[4];
            statusThreads[0] = new Thread(threadFuel);
            statusThreads[1] = new Thread(threadCleaning);
            statusThreads[2] = new Thread(threadSafety);
            statusThreads[3] = new Thread(threadCatering);

            foreach (Thread thread in statusThreads)
            {
                thread.Start();
            }
        }

        public void FuelThread1()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: FUEL EMPTY");
                GenerateRandomData(1, StatusEnum.FUEL);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: FUEL FULL");
                Thread.Sleep(_standDelay);
            }
        }
        public void FuelThread2()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: FUEL EMPTY");
                GenerateRandomData(2, StatusEnum.FUEL);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: FUEL FULL");
                Thread.Sleep(_standDelay);
            }
        }
        public void FuelThread3()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: FUEL EMPTY");
                GenerateRandomData(3, StatusEnum.FUEL);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: FUEL FULL");
                Thread.Sleep(_standDelay);
            }
        }
        public void FuelThread4()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: FUEL EMPTY");
                GenerateRandomData(4, StatusEnum.FUEL);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: FUEL FULL");
                Thread.Sleep(_standDelay);
            }
        }
        public void FuelThread5()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: FUEL EMPTY");
                GenerateRandomData(5, StatusEnum.FUEL);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: FUEL FULL");
                Thread.Sleep(_standDelay);
            }
        }

        public void CleaningThread1()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: CLEANING STARTED");
                GenerateRandomData(1, StatusEnum.CLEANING);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: CLEANING FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CleaningThread2()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: CLEANING STARTED");
                GenerateRandomData(2, StatusEnum.CLEANING);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: CLEANING FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CleaningThread3()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: CLEANING STARTED");
                GenerateRandomData(3, StatusEnum.CLEANING);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: CLEANING FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CleaningThread4()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: CLEANING STARTED");
                GenerateRandomData(4, StatusEnum.CLEANING);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: CLEANING FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CleaningThread5()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: CLEANING STARTED");
                GenerateRandomData(5, StatusEnum.CLEANING);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: CLEANING FINISHED");
                Thread.Sleep(_standDelay);
            }
        }

        public void SafetyThread1()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: SAFETY CHECK STARTED");
                GenerateRandomData(1, StatusEnum.SAFETY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: SAFETY CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void SafetyThread2()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: SAFETY CHECK STARTED");
                GenerateRandomData(2, StatusEnum.SAFETY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: SAFETY CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void SafetyThread3()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: SAFETY CHECK STARTED");
                GenerateRandomData(3, StatusEnum.SAFETY);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: SAFETY CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void SafetyThread4()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: SAFETY CHECK STARTED");
                GenerateRandomData(4, StatusEnum.SAFETY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: SAFETY CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void SafetyThread5()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: SAFETY CHECK STARTED");
                GenerateRandomData(5, StatusEnum.SAFETY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: SAFETY CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }

        public void CateringThread1()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: CATERING CHECK STARTED");
                GenerateRandomData(1, StatusEnum.CATERING);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Stand 1: CATERING CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CateringThread2()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: CATERING CHECK STARTED");
                GenerateRandomData(2, StatusEnum.CATERING);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stand 2: CATERING CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CateringThread3()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: CATERING CHECK STARTED");
                GenerateRandomData(3, StatusEnum.CATERING);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Stand 3: CATERING CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CateringThread4()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: CATERING CHECK STARTED");
                GenerateRandomData(4, StatusEnum.CATERING);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Stand 4: CATERING CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }
        public void CateringThread5()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: CATERING CHECK STARTED");
                GenerateRandomData(5, StatusEnum.CATERING);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Stand 5: CATERING CHECK FINISHED");
                Thread.Sleep(_standDelay);
            }
        }

        #endregion

    }

}
