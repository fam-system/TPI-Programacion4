# Trabajo Practico Programacion 4 - Grupo FAM

Desarrolladores: Avila Brian,
		 Franceschina Gonzalo,
		 Moccicafreddo Juan

## Descripción
API RESTful construida con .NET 8 (C# 12) para la gestión de Productos, Procesos, Archivos y Usuarios. 
La capa de dominio contiene entidades como Producto, Proceso ,Archivo y Usuario; existe una capa de Application con servicios y DTOs, 
y una capa de Infrastructure que persiste datos en MySQL mediante Entity Framework Core.

## Tecnologías
- .NET 8
- C# 12
- Entity Framework Core (MySQL)
- MySQL
- Arquitectura en capas(Clean Architecture) (Domain / Application / Infrastructure / Presentation)

## Estructura principal
- src/Domain - Entidades y contratos
- src/Application - Servicios, DTOs y lógica de negocio
- src/Infrastructure - Repositorios, servicios externos, JWT, DbContext y migraciones
- src/Presentation - API (controllers, Program.cs, configuración)

## Requisitos
- .NET 8 SDK
- MySQL Workbench o compatible
- __dotnet-ef__ para aplicar migraciones

## Configuración
1. Configurar la cadena de conexión en `src/Presentation/appsettings.json` o `src/Presentation/appsettings.Development.json` si se esta desarrollando:
   - key sugerida: `ConnectionStrings:DefaultConnection`
2. instalar herramientas de EF Core:
   - __dotnet tool install --global dotnet-ef__

   - En "Application":
	> BCrypt.Net-Next                                      4.0.3
	> Microsoft.Extensions.Configuration.Abstractions      8.0.0
	> System.IdentityModel.Tokens.Jwt                      7.1.2

   - En "Infrastructure":
	> BCrypt.Net-Next                           4.0.3
	> Microsoft.EntityFrameworkCore             8.0.13
	> Microsoft.EntityFrameworkCore.Design      8.0.13
	> Newtonsoft.Json                           13.0.4
	> Pomelo.EntityFrameworkCore.MySql          8.0.3

   - En "Presentation":
   	> Microsoft.AspNetCore.Authentication.JwtBearer      8.0.21
   	> Swashbuckle.AspNetCore                             6.6.2

## Migraciones y base de datos
Desde la raíz del proyecto ejecutar (ajustar rutas si es necesario):
- Restaurar y compilar:
  - __dotnet restore__
  - __dotnet build__
- Aplicar migraciones:
  - dotnet ef migrations add InitialMigration --context ApplicationDbcontext --startup-project src/Presentation --project src/Infrastructure -o Data/Migrations
  - dotnet ef database update --context ApplicationDbContext --startup-project src/Presentation --project src/Infrastructure
  (o usar el explorador de migraciones en Visual Studio).

## Paso necesario para habilitar la consulta de los Endpoints

Esta Api usa el Rol de autorizacion de "Oficina" para poder crear usuarios, por lo tanto, depues de hacer la migracion
si la base de datos no tiene un usuario precargado con el Rol "Oficina" no va a poder crear usuarios y por lo tanto no
va a poder usar ninguno de los Endpoints restringidos.

En este punto debemos hacer un INSERT directamente en la base de datos que tenga el Rol de "Oficina", para asi, poder 
autentificarse para habilitar la carga de datos.

Con el siguiente codigo podemos lograr este INSERT en la base:

"Nombre de la base usada en la migracion"
use api_tpí;
INSERT INTO usuarios (
    Id,
    Nombre,
    Apellido,
    Dni,
    Direccion,
    Telefono,
    FechaIngreso,
    NombreUsuario,
    PasswordHash,
    Estado,
    Rol
)
VALUES (
    1,
    'oficina',
    'oficina',
    '11111111',
    'delfo 1111',
    '1111111111',
    '2025-10-16 20:28:25.384000',
    'oficina1',
    '$2a$11$vJBflic0KzLwuzrEDoUQu.93MbH5H0u75OWIGpX6EDqVjpbrY0lnO',
    0,
    0
);

## Endpoints principales
Rutas REST típicas:
Archivo:
	-GET /api/Archivo 		(Roles Permitidos: Oficina)
	-POST /api/Archivo 		(Roles Permitidos: Oficina)
	-GET /api/Archivo/{id} 		(Roles Permitidos: Oficina, Encargado, Operario)
	-PUT /api/Archivo/{id} 		(Roles Permitidos: Oficina)
	-DELETE /api/Archivo/{id} 	(Roles Permitidos: Oficina)
Authetication:
	-POST /api/authentication/login	(No requiere autorizacion)
Jokes:
	-GET /api/Jokes			(No requiere autorizacion)
Proceso:
	-POST /api/Proceso 		(Roles Permitidos: Oficina)
	-GET /api/Proceso 		(Roles Permitidos: Oficina, Encargado)
	-GET /api/Proceso/{id} 		(Roles Permitidos: Oficina, Encargado, Operario)
	-DELETE /api/Proceso/{id} 	(Roles Permitidos: Oficina)
	-PUT /api/Proceso/Start/{id} 	(Roles Permitidos: Operario)
	-PUT /api/Proceso/End/{id} 	(Roles Permitidos: Operario)
Producto:
	-GET /api/Producto 		(Roles Permitidos: Oficina)
	-GET /api/Producto/{id} 	(Roles Permitidos: Oficina, Encargado, Operario)
	-POST /api/Producto 		(Roles Permitidos: Oficina)
	-PUT /api/Producto/{id} 	(Roles Permitidos: Oficina)
	-DELETE /api/Producto/{id} 	(Roles Permitidos: Oficina)
Usuario:
	-GET /api/Usuarios		(Roles Permitidos: Oficina)
	-POST /api/Usuarios		(Roles Permitidos: Oficina)
	-GET /api/Usuarios/Activos 	(Roles Permitidos: Oficina)
	-GET /api/Usuarios/{id}		(Roles Permitidos: Oficina, Encargado)
	-PUT /api/Usuarios/{id}		(Roles Permitidos: Oficina)
	-DELETE /api/Usuarios/{id}	(Roles Permitidos: Oficina)

## Buenas prácticas / notas
- Mantener las migraciones en sync con el modelo; usar __dotnet ef migrations add <Name>__ antes de aplicar cambios.
- Usar DTOs para separar la API del modelo de dominio.
- Manejar excepciones y devolver códigos HTTP adecuados desde los controllers.

## Licencia

MIT License

Copyright (c) 2025 Grupo FAM

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

---