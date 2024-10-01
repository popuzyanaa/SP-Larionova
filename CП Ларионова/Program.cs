////05.09.2024 задание с запуском и завершением процессов//

//using System.Diagnostics;
//using System.Security.Cryptography;

//Process[] processes = Process.GetProcesses();

//foreach (Process process in processes)
//{
//    Console.Write("Name: " + process.ProcessName + "\tId: " + process.Id + "\tRAM: " + process.PagedMemorySize + "\tStatus: ");
//    if (process.ProcessName.Length == 0)
//    {
//        Console.Write("Не робит");
//    }
//    else
//    {
//        Console.Write("Робит");
//    }
//    Console.WriteLine();
//}
//Console.Write("Введи ID процесса который хотите завершить: ");
//int pid = int.Parse(Console.ReadLine());
//Process p = Process.GetProcessById(pid);
//Logger.Logging("Процесс убит " + p.ProcessName);
//p.Kill();

//Console.Write("Напишите название чтобы запустить программу: ");
//string path = Console.ReadLine();
//Process.Start(path);

//Logger.Logging("Процесс запущен");

//class Logger
//{
//    public static void Logging(string message)
//    {
//        string logPath = "process_log.txt";

//        using (StreamWriter sw = new StreamWriter(logPath, true))
//        {
//            sw.WriteLine($"{DateTime.Now} {message}");
//        }
//    }
//}


////11.09.2024 запись в файл, чтение файла, получение HTTP запроса и потоковая запись файла//
//using System;
//using System.Net.Http;
//using System.IO;
//using System.Threading;
//using System.Reflection.Metadata.Ecma335;

//class Program
//{
//    static async System.Threading.Tasks.Task Main(string[] args)
//    {
//        HttpClient client = new HttpClient();
//        string result = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1");
//        Console.WriteLine("Выберите действие:\n1. Отправка и получение данных с сервера\n2. Чтение файла\n3. Запись файла\n4. Запуск поточного чтения\nВведите цифру:");
//        string a = Console.ReadLine();

//        switch (a)
//        {
//            case "1":
//                {
//                    Console.WriteLine(result);
//                }
//                break;
//            case "2":
//                {
//                    Console.WriteLine("Введите название файла: ");
//                    string txt = Console.ReadLine();
//                    string pach = txt;

//                    string[] lines = File.ReadAllLines(txt);
//                    foreach (string line in lines)
//                    {
//                        Console.WriteLine(line);
//                    }
//                    break;
//                }
//            case "3":
//                {
//                    Console.WriteLine("Введите название файла: ");
//                    string txt = Console.ReadLine();
//                    string pach = txt;
//                    using (StreamWriter sr = new StreamWriter(txt, true))
//                    {
//                        for (int i = 0; i < 5; i++)
//                        {
//                            sr.WriteLine($"Строка{i}");
//                        }
//                    }
//                    break;
//                }

//            case "4":
//                {
//                    Mutex mutex = new Mutex();
//                    for (int i = 0; i < 50; i++)
//                    {
//                        Thread thread = new Thread(Emelya);
//                        Thread thread1 = new Thread(Eshkeree);
//                        thread.Start();
//                        thread1.Start();
//                    }
//                    break;
//                }
//        }
//    }

//    static void Eshkeree()
//    {
//        int sum = 0;
//        string pach = "file.txt"; // replace with the actual file path
//        Mutex mutex = new Mutex();
//        mutex.WaitOne();
//        string[] arr = File.ReadAllLines(pach);

//        using (StreamReader sw = new StreamReader(pach, true))
//        {
//            for (int i = 0; i < arr.Length; i++)
//            {
//                string word = sw.ReadLine();
//                for (int j = 0; j < word.Length; j++)
//                {
//                    sum += int.Parse(word[j].ToString());
//                }
//                Console.WriteLine($"Сумма всех цифр числа {word}: " + sum);
//                sum = 0;
//            }
//        }
//        mutex.ReleaseMutex();
//    }

//    static void Emelya()
//    {
//        string pach = "file.txt";
//        Mutex mutex = new Mutex();
//        mutex.WaitOne();
//        string[] arr = File.ReadAllLines(pach);
//        int[] numbers = new int[arr.Length];
//        double sum = 0;
//        using (StreamReader sw = new StreamReader(pach, true))
//        {
//            for (int i = 0; i < arr.Length; i++)
//            {
//                string word = sw.ReadLine();
//                for (int j = 0; j < word.Length; j++)
//                {
//                    sum += int.Parse(word[j].ToString());
//                }
//                Console.WriteLine($"Среднее арифметическое всех цифр числа {word}: " + sum);
//                sum = 0;
//            }
//        }
//        mutex.ReleaseMutex();
//    }
//}


