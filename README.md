# personapi-dotnet

## Descripción general
Este proyecto corresponde al Laboratorio .NET – Arquitectura de Software, cuyo objetivo es implementar un monolito MVC en ASP.NET Core 7 con Entity Framework Core y SQL Server Express, aplicando buenas prácticas de desarrollo, separación por capas y documentación con Swagger.

El sistema gestiona una base de datos llamada persona_db con las siguientes entidades:
- Persona
- Profesion
- Telefono
- Estudios

Cada entidad cuenta con un CRUD completo accesible a través de la API REST y probado mediante Swagger.

## Stack tecnológico

| Componente | Tecnología |
|-------------|-------------|
| Lenguaje | C# (.NET 7) |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core 7 |
| Base de datos | SQL Server 2022 Express |
| Documentación API | Swagger / Swashbuckle |
| IDE recomendado | Visual Studio 2022 Community |

## Requisitos previos

- Visual Studio Community 2022 (con las cargas de trabajo de ASP.NET y desarrollo de bases de datos)
- SQL Server Express 2019 o 2022
- SQL Server Management Studio (SSMS)
- .NET SDK 7.0 o superior

## Configuración de la base de datos

1. Crear la base de datos en SQL Server (ddl.sql):
   ```sql
   CREATE DATABASE persona_db;
   GO
   ```

2. Ejecutar el script ddl.sql o el correspondiente DDL que crea las tablas:
   - persona
   - profesion
   - telefono
   - estudios

3. Verificar la creación:
   ```sql
   USE persona_db;
   SELECT * FROM persona;
   ```

## Configuración del proyecto

1. Clona este repositorio:
   ```bash
   git clone https://github.com/CIFU1104/personapi-dotnet.git
   cd personapi-dotnet
   ```

2. Verifica el archivo appsettings.json:
   ```json
   {
     "ConnectionStrings": {
       "PersonaDb": "Server=JUAN_CIFUENTES\\SQLEXPRESS01;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true"
     }
   }
   ```
   Reemplazar JUAN_CIFUENTES con el usuario del sistema, tambien verificar si debe ir SQLEXPRESS o SQLEXPRESS01, dependiendo de como se haya instalado el SQL SERVER 

3. Instala los paquetes NuGet:
   ```powershell
   Install-Package Microsoft.EntityFrameworkCore 
   Install-Package Microsoft.EntityFrameworkCore.SqlServer 
   Install-Package Microsoft.EntityFrameworkCore.Tools 
   Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design 
   ```

## Generación de entidades con Scaffold

El modelo de datos se generó desde la base de datos con el siguiente comando:

```powershell
Scaffold-DbContext "Server=JUAN_CIFUENTES\SQLEXPRESS01;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Entities -ContextDir Data -Context PersonaDbContext -UseDatabaseNames -Force
```
Reemplazar JUAN_CIFUENTES con el usuario del sistema, tambien verificar si debe ir SQLEXPRESS o SQLEXPRESS01, dependiendo de como se haya instalado el SQL SERVER 


Esto crea:
```
Data/PersonaDbContext.cs
Models/Entities/Persona.cs
Models/Entities/Profesion.cs
Models/Entities/Telefono.cs
Models/Entities/Estudios.cs
```

## Ejecución del proyecto

1. Abre la solución en Visual Studio.  
2. Establece personapi-dotnet como proyecto de inicio.  
3. Ejecuta con Ctrl + F5 o el botón “Run”.  
4. Abre Swagger en el navegador:  
   ```
   http://localhost:5086/swagger
   ```

## Endpoints principales

### PersonaController (/api/persona)

| Método | Endpoint | Descripción |
|---------|-----------|-------------|
| GET | /api/persona | Listar todas las personas |
| GET | /api/persona/{cc} | Obtener persona por cédula |
| POST | /api/persona | Crear persona |
| PUT | /api/persona/{cc} | Actualizar persona |
| DELETE | /api/persona/{cc} | Eliminar persona |

