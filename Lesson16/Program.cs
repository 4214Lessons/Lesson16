namespace Lesson16;


class Student : IDisposable
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Student()
    {
        Console.Write("Constructor - Gen: ");
        Console.WriteLine(GC.GetGeneration(this));
    }


    ~Student() // Finalizer
    {
        Console.WriteLine($"Finalizer {Id}");
        Console.WriteLine($"Gen: {GC.GetGeneration(this)}");
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Console.WriteLine($"Dispose {Id}");
        Console.WriteLine($"Gen: {GC.GetGeneration(this)}");
    }
}

class Program
{
    static void DoSomething()
    {
        for (int i = 0; i < 3; i++)
        {
             using var s = new Student() { Id = i };
        }
    }


    static void Main()
    {
        using FileStream fs = new FileStream("myFile.txt", FileMode.Create);
        using StreamWriter sw = new StreamWriter(fs);



        DoSomething();



        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}