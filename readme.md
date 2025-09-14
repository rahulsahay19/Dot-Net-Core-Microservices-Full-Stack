# 🚀 .NET Core Microservices with Saga, Outbox, RabbitMQ & Secure Payments  

Welcome to the **official repository** of our **advanced microservices course**.  
This project is a **real-world e-commerce application** built with **.NET 9, RabbitMQ, SQL Server, Redis, PostgreSQL, and Docker**.  

We go **beyond CRUD APIs** — this course teaches you how to build **enterprise-grade, event-driven, cloud-native microservices** with **Saga and Outbox patterns** for **transactional consistency**.

## Subscribe here:- https://www.udemy.com/course/building-amazon-style-full-stack-microservices/?couponCode=D06FD2086EDAD9583B47

## 🏗 Architecture Overview  

![Image](https://github.com/user-attachments/assets/3ae78952-e8cf-44ab-bdc0-1d45fe577657)

## 📡 Application Flow

![Image](https://github.com/user-attachments/assets/daf29083-5fd8-4947-ba27-1b68b21806db)

![alt text](https://github.com/user-attachments/assets/37a0c875-0e35-4382-81c4-1b3f44e41d3d)

Catalog exposes products → consumed by Basket.

Basket stores items in Redis → user checks out.

Ordering receives checkout event → creates order using CQRS.

Outbox Pattern ensures order event is published reliably.

Saga Orchestrator coordinates Payment.

If success ✅ → Order Confirmed.

If failure ❌ → Compensation triggered (rollback).

RabbitMQ transports events like OrderStarted, PaymentSucceeded, PaymentFailed.

Identity Service secures APIs with JWT.

Angular Frontend provides seamless end-to-end shopping experience.

## Solution Overview:

![Image](https://github.com/user-attachments/assets/b4bae010-3dbc-4d6d-b843-109ae5d20957)
![Image](https://github.com/user-attachments/assets/a0cb1e39-081e-4415-bfb5-115122751457)

🧩 Patterns & Practices Implemented

✔ CQRS (Command Query Responsibility Segregation)
✔ Saga Pattern (Distributed Transactions)
✔ Outbox Pattern (Reliable Messaging)
✔ Repository & Specification Pattern
✔ Factory & Domain Events
✔ Event-Driven Communication with RabbitMQ
✔ Polyglot Persistence (SQL Server, PostgreSQL, Redis)
✔ Containerized Deployment with Docker

🛠 Tech Stack

Backend: .NET 8, ASP.NET Core WebAPI, gRPC

Database: SQL Server, PostgreSQL, Redis

Messaging: RabbitMQ

Security: Identity Microservice (JWT)

Containerization: Docker, Docker Compose

Frontend: Angular 20 (Phase 2)

Cloud Ready: Kubernetes, Azure CI/CD, Service Mesh (Phase 3)

## Code Structure:- 
![image](https://github.com/user-attachments/assets/9914fe05-aadf-42fb-8ab3-3b39e7f30433)
## Frontend Flow
![image](https://github.com/user-attachments/assets/e5cd42f4-8955-42d7-8678-d9b605aa7035)

![image](https://github.com/user-attachments/assets/a454083d-0277-49b5-925a-04aefc919a79)

![image](https://github.com/user-attachments/assets/26e70dbb-96b7-47b4-8020-a4c006bd6a19)

![image](https://github.com/user-attachments/assets/2f2a118e-a087-44f1-bbf4-495ff3837784)

![image](https://github.com/user-attachments/assets/563278fd-21c1-4696-9052-e7f69a5c0648)

![image](https://github.com/user-attachments/assets/ddcaa331-e362-4b33-a25a-56a243c98fcc)

![image](https://github.com/user-attachments/assets/1dc90af2-b3ad-47fd-a499-2ba1ccfbb87c)

![image](https://github.com/user-attachments/assets/9b9f2365-ae10-43c6-9cc2-47de229927fa)

![image](https://github.com/user-attachments/assets/6e1b5eb5-24f4-4927-b1b9-d093d85f88c7)

![image](https://github.com/user-attachments/assets/b32feec0-3aeb-44db-8336-c81b5cc78ac3)

![image](https://github.com/user-attachments/assets/47c76428-008d-4cf4-ad2d-66598a140124)

![image](https://github.com/user-attachments/assets/ab7bf624-88f7-4ff8-85ef-db8c4026e413)

![image](https://github.com/user-attachments/assets/a34971bf-d04f-410c-b9c4-1bf75f183a63)

![image](https://github.com/user-attachments/assets/dd178589-fe17-40a6-9972-86e6f4385be0)

## Docker Commands
Docker commands to help you with different dbs and services during the development process.

```Docker Commands
docker run -d --name mongodb -p 27017:27017 -e mongo:latest

docker run -d --name redis-server -p 6379:6379 redis

docker run --name discount-postgres -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=Password@1 -e POSTGRES_DB=DiscountDb -p 5432:5432 -d postgres

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Rahul1234567" -p 1433:1433 --name order-sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

docker run -d --hostname rabbitmq-host --name rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Rahul1234567" -p 1434:1433 --name identitydb -d mcr.microsoft.com/mssql/server:2022-latest

docker run -d --name elasticsearch -p 9200:9200 -e "discovery.type=single-node" -e "xpack.security.enabled=false" docker.elastic.co/elasticsearch/elasticsearch:8.13.4

docker run -d --name kibana --link elasticsearch:elasticsearch -p 5601:5601 -e "ELASTICSEARCH_HOSTS=http://elasticsearch:9200" docker.elastic.co/kibana/kibana:8.13.4

```

## Docker-Compose 

```
docker-compose up -d
```

📚 Course Structure
🔹 Phase 1: Backend Microservices Development

Catalog, Basket, Discount, Ordering, Payment, Identity

CQRS, Outbox, Saga, RabbitMQ messaging

🔹 Phase 2: Frontend Development

Angular 20, API Gateway, Secure Integration

🔹 Phase 3: Infra & Cloud-Native Journey

Docker & Kubernetes

Azure Deployment & CI/CD pipelines

Observability with Prometheus, Grafana

Service Mesh (Istio/Linkerd)

🎯 Why Take This Course?

✅ 31+ Hours of Hands-On Content
✅ Build a Real-World E-Commerce Platform
✅ Master Advanced Patterns: Saga + Outbox
✅ Enterprise-Grade Event-Driven Design
✅ Polyglot Persistence in Action
✅ Cloud Native Ready
✅ Amazon like UI using Angular 20 

🔗 [Follow me on LinkedIn](https://www.linkedin.com/in/rahulsahay19/)