**Ejemplo JSON (POST/PUT):**
```json
{
  "cc": 10123456,
  "nombre": "Juan",
  "apellido": "Pérez",
  "genero": "M",
  "edad": 25
}
```

### ProfesionController (/api/profesion)

| Método | Endpoint | Descripción |
|---------|-----------|-------------|
| GET | /api/profesion | Listar profesiones |
| POST | /api/profesion | Crear nueva profesión |
| PUT | /api/profesion/{id} | Actualizar |
| DELETE | /api/profesion/{id} | Eliminar |

**Ejemplo JSON (POST):**
```json
{
  "id": 2,
  "nom": "Ingeniero de Software",
  "des": "Diseño y desarrollo de software"
}
```

### TelefonoController (/api/telefono)

| Método | Endpoint | Descripción |
|---------|-----------|-------------|
| GET | /api/telefono | Listar teléfonos |
| POST | /api/telefono | Crear teléfono |
| PUT | /api/telefono/{num} | Actualizar |
| DELETE | /api/telefono/{num} | Eliminar |

**Ejemplo JSON (POST):**
```json
{
  "num": "3105551234",
  "oper": "Claro",
  "duenio": 10123456
}
```

### EstudiosController (/api/estudios)

| Método | Endpoint | Descripción |
|---------|-----------|-------------|
| GET | /api/estudios | Listar todos los estudios |
| GET | /api/estudios/{idProf}/{ccPer} | Obtener un estudio |
| POST | /api/estudios | Crear estudio |
| PUT | /api/estudios/{idProf}/{ccPer} | Actualizar estudio |
| DELETE | /api/estudios/{idProf}/{ccPer} | Eliminar estudio |

**Ejemplo JSON (POST):**
```json
{
  "idProf": 2,
  "ccPer": 10123456,
  "fecha": "2025-11-04T00:00:00",
  "univer": "Pontificia Universidad Javeriana"
}
```

## Flujo de pruebas sugerido en Swagger

1. POST /api/profesion → crea una profesión.  
2. POST /api/persona → registra una persona.  
3. POST /api/telefono → asocia un teléfono al dueño.  
4. POST /api/estudios → crea el estudio con la persona y profesión anteriores.  
5. GET /api/ → verifica los datos creados.  
6. PUT /api/ → actualiza un registro.  
7. DELETE /api/ → elimina una entidad.

## Estructura del proyecto

```
personapi-dotnet/
│
├── Controllers/
│   ├── Api/
│   │   ├── PersonaController.cs
│   │   ├── ProfesionController.cs
│   │   ├── TelefonoController.cs
│   │   └── EstudiosController.cs
│   └── HomeController.cs
│
├── Data/
│   └── PersonaDbContext.cs
│
├── Models/
│   └── Entities/
│       ├── Persona.cs
│       ├── Profesion.cs
│       ├── Telefono.cs
│       └── Estudios.cs
│
├── Views/
│   ├── Home/
│   └── Shared/
│
├── appsettings.json
├── Program.cs
└── personapi-dotnet.csproj
```

## Conclusiones

- Se construyó un sistema monolítico MVC basado en ASP.NET Core.  
- Se implementó acceso a datos con Entity Framework Core mediante DbContext.  
- Se logró exposición de servicios REST documentados en Swagger.  
- Se aplicó separación por capas y buenas prácticas de arquitectura.  
- El sistema es fácilmente escalable hacia una arquitectura distribuida.

## Referencias

- https://learn.microsoft.com/en-us/ef/core/
- https://learn.microsoft.com/en-us/aspnet/core/mvc/overview
- https://www.hdeleon.net/
- https://github.com/vguaman/

Autor: Juan Cifuentes, Sebastian Novoa, Daniel Cordoba 
Pontificia Universidad Javeriana  
Arquitectura de Software – Laboratorio .NET MVC