////13.09.2024 второе//
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProcessScheduler
//{
//    public class Process
//    {
//        public int Id { get; set; }
//        public int BurstTime { get; set; }
//        public int Priority { get; set; }
//    }

//    public class ProcessScheduler
//    {
//        private Queue<Process> processQueue;
//        private int quantumTime;

//        public ProcessScheduler(int quantumTime = 2)
//        {
//            this.processQueue = new Queue<Process>();
//            this.quantumTime = quantumTime;
//        }

//        public void AddProcess(Process process)
//        {
//            processQueue.Enqueue(process);
//        }

//        public async Task RunRoundRobin()
//        {
//            while (processQueue.Count > 0)
//            {
//                Process currentProcess = processQueue.Dequeue();

//                Console.WriteLine($"Процесс {currentProcess.Id} начал выполнение");

//                for (int i = 0; i < currentProcess.BurstTime; i += quantumTime)
//                {
//                    await Task.Delay(quantumTime * 1000);
//                    Console.WriteLine($"Процесс {currentProcess.Id} выполняется (осталось {currentProcess.BurstTime - i} секунд)");

//                    if (i + quantumTime >= currentProcess.BurstTime)
//                    {
//                        Console.WriteLine($"Процесс {currentProcess.Id} завершён");
//                        break;
//                    }

//                    processQueue.Enqueue(currentProcess);
//                }
//            }
//        }

//        class Program
//        {
//            static void Main(string[] args)
//            {

//                List<Process> processes = new List<Process>
//            {
//                new Process { Id = 1, BurstTime = 5, Priority = 2 },
//                new Process { Id = 2, BurstTime = 3, Priority = 1 },
//                new Process { Id = 3, BurstTime = 8, Priority = 3 }
//            };

//                ProcessScheduler scheduler = new ProcessScheduler();

//                foreach (Process process in processes)
//                {
//                    scheduler.AddProcess(process);
//                }


//                scheduler.RunRoundRobin();

//                Console.ReadLine();
//            }
//        }
//    }
//}

////18.09.2024 Задание с посещаемостью студентов//
//List<Student> students = new List<Student>();
//AttendanceManager attendanceManager = new AttendanceManager();
//while (true)
//{
//    Console.Write("1.Добавить нового студента\n2.Удалить студента по идентификатору\n3.Редактировать данные студента\n4.Отобразить список всех студентов\n5.Добавить запись о посещении для студента\n6.Отобразить записи о посещаемости для конкретного студента\n7.Выйти из приложения\n");
//    int choice = int.Parse(Console.ReadLine());

//    switch (choice)
//    {
//        case 1:
//            {
//                students.Add(attendanceManager.AddStudent(students.Count + 1));
//                break;
//            }
//        case 2:
//            {
//                Console.Write("Введите ID студента: ");
//                int ID = int.Parse(Console.ReadLine());
//                students = attendanceManager.RemoveStudent(ID, students);
//                break;
//            }
//        case 3:
//            {
//                Console.Write("Введите ID студента: ");
//                int ID = int.Parse(Console.ReadLine());
//                students = attendanceManager.EditStudent(ID - 1, students);
//                break;
//            }
//        case 4:
//            {
//                attendanceManager.ListStudents(students);
//                break;
//            }
//        case 5:
//            {
//                Console.Write("Введите ID студента: ");
//                int ID = int.Parse(Console.ReadLine());
//                students = attendanceManager.AddAttendance(ID, students);
//                break;
//            }
//        case 6:
//            {
//                Console.Write("Введите ID студента: ");
//                int ID = int.Parse(Console.ReadLine());
//                attendanceManager.ListAttendance(ID, students);
//                break;
//            }
//        case 7:
//            {
//                Environment.Exit(0);
//                break;
//            }
//    }
//}

//class Student
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//    public List<AttendanceRecord> Attendance = new List<AttendanceRecord>();
//}
//class AttendanceRecord
//{
//    public DateTime Date { get; set; }
//    public bool IsPresent { get; set; }

