using System.Diagnostics;

namespace bruteForce_1;


public class MiHilo
{
    private Stopwatch sw;
    Thread hilo;
    private string text;
    private Wrapper<Action> finalizar;
    private List<string> passwords;
    private string realPasswordHash;
    private CancellationToken token;
    private CancellationTokenSource cancellationTokenSource;


    public MiHilo(
        Stopwatch sw,
        string text,
        Wrapper<Action> finalizar,
        List<String> passwords,
        string realPasswordHash,
        CancellationToken token,
        CancellationTokenSource cancellationTokenSource
    )
    {
        this.sw = sw;
        this.text = text;
        this.finalizar = finalizar;
        this.passwords = passwords;
        this.realPasswordHash = realPasswordHash;
        this.token = token;
        this.cancellationTokenSource = cancellationTokenSource;
        finalizar.Value += () => { Console.WriteLine($"Hilo {text} termina."); };
        hilo = new Thread(_process);
    }

    public void Start()
    {
        hilo.Start();
    }

    void _process()
    {

        Console.WriteLine($"Numero de contraseñas to check en el hilo {text}: {passwords.Count}");

        foreach (var (pass, index) in passwords.Select((password, idx) => (password, idx)))
        {

            if (token.IsCancellationRequested)
            {
                return;
            }

            string passwordToCheckHashed = Utils.HashPass(pass);

            //Console.WriteLine($"Hilo {text} procesando: {pass} . {index}");

            if (passwordToCheckHashed == realPasswordHash)
            {
                Console.WriteLine($"Password found: {pass} por el hilo {text} en el indice {index}");
                finalizar.Value.Invoke();
                cancellationTokenSource.Cancel();
                sw.Stop();
                Console.WriteLine(sw.Elapsed);
                return;
            }
        }
    }
}