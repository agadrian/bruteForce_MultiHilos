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
