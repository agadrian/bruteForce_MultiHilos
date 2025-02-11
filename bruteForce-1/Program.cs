namespace bruteForce_1;

using System.Diagnostics;

// 1.- Monohilo
// 2.- Multihilo (nº fijo de hilo)
// 3.- Investigar n de hilos optimo (diccionario completo [+claves])

/* EXTRA */

// Pool de hilos
// Test unitarios
// Interfaz personalizacion


public class Program
{
    public static void Main(string[] args)
    {
        
        Stopwatch sw = new Stopwatch();
        sw.Start();

        string path = "../../../rockyou.txt";
        var passwordsFile = File.ReadAllLines(path).ToList();

        Random rnd = new Random();
        String rndPassword = passwordsFile[rnd.Next(0, passwordsFile.Count)];
        Console.WriteLine("Password: " + rndPassword);

        
        // La password random hasheada
        string hashedPassword = Utils.HashPass(rndPassword);
        Console.WriteLine($"Hashed password: {hashedPassword}");


        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        
        Wrapper<Action> finalizar = new Wrapper<Action>(() => { });
        
        int numHilos = 6; //Environment.ProcessorCount ;
        int totalPasswords = passwordsFile.Count;
        int batchSize = totalPasswords / numHilos;
        
        Console.WriteLine($"Total passwords: {totalPasswords}. Batchsize: {batchSize}. Num hilos: {numHilos}");
        
        List<MiHilo> hilos = new List<MiHilo>();

        for (int i = 0; i < numHilos; i++)
        {
            int inicio = i * batchSize;
            int fin;

            // Si es el ultimo hilo, toma desde el indice de inicio hasta el indice del ultimo elemento de la lista
            if (i == numHilos - 1)
            {
                fin = passwordsFile.Count;
            }
            else // Si no es el ultimo, toma 'batchsize' contraseñas
            {
                fin = inicio + batchSize;
            }
            
            // (fin - inicio) en vez de (inicio - batchsize), porque el ultimo hilo pilla un num disintos de passwords que el resto de hilos
            // Index incluido (incio = 10, count = 10 => Comprueba 10-19)
            List<String> batch = passwordsFile.GetRange(inicio, fin - inicio);
            
            hilos.Add(
                new MiHilo(
                    sw,
                    (i + 1).ToString(),
                    finalizar,
                    batch,
                    hashedPassword,
                    token,
                    cancellationTokenSource
                )
            );
            
            // Mostrar las contraseñas asignadas al hilo actual
            Console.WriteLine($"Hilo {i+1} procesará {batch.Count} contraseñas");
        }

        foreach (var hilo in hilos)
        {
            hilo.Start();
        }
    }
}


    