//}
//class AttendanceManager
//{
//    public Student AddStudent(int NewID)
//    {
//        Student student = new Student();
//        student.Id = NewID;
//        Console.Write("Напишите ФИО студента: ");
//        student.Name = Console.ReadLine();
//        Console.Write("Напишите Email студента: ");
//        student.Email = Console.ReadLine();
//        Console.Clear();
//        return student;
//    }
//    public List<Student> RemoveStudent(int id, List<Student> students)
//    {
//        foreach (Student student in students)
//        {
//            if (student.Id == id)
//            {
//                students.Remove(student);
//                break;
//            }
//        }
//        Console.Clear();
//        return students;
//    }
//    public List<Student> EditStudent(int id, List<Student> updatedStudent)
//    {
//        Console.Write("Напишите новое ФИО студента: ");
//        updatedStudent[id].Name = Console.ReadLine();
//        Console.Write("Напишите новый Email студента: ");
//        updatedStudent[id].Email = Console.ReadLine();
//        Console.Clear();
//        return updatedStudent;
//    }
//    public void ListStudents(List<Student> students)
//    {
//        foreach (Student student in students)
//        {
//            Console.WriteLine($"{student.Id}.Имя студента: {student.Name}|Почта студента: {student.Email}");
//        }
//    }
//    public List<Student> AddAttendance(int studentId, List<Student> students)
//    {
//        foreach (Student student in students)
//        {
//            if (student.Id == studentId)
//            {
//                AttendanceRecord record = new AttendanceRecord();
//                student.Attendance.Add(record);
//                Console.Write("Введите месяц: ");
//                int month = int.Parse(Console.ReadLine());
//                Console.Write("Введите день: ");
//                int day = int.Parse(Console.ReadLine());
//                DateTime time = new DateTime(2024, month, day);
//                student.Attendance[student.Attendance.Count - 1].Date = time;
//                Console.Write("Введите присутсвовал ли студент?\n1.Да\n2.Нет\n");
//                int choice = int.Parse(Console.ReadLine());
//                if (choice == 1)
//                {
//                    student.Attendance[student.Attendance.Count - 1].IsPresent = true;
//                }
//                else
//                {
//                    student.Attendance[student.Attendance.Count - 1].IsPresent = false;
//                }
//            }
//        }
//        return students;
//    }
//    public void ListAttendance(int studentId, List<Student> students)
//    {
//        foreach (Student student in students)
//        {
//            if (student.Id == studentId)
//            {
//                Console.WriteLine($"{student.Id}.Имя студента: {student.Name}|Почта студента: {student.Email}\n");
//                foreach (AttendanceRecord record in student.Attendance)
//                {
//                    Console.Write($"{record.Date}: {record.IsPresent}\n");
//                }
//            }
//        }
//    }
//}


////20.09.2024 Создание утилиты для анализа и управления памятью в системе//

//using System;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Threading;

//namespace MemoryManagementApp
//{
//    class Program
//    {
//        public static void Main()
//        {
//            Console.WriteLine("Выберите действие:");
//            Console.WriteLine("1. Анализ использования памяти");
//            Console.WriteLine("2. Выделение и освобождение памяти");
//            Console.WriteLine("3. Мониторинг использования памяти процессом");
//            string choice = Console.ReadLine();

//            switch (choice)
//            {
//                case "1":
//                    DisplayMemoryInfo();
//                    break;
//                case "2":
//                    AllocateAndFreeMemory();
//                    break;
//                case "3":
//                    MonitorProcessMemory();
//                    break;
//                default:
//                    Console.WriteLine("Некорректный ввод");
//                    break;
//            }
//        }

//        // 1. Анализ использования памяти
//        static void DisplayMemoryInfo()
//        {
//            long totalMemory = GetTotalPhysicalMemory();
//            long freeMemory = GetAvailablePhysicalMemory();

//            Console.WriteLine($"Общая память: {totalMemory} MB");
//            Console.WriteLine($"Свободная память: {freeMemory} MB");
//            Console.WriteLine($"Занятая память: {totalMemory - freeMemory} MB");
//        }

//        private static long GetTotalPhysicalMemory()
//        {
//            var memoryStatus = new MEMORYSTATUSEX();
//            GlobalMemoryStatusEx(memoryStatus);
//            return (long)(memoryStatus.ullTotalPhys / (1024 * 1024));
//        }

