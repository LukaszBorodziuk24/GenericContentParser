# Generic Content Parser

ASP.NET Core Web API application that provides a generic endpoint for parsing Base64 encoded content in different formats.

The application supports:

* CSV parsing
* Internal JSON parsing

The parser architecture is based on an abstraction (`IContentParser`), allowing new content formats to be added without modifying the existing API logic.

## Requirements

* .NET 10 SDK

## Running the application

Clone the repository:

```bash
git clone <repository-url>
```

Navigate to the project directory:

```bash
cd GenericContentParser
```

Restore dependencies:

```bash
dotnet restore
```

Run the application:

```bash
dotnet run
```

The API will be available at:

```
http://localhost:5294
```

## API Endpoint

### Parse content

```
POST /api/v1/parse-content
```

Required header:

```
Content-Type: application/json
```

### Request body

```json
{
  "type": "CSV",
  "content": "<base64 encoded content>"
}
```

Supported content types:

* `CSV`
* `INTERNAL_JSON`

The `content` field must contain Base64 encoded data.

## Example

CSV input:

```
name,age,city
John,25,London
Anna,30,Paris
```

Base64 encoded and sent as:

```json
{
  "type": "CSV",
  "content": "bmFtZSxhZ2UsY2l0eQpKb2huLDI1LExvbmRvbgpBbm5hLDMwLFBhcmlz"
}
```

Example response:

```json
{
  "status": "success",
  "count": 2,
  "data": [
    {
      "name": "John",
      "age": "25",
      "city": "London"
    },
    {
      "name": "Anna",
      "age": "30",
      "city": "Paris"
    }
  ]
}
```

## Project Structure

```
GenericContentParser
│
├── Controllers
│   └── ParserController.cs
│
├── Models
│   ├── ParseRequest.cs
│   ├── ParseResponse.cs
│   └── ContentType.cs
│
├── Services
│   ├── ContentParserService.cs
│   ├── IContentParser.cs
│   └── Parsers
│       ├── CsvParser.cs
│       └── InternalJsonParser.cs
│
└── Middleware
    └── ExceptionHandlingMiddleware.cs
```

## Architecture

The application uses a parser abstraction:

```
Controller
    |
    v
ContentParserService
    |
    v
IContentParser
    |
    +-------------+
    |             |
    v             v
CsvParser   InternalJsonParser
```

`ContentParserService` does not depend on concrete parser implementations. New formats can be added by implementing `IContentParser` and registering the new parser in dependency injection.
