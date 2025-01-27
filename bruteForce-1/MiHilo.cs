namespace bruteForce_1;


public class MiHilo
{
    Thread hilo;
    private string text;
    private Wrapper<Action> finalizar;
    private List<string> passwords;
    private string realPasswordHash;
    private CancellationToken token;
    private CancellationTokenSource cancellationTokenSource;

    
    public MiHilo(string text, Wrapper<Action> finalizar, List<String> passwords, string realPasswordHash, CancellationToken token, CancellationTokenSource cancellationTokenSource)
    {
        this.text = text;
        this.finalizar = finalizar;
        this.passwords = passwords;
        this.realPasswordHash = realPasswordHash;
        this.token = token;
        this.cancellationTokenSource = cancellationTokenSource;
        finalizar.Value += () => { Console.WriteLine ($"Hilo {text} termina."); };
        hilo = new Thread(_process);
    }

    public void Start()
    {
        hilo.Start();
    }

    void _process()
    {
        
        Console.WriteLine($"Number of passwords to check in thread ${text}: {passwords.Count}");
        
        foreach (var (pass, index) in passwords.Select((password, idx) => (password, idx)))
        {

            if (token.IsCancellationRequested)
            {
                finalizar.Value.Invoke();
                return;
            }
            
            string passwordToCheckHashed = Utils.HashPass(pass);
        
            Console.WriteLine($"Hilo {text} procesando: {pass} . {index}");

            if (passwordToCheckHashed == realPasswordHash)
            {
                Console.WriteLine($"Password found: {pass} por el hilo {text} en el indice {index}");
                //finalizar.Value.Invoke();
                cancellationTokenSource.Cancel();
                
                break;
            }
        }
        
    }
    
    
    
    
    
}