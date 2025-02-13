# Brute Force Multi-threading Program
## Descripción
Este programa implementa un sistema de fuerza bruta multihilo para encontrar una contraseña en una lista de contraseñas aleatorias. Para ello, se hace uso de la clase SHA256 de la librería System.Security.Cryptography, que forma parte del .NET Framework y .NET Core. Se utiliza el hash SHA256 generado para verificar si las contraseñas coinciden con el hash de la contraseña objetivo.

El programa divide la carga de trabajo entre varios hilos, cada uno verificando una porción de la lista de contraseñas. Una vez que un hilo encuentra la contraseña correcta, notifica a los demás hilos para que se detengan usando un mecanismo de cancelación global.

## Funcionamiento
- Cargar contraseñas: El programa lee un archivo de contraseñas (passwords.txt) y selecciona aleatoriamente 200 de ellas.
- División en hilos: Se divide a partes iguales las contraseñas (el último hilo se encarga de manejar si queda alguna suelta para obtennerla él).
- Hash aleatorio: Se selecciona aleatoriamente una contraseña como objetivo, y se genera un hash SHA256.
- Procesamiento: Cada hilo calcula el hash de las contraseñas en su rango y lo compara con el hash objetivo.
- Cancelación: Cuando un hilo encuentra la contraseña correcta, detiene los demás hilos usando un mecanismo de cancelación mediante token.

# Parte 2 (extras)

## Pool de Hilos

He sustituido la creacion de hilos de forma manual, por un pool de hilos, usando **Parallel.forEach**.
- Esta clase usa **MaxDegreeOfParallelism** para determinar el número máximo de hilos, en este caso, usando **Environment.ProcessorCount** para ver el número de hilos disponible en el equipo en ese momento.
- Además, divide de forma eficiente y automática la cantidad de contraseñas que cada Hilo gestionará.
- Implemento un diccionario para llevar un conteo de cuantas contraseñas a comprobado cada hilo, y se muestra al terminar la ejecución del programa.
- Cuando se encuentra la contraseña, se usa cancellationTokenSource.Cancel() para evitar nuevas iteracions,  y state.Stop() para cancelar la ejecución actual del resto de hilos.

## Interfaz BruteForce

Para comprobar que efectivamente  ```ThreadPool.SetMaxThreads(70, 70);``` permite que el máximo de hilos sea 70 en este caso, y no ```Environment.ProcessorCount```, por defecto.
Aún así, TheradPool sigue asignando la cantidad de hilos que considdera necesaria para la tarea solicitada en cada caso.
En el caso de la fuerza bruta, en mi caso decide usar 12 aunque tenga márgen de usar más.

```
ThreadPool.SetMaxThreads(70, 70);

ConcurrentDictionary<int, bool> threadIds = new ConcurrentDictionary<int, bool>();

Parallel.For(0, 50, i =>
{
    int threadId = Thread.CurrentThread.ManagedThreadId;
    threadIds.TryAdd(threadId, true);
    textBox_log.Invoke((Action)(() =>
        textBox_log.AppendText($"Hilo {threadId} procesando tarea {i}\r\n")));
    Thread.Sleep(15000); // Simula una tarea larga
});

// Total de hilos distintos usados
textBox_log.Invoke((Action)(() =>
    textBox_log.AppendText($"\r\nTotal de hilos distintos usados: {threadIds.Count}\r\n")));
```

Comprobar threads disponibles:
```
int workerThreads, completionPortThreads;
ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
textBox_log.AppendText($"Hilos disponibles en ThreadPool: {workerThreads}\r\n");
```
