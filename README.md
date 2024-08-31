
# API de Gestión de stock - GYF Challenge
Esta API permite gestionar el stock de productos, utilizando autenticación de usuarios para asegurar que solo usuarios autenticados puedan acceder a las funcionalidades de gestión de productos.



## Requerimientos Previos:

- NET 8 SDK
- SQL Server
- Herramienta para solicitudes HTTP (Postman, Swagger UI, etc.)
- Clonar este repositorio:
```sh
 git clone https://github.com/tu_usuario/tu_repositorio.git
```


## Tablas: 
La API cuenta con dos tablas principales: **Users** y **Products**.
### Products

| Id  | Price         | Date | Category |
|-----|---------------|------|----------|
| int | decimal(10,2) | date | int      |
| not null | not null      | not null | not null |

*El campo Category solo tiene dos posibles valores: **0** y **1**.

**ejemplo de cuerpo de solicitud:**
```
{
  "price": 5000,
  "date": "2024-01-22",
  "category": 0
}
```



### Users
| Id       | Name         | Email         | Password      | Token         |
|----------|--------------|---------------|---------------|---------------|
| int      | nvarchar(50) | nvarchar(100) | nvarchar(100) | nvarchar(MAX) |
| not null | not null     | not null      | not null      | null          |

**ejemplo de cuerpo de solicitud:**
```
{
  "name": "userName",
  "email": "userEmail@some.com",
  "password": "userPassword1"
}
```


## Endpoints:

#### Access

```http
  POST /api/Access/sign-up
```
```http
  GET /api/Access/login
```

#### Product

```http
  GET /api/Product/get-all
```
```http
  GET /api/Product/get-by-id{id}
```
```http
  GET /api/Product/filter-by-budget
```
```http
  POST /api/Product/add-product
```
```http
  PUT /api/Product/update{id}
```
```http
  DELETE /api/Product/delete{id}
```


# Configuración del Proyecto

Este proyecto utiliza archivos de configuración que no están incluidos en el repositorio por razones de seguridad. Para que el proyecto funcione correctamente en tu entorno local, necesitarás crear estos archivos manualmente.

## Archivos de Configuración

Asegúrate de crear los siguientes archivos en la raíz de tu proyecto:

### `appsettings.json`

Este archivo contiene la configuración general del proyecto. Puedes crear un archivo `appsettings.json` en la raíz del proyecto con el siguiente contenido de ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=userServer; Database=DatabaseName; TrustServerCertificate=True; Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "UserKey1234",
    "Issuer": "YourUserAuthorizationIssuer",
    "Audience": "YourUsersAudience",
    "ExpireDays": 5
  }
}

```



#### Archivo para inserción de datos en Sql:

*SqlDataFake.txt*