//        private static long GetAvailablePhysicalMemory()
//        {
//            var memoryStatus = new MEMORYSTATUSEX();
//            GlobalMemoryStatusEx(memoryStatus);
//            return (long)(memoryStatus.ullAvailPhys / (1024 * 1024));
//        }

//        // 2. Выделение и освобождение памяти
//        static void AllocateAndFreeMemory()
//        {
//            Console.WriteLine("Введите размер блока памяти для выделения (в MB):");
//            if (int.TryParse(Console.ReadLine(), out int sizeMB))
//            {
//                try
//                {
//                    byte[] memoryBlock = new byte[sizeMB * 1024 * 1024];
//                    Random rand = new Random();
//                    for (int i = 0; i < memoryBlock.Length; i++)
//                    {
//                        memoryBlock[i] = (byte)rand.Next(0, 256);
//                    }
//                    Console.WriteLine($"Выделено {sizeMB} MB памяти и инициализировано случайными данными.");

//                    memoryBlock = null;
//                    GC.Collect();
//                    Console.WriteLine("Память освобождена.");
//                }
//                catch (OutOfMemoryException)
//                {
//                    Console.WriteLine("Ошибка: Недостаточно памяти для выделения.");
//                }
//            }
//            else
//            {
//                Console.WriteLine("Некорректный ввод.");
//            }
//        }

//        // 3. Мониторинг использования памяти процессом
//        static void MonitorProcessMemory()
//        {
//            Console.WriteLine("Введите PID процесса для мониторинга:");
//            if (int.TryParse(Console.ReadLine(), out int pid))
//            {
//                try
//                {
//                    Process process = Process.GetProcessById(pid);

//                    while (!process.HasExited)
//                    {
//                        Console.WriteLine($"Использование памяти процессом {process.ProcessName}: {process.WorkingSet64 / (1024 * 1024)} MB");
//                        Thread.Sleep(1000);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Ошибка: {ex.Message}");
//                }
//            }
//            else
//            {
//                Console.WriteLine("Некорректный PID.");
//            }
//        }

//        // Импорт функции GlobalMemoryStatusEx из kernel32.dll
//        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
//        [return: MarshalAs(UnmanagedType.Bool)]
//        private static extern bool GlobalMemoryStatusEx([In] MEMORYSTATUSEX lpBuffer);

//        // Структура для хранения информации о памяти
//        [StructLayout(LayoutKind.Sequential)]
//        private class MEMORYSTATUSEX
//        {
//            public uint dwLength;
//            public uint dwMemoryLoad;
//            public ulong ullTotalPhys;
//            public ulong ullAvailPhys;
//            public ulong ullTotalPageFile;
//            public ulong ullAvailPageFile;
//            public ulong ullTotalVirtual;
//            public ulong ullAvailVirtual;
//            public ulong ullAvailExtendedVirtual;

//            public MEMORYSTATUSEX()
//            {
//                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
//            }
//        }
//    }
//}


////25.09 Нахождение максимального количества путей в матрице//

//using System;
//class Program
//{
//    static void Main(string[] args)
//    {
//        Console.Write("Введите количество строк (n): ");
//        int n = int.Parse(Console.ReadLine());

//        Console.Write("Введите количество столбцов (m): ");
//        int m = int.Parse(Console.ReadLine());

//        int pathCount = CountPaths(n, m);
//        Console.WriteLine($"Максимальное количество путей в матрице {n}x{m}: {pathCount}");
//    }

//    static int CountPaths(int n, int m)
//    {
//        // Создаем матрицу для хранения количества путей
//        int[,] dp = new int[n, m];

//        // Заполняем первую строку
//        for (int j = 0; j < m; j++)
//        {
//            dp[0, j] = 1; // В первой строке только 1 путь
//        }

//        // Заполняем первый столбец
//        for (int i = 0; i < n; i++)
//        {
//            dp[i, 0] = 1; // В первом столбце только 1 путь
//        }

//        // Заполняем остальные ячейки матрицы
//        for (int i = 1; i < n; i++)
//        {
//            for (int j = 1; j < m; j++)
//            {
//                dp[i, j] = dp[i - 1, j] + dp[i, j - 1]; // Суммируем пути из верхней и левой ячеек
//            }
//        }

//        // Возвращаем количество путей к нижнему правому углу
//        return dp[n - 1, m - 1];
//    }
//}
