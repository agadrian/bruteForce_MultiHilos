

using System.Security.Cryptography;
using System.Text;


string path = "../../../passwords.txt";
List<string> passwordsFile = File.ReadAllLines(path).ToList();

Random rnd = new Random();
List<string> passwordsList = passwordsFile.OrderBy(x => rnd.Next()).Take(100).ToList();


// Password random de la lista
string randomPassword = passwordsList[rnd.Next(passwordsList.Count)];
Console.WriteLine($"Password random: {randomPassword}");

// La password random hasheada
string hashedPassword = HashPass(randomPassword);
Console.WriteLine($"Hashed password: {hashedPassword}");



Thread hilo1 = new Thread(() => CheckPassword(passwordsList.Take(50).ToList(), hashedPassword));
Thread hilo2 = new Thread(() => CheckPassword(passwordsList.Skip(50).ToList(), hashedPassword));


hilo1.Start();
hilo2.Start();



static void CheckPassword(List<string> passwords, string realPasswordHash)
{
    foreach (var (pass, index) in passwords.Select((password, idx) => (password, idx)))
    {
        string passwordToCheckHashed = HashPass(pass);
        
        Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} procesando: {pass} . {index}");

        if (passwordToCheckHashed == realPasswordHash)
        {
            Console.WriteLine($"Password found: {pass} por el hilo {Thread.CurrentThread.ManagedThreadId} en el indice {index}");
        }
    }


    //return "counter.ToString()";

}


    
static string HashPass(string password)
{
    using (SHA256 sha256Hash = SHA256.Create())
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256Hash.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}



    


