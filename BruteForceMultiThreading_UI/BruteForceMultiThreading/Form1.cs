using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



// Crear .exe (bin\Release\net8.0-windows\win-x64\publish): dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true


namespace BruteForceMultiThreading
{
    public partial class BruteForce : Form
    {

        private string selectedFile = "";
        private CancellationTokenSource cancelationTokenSrc;
        ConcurrentDictionary<int, int> threadTasksCounter = new ConcurrentDictionary<int, int>();
        int numThreads;

        private int defaultMinThreads;
        private int defaultMaxThreads;


        public BruteForce()
        {
            InitializeComponent();
        }


        private void btn_choose_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt",
                Title = "Selecciona un wordlist de passwords"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedFile = dialog.FileName;
                textBox_log.AppendText($"WordList seleccionado: {selectedFile}\r\n\r\n");
            }


        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFile))
            {
                MessageBox.Show("Selecciona un archivo de contraseñas primero");
                return;
            }



            ResetThreadPool();


            if (checkBox_auto.Checked)
            {
                numThreads = 100;
                ThreadPool.SetMinThreads(1, 1);
                ThreadPool.SetMaxThreads(numThreads, numThreads);
                textBox_log.AppendText($"Num de hilos automatico (El sistema decide)\r\n");
            }
            else
            {
                if ((int)number_threads.Value <= 0)
                {
                    MessageBox.Show("Número de hilos mínimo: 1");
                    return;
                }

                numThreads = (int)number_threads.Value;

                // Forzar al numero de hilos establecidos
                ThreadPool.SetMinThreads(numThreads, numThreads);
                ThreadPool.SetMaxThreads(numThreads, numThreads);
                textBox_log.AppendText($"\r\nNum de hilos seleccionados: {numThreads}\r\n");
            }

            textBox_log.AppendText($"\r\n\r\n{string.Concat(Enumerable.Repeat("#", 50))}\r\n\r\n");


            //textBox_log.AppendText($"Threadcounter de la ejecucion anterior: {threadTasksCounter.Keys.Count} - {threadTasksCounter.Values.Count}\r\n");
            //textBox_log.AppendText($"Numthread de la ejecucion anterior: {numThreads}\r\n");

            Task.Run(() => StartBruteForce(numThreads));
        }


        private void StartBruteForce(int numThreads)
        {
            var passwords = File.ReadAllLines(selectedFile).ToList();
            string targetPassword = passwords[new Random().Next(passwords.Count)];
            string hashedPassword = Utils.HashPass(targetPassword);

            textBox_log.Invoke((Action)(() =>
                textBox_log.AppendText($"Contraseña random a buscar: {hashedPassword}  --> ({targetPassword})\r\n\r\n")));

            cancelationTokenSrc = new CancellationTokenSource();
            var token = cancelationTokenSrc.Token;

            Stopwatch sw = new Stopwatch();
            sw.Start();


            // Limpiar la cuenta de hilos
            threadTasksCounter.Clear();

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = numThreads , 
                CancellationToken = token
            };
 


            try
            {
                Parallel.ForEach(passwords, options, (password, state, index) =>
                {
                    
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    threadTasksCounter.AddOrUpdate(threadId, 1, (k, v) => v + 1);

                   
                    // Cancelar ejecucion de las tareas cuando se encuentre la password
                    if (token.IsCancellationRequested)
                    {
                        state.Stop();
                        return;
                    }


                    string passwordToCheckHashed = Utils.HashPass(password);

                    if (passwordToCheckHashed == hashedPassword)
                    {
                        textBox_log.Invoke((Action)(() =>
                            textBox_log.AppendText($"Password found: {password} en el índice {index + 1} por el hilo {threadId}\r\n\r\n")
                        ));
                        // Cancelar tanto el token como el state para detener nuevas iteraciones y tareas en curso
                        cancelationTokenSrc.Cancel();
                        state.Stop();
                        sw.Stop();

                        textBox_log.Invoke( (Action)(() =>
                            textBox_log.AppendText($"Tiempo total: {sw.Elapsed} \r\n")
                        ));
                    }
                });

                

            }
            catch (OperationCanceledException e)
            {
                textBox_log.AppendText($"Búsqueda terminada. Todos los hilos se detuvieron.\r\n\r\n");
                //textBox_log.AppendText($"Threadcounter despues de acabar: {threadTasksCounter.Keys.Count} - {threadTasksCounter.Values.Count}\r\n");
                //textBox_log.AppendText($"Numthread despues de acabar: {numThreads}\r\n");
            }
            catch (Exception e)
            {
                textBox_log.AppendText($"Error inesperado: {e.Message}");
            }



            textBox_log.AppendText($"Hilos usados: {threadTasksCounter.Count()}. Reparto de tareas por hilo:\r\n");

            foreach (var hilo in threadTasksCounter)
            {

                textBox_log.AppendText($"   * Hilo {hilo.Key} procesó: {hilo.Value} contraseñas\r\n");
            }
        }

        private void ResetThreadPool()
        {
            // Restaurar los valores originales
            ThreadPool.SetMinThreads(defaultMinThreads, defaultMinThreads);
            ThreadPool.SetMaxThreads(defaultMaxThreads, defaultMaxThreads);
        }


        private void btn_clear_Click(object sender, EventArgs e)
        {
            textBox_log.Clear();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ThreadPool.GetMinThreads(out defaultMinThreads, out _);
            ThreadPool.GetMaxThreads(out defaultMaxThreads, out _);
        }


        private void checkBox_auto_change(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        
    }
}
