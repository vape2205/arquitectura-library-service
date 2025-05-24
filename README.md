<h1 align="center" id="title">SERVICIO DE LIBRERIA</h1>

<p align="center"><img src="https://github.com/vape2205/arquitectura-library-service" alt="project-image"></p>

<p id="description">Servicio para la gestion de libros</p>
  
  
<h2>🧐 Features</h2>

Caracteristicas

*   Crear libros
*   Eliminar libros
*   Listar libros
*   Listar libros por titulo
*   Listar libros por autor
*   Buscar libros por id

<h2>🛠️ Installation Steps:</h2>

<p>1. Agregar archivo de environment</p>

Crear un archivo .env en la carpeta donde se encuentre el archivo docker-compose

```
AUTH_RSA_PUBLICKEY=<Llave publica RSA>
LIBRARY_MONGODB_CONNECTIONSTRING=mongodb://library.mongo:27017
LIBRARY_MONGODB_DATABASE_NAME=<Nombre bd>
LIBRARY_AWSS3_BUCKETNAME=<Nombre bucket S3>
LIBRARY_AWSS3_ACCESSKEY=<Llave acceso S3>
LIBRARY_AWSS3_SECRETKEY=<Secret key S3>
LIBRARY_PORT=6000

```

<p>2. Ejecutar Docker Compose</p>

```
docker compose up -d --build
```

<h2>💻 Built with</h2>

Tecnologias usadas en este proyecto:

*   ASP .NET 8
*   MongoDB