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

He creado una interfaz básica donde el funcionamiento base es el mismo, pero pudiendo ajustar algún parámetro.
Características:
- Button para seleccionar el archivo que contiene las contraseñas
- TextBox para registrar y mostrar logs continuamente
- Checkbox para la seleccion automatica de hilos, y NumericUpDown para establecer el número de hilos manualmente.
- Button tanto para iniciar la búsqueda, como para limpiar el log

Path al directorio: [Directorio_UI](BruteForceMultiThreading_UI/BruteForceMultiThreading)
  

