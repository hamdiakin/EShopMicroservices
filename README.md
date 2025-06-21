# EShop Microservices

A microservices-based e-commerce platform built with .NET 8, using clean architecture principles and modern development practices.

## 🏗️ Architecture

This project follows a microservices architecture with the following components:

- **BuildingBlocks**: Shared libraries and common functionality
- **Catalog Service**: Product catalog management
- **PostgreSQL**: Primary database for the catalog service

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK
- Docker and Docker Compose
- Git

### Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/hamdiakin/EShopMicroservices.git
   cd EShopMicroservices
   ```

2. **Run with Docker Compose**
   ```bash
   cd src
   docker-compose up --build
   ```

3. **Access the API**
   - Catalog API: http://localhost:6000
   - Health Check: http://localhost:6000/health
   - PostgreSQL: localhost:5432

## 📋 Services

### Catalog API

The Catalog service manages product information and provides RESTful endpoints.

**Endpoints:**
- `GET /health` - Health check
- `GET /products` - Get all products
- `GET /products/{id}` - Get product by ID
- `GET /products/category/{category}` - Get products by category
- `POST /products` - Create new product
- `PUT /products/{id}` - Update product
- `DELETE /products/{id}` - Delete product

**Sample Product Data:**
The service comes pre-loaded with sample products including:
- iPhone X, Samsung 10, Huawei Plus
- Xiaomi Mi 9, HTC U11+ Plus, LG G7 ThinQ
- Panasonic Lumix

### Database

- **PostgreSQL** running on port 5432
- Database: `CatalogDb`
- Username: `postgres`
- Password: `postgres`

## 🛠️ Development

### Project Structure

```
src/
├── BuildingBlocks/
│   └── BuildingBlocks/
│       ├── Behaviours/
│       │   ├── LoggingBehaviour.cs
│       │   └── ValidationBehaviour.cs
│       ├── CQRS/
│       ├── Exceptions/
│       └── BuildingBlocks.csproj
├── Services/
│   └── Catalog/
│       └── Catalog.API/
│           ├── Data/
│           ├── Products/
│           ├── Dockerfile
│           └── Program.cs
└── docker-compose.yml
```

### Key Features

- **CQRS Pattern**: Command Query Responsibility Segregation
- **MediatR**: Mediator pattern implementation
- **Marten**: Document database for PostgreSQL
- **Carter**: Minimal API framework
- **Health Checks**: Built-in health monitoring
- **Logging**: Request logging with performance monitoring
- **Validation**: FluentValidation integration

### Docker Configuration

The project uses Docker Compose for easy development setup:

- **catalogdb**: PostgreSQL database with persistent storage
- **catalog.api**: .NET 8 API service
- **Port Mapping**: 
  - API: 6000 → 8080
  - Database: 5432 → 5432

## 🔧 Configuration

### Environment Variables

- `ASPNETCORE_ENVIRONMENT`: Development
- `ASPNETCORE_HTTP_PORTS`: 8080
- `ASPNETCORE_URLS`: http://+:8080
- `ConnectionStrings__CatalogConnection`: Database connection string

### Connection String Format

```
Server=catalogdb;Port=5432;Database=CatalogDb;User Id=postgres;Password=postgres;Include Error Detail=true
```

## 📊 Monitoring

### Health Checks

The API includes health checks for:
- Database connectivity (PostgreSQL)
- Overall service health

Access health status at: `http://localhost:6000/health`

### Logging

The application includes comprehensive logging:
- Request/response logging
- Performance monitoring
- Warning logs for requests taking > 3 seconds

## 🧪 Testing

### API Testing

You can test the API using:

```bash
# Health check
curl http://localhost:6000/health

# Get all products
curl http://localhost:6000/products

# Get product by ID
curl http://localhost:6000/products/5334c996-8457-4cf0-815c-ed2b77c4ff61
```

## 🚀 Deployment

### Docker Deployment

```bash
# Build and run
docker-compose up --build

# Run in background
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f catalog.api
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License.

## 🔗 Links

- [.NET 8 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Docker Documentation](https://docs.docker.com/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [Marten Documentation](https://martendb.io/)