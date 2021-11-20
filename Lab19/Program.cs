using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Модель  компьютера  характеризуется  кодом  
 * и  названием  марки компьютера,  типом  процессора,  
 * частотой  работы  процессора,  объемом оперативной памяти, 
 * объемом жесткого диска, объемом памяти видеокарты, 
 * стоимостью компьютера в условных единицах 
 * и количеством экземпляров, имеющихся в наличии.
 * Создать список, содержащий 6-10 записей с различным 
 * набором значений характеристик.

Определить:
- все компьютеры с указанным процессором. Название процессора запросить у пользователя;
- все компьютеры с объемом ОЗУ не ниже, 
чем указано. Объем ОЗУ запросить у пользователя;
- вывести весь список, отсортированный по увеличению стоимости;
- вывести весь список, сгруппированный по типу процессора;
- найти самый дорогой и самый бюджетный компьютер;
- есть ли хотя бы один компьютер в количестве не менее 30 штук?*/

namespace Lab19
{
    class Processor
    {
        public Processor(string name, double frequency)
        {
            Name = name;
            Frequency = frequency;
        }
        public string Name { get; set; }
        public double Frequency { get; set; }
    }
    class Computer
    {
        public Computer(string label, Processor proc, int ramVal, int hardVal, int gpuVal, double price, int count)
        {
            Label = label;
            Processor = proc;
            RamValue = ramVal;
            HardValue = hardVal;
            GPUValue = gpuVal;
            Price = price;
            Count = count;
        }
        public string Label { get; set; }
        public Processor Processor { get; set; }
        public int RamValue { get; set; }
        public int HardValue { get; set; }
        public int GPUValue { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Processor intelCoreI5 = new Processor("Intel Core I5", 3.4);
            Processor intelCoreI7 = new Processor("Intel Core I7", 3.6);
            Processor intelCoreI9 = new Processor("Intel Core I9", 3.9);
            List<Computer> listComputer = new List<Computer>()
            {
            new Computer("001.Тайга", intelCoreI5, 4, 256, 1, 10599.9, 32),
            new Computer("002.Буран", intelCoreI7, 8, 512, 2, 20799.9, 16),
            new Computer("003.Сайгак", intelCoreI7, 4, 256, 2, 15799.9, 8),
            new Computer("004.Баргузин", intelCoreI9, 16, 1024, 4, 30899.9, 3),
            new Computer("005.Сармат", intelCoreI5, 2, 128, 1, 8999.9, 30),
            new Computer("006.Байкал", intelCoreI9, 32, 1024, 4, 35999.9, 1),
            };

            string inputProcessor = Input("Запрос 1. Введите название процессора: ");
            List<Computer> computers1 = listComputer.Where(c => c.Processor.Name == inputProcessor)
                                                    .ToList();
            if (computers1.Count != 0) listPrint(computers1, "Результат запроса:");
            else Console.WriteLine("Компьютеров с таким процессором нет в наличии.\n");
            int inputRam = 4;
            try
            {
                inputRam = Convert.ToInt32(Input("Запрос 2. Введите объем ОЗУ в Гб: "));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Значение выставлено по умолчанию: 4 Гб.");
            }
            List<Computer> computers2 = listComputer.Where(c => c.RamValue >= inputRam)
                                                    .OrderBy(c => c.RamValue)
                                                    .ToList();
            if (computers2.Count != 0) listPrint(computers2,"Результат запроса");
            else Console.WriteLine("Компьютеров с такой ОЗУ нет в наличии.\n");

            Console.WriteLine("Запрос 3. Отсортировать по возрастанию стоимости.");
            Console.ReadKey();
            List<Computer> computers3 = listComputer.OrderBy(c => c.Price)
                                                    .ToList();
            listPrint(computers3,"Результат сортировки:");

            Console.WriteLine("Запрос 4. Cгруппировать по типу процессора.");
            Console.ReadKey();
            var computers4 = listComputer.GroupBy(c => c.Processor.Name)
                                                    .ToList();
            foreach (IGrouping<string, Computer> cg in computers4)
            {
                Console.WriteLine(cg.Key);
                foreach (var c in cg)
                    Console.WriteLine(c.Label);
                Console.WriteLine();
            }
            Console.WriteLine("Запрос 5. Самый дорогой и самый бюджетный компьютер.");
            Console.ReadKey();
            double maxPrice = listComputer.Max(c => c.Price);
            double minPrice = listComputer.Min(c => c.Price);
            List<Computer> maxPriceComputers = listComputer.Where(c=>c.Price==maxPrice)
                                                          .ToList();
            List<Computer> minPriceComputers = listComputer.Where(c => c.Price == minPrice)
                                                          .ToList();
            listPrint(maxPriceComputers, $"Компьютер(ы) с максимальной ценой = {maxPrice} у.е.:");
            listPrint(minPriceComputers, $"Компьютер(ы) с минимальной ценой = {minPrice} у.е.:");

            Console.WriteLine("Запрос 6. Компьютеры, с наличием в 30 и более штук.");
            Console.ReadKey();
            List<Computer> lotComputers = listComputer.Where(c => c.Count >= 30)
                                                          .ToList();
            if (lotComputers.Count != 0) listPrint(lotComputers, "Такие есть:");
            else Console.WriteLine("Таких компьютеров нет.");

            Console.ReadKey();

        }
        static string Input(string text)
        {
                Console.Write(text);
                return Console.ReadLine();
        }
        static void listPrint(List<Computer> list, string text)
        {
            Console.WriteLine(text);
            foreach (Computer c in list)
            {
                Console.WriteLine();
                Console.WriteLine($"Имя: {c.Label}\n" +
                                $"Процессор: {c.Processor.Name}\n" +
                                $"Частота процессора: {c.Processor.Frequency} ГГц\n" +
                                $"Объем ОЗУ: {c.RamValue} Гб\n" +
                                $"Объем жёсткого диска: {c.HardValue} Гб\n" +
                                $"Объем видеопамяти: {c.GPUValue} Гб\n" +
                                $"Цена: {c.Price} у.е.\n" +
                                $"Количество в наличии: {c.Count} шт.");
            }
            Console.WriteLine();
        }








    }
}
