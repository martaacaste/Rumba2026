using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int CalcularArea(int largo, int ancho) => largo * ancho;

    static void Main(string[] args)
    {
        var zonas = new Dictionary<string, (int largo, int ancho)>
        {
            { "Zona 1", (500, 150) },
            { "Zona 2", (480, 101) },
            { "Zona 3", (309, 480) },
            { "Zona 4", (90, 220) }
        };

        double tasaLimpieza = 1000.0;
        var areas = new ConcurrentDictionary<string, int>();

        var threads = new List<Thread>();
        foreach (var zona in zonas)
        {
            var localZona = zona; // evitar cierre sobre la variable de iteración
            var t = new Thread(() =>
            {
                int area = CalcularArea(localZona.Value.largo, localZona.Value.ancho);
                areas[localZona.Key] = area;
            });
            threads.Add(t);
            t.Start();
        }

        foreach (var t in threads)
            t.Join();

        Console.WriteLine("Áreas calculadas:");
        foreach (var zona in areas)
            Console.WriteLine($"{zona.Key}: {zona.Value} cm²");

        int superficieTotal = areas.Values.Sum();
        double tiempoLimpieza = superficieTotal / tasaLimpieza;

        Console.WriteLine($"\nSuperficie total a limpiar: {superficieTotal} cm²");
        Console.WriteLine($"Tiempo estimado de limpieza: {tiempoLimpieza:F2} segundos");
        Console.WriteLine($"Equivalente en minutos: {tiempoLimpieza / 60:F2} min");
        Console.WriteLine($"Equivalente en horas: {tiempoLimpieza / 3600:F3} h");
    }
}