namespace bruteForce_1;

public class Program
{
    public static void Main(string[] args)
    {
        
        string path = "../../../passwords.txt";
        List<string> passwordsFile = File.ReadAllLines(path).ToList();

        Random rnd = new Random();
        List<string> passwordsList = passwordsFile.OrderBy(x => rnd.Next()).Take(200).ToList();


        // Password random de la lista
        string randomPassword = passwordsList[rnd.Next(passwordsList.Count)];
        Console.WriteLine($"Password random: {randomPassword}");

        // La password random hasheada
        string hashedPassword = Utils.HashPass(randomPassword);
        Console.WriteLine($"Hashed password: {hashedPassword}");


        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        
        Wrapper<Action> finalizar = new Wrapper<Action>(() => { });
        
        
        
        MiHilo hilo1 = new MiHilo("1", finalizar, passwordsList.GetRange(0,50), hashedPassword, token, cancellationTokenSource);
        MiHilo hilo2 = new MiHilo("2", finalizar, passwordsList.GetRange(50,50), hashedPassword, token, cancellationTokenSource);
        MiHilo hilo3 = new MiHilo("3", finalizar, passwordsList.GetRange(100,50), hashedPassword, token, cancellationTokenSource);
        MiHilo hilo4 = new MiHilo("4", finalizar, passwordsList.GetRange(150,50), hashedPassword, token, cancellationTokenSource);
        
        hilo1.Start();
        hilo2.Start();
        hilo3.Start();
        hilo4.Start();
    }
}


    


