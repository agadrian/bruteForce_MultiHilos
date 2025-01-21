

using System.Security.Cryptography;
using System.Text;


string path = "../../../passwords.txt";
List<string> passwordsFile = File.ReadAllLines(path).ToList();

Random rnd = new Random();
List<string> passwordsList = passwordsFile.OrderBy(x => rnd.Next()).Take(100).ToList();


// TEST: Mostrar la lista random que ha elegido
foreach (string test in passwordsList)
{
    Console.Write($"{test}, ");
}

Console.WriteLine();
Console.WriteLine();

// Password random de la lista
string randomPassword = passwordsList[rnd.Next(passwordsList.Count)];
Console.WriteLine($"Password random: {randomPassword}");

// La password random hasheada
string hashedPassword = HashPass(randomPassword);
Console.WriteLine($"Hashed password: {hashedPassword}");


foreach (string password in passwordsList)
{
    string checkHash = HashPass(password);

    if (checkHash == hashedPassword)
    {
        Console.WriteLine($"Contraseña encontrada: {password}");
        Console.WriteLine($"Hash encontrado: {checkHash}");
    }
}


// Hashear string a sha256  
static string HashPass(string password)
{
    using (SHA256 sha256Hash = SHA256.Create())
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256Hash.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
        
    }
}