# LocalBlockchain

![LocalBlockchain Banner](./docs/images/banner.png)

A lightweight local blockchain built with **.NET** for educational and experimental purposes.  
The goal is to simulate immutable transaction records, similar to a minimal digital ledger or "digital notary".

---

## 🚀 Features

- Chained block generation
- Transaction registration
- Public / Private key signing
- REST API
- Local persistence (SQLite)
- Designed for future scalability (GraphQL, RabbitMQ, etc.)

---

## 🧠 Concept

This project simulates a **local blockchain**, where each block contains:

- Index
- Hash
- Previous Hash
- Nonce
- Transactions list
- Timestamp

Use cases:

- Educational experiments
- Asset ownership simulation
- Contract recording
- Architecture prototyping

---

## 🛠 Technologies

- .NET 9 / .NET 10
- ASP.NET Core Web API
- SQLite
- Entity Framework (optional)
- xUnit / NUnit

---

## 📁 Project Structure

    LocalBlockchain/
    ├─ Properties/
    ├─ Controllers/
    ├─ Dto/
    ├─ Services/
    ├─ └─ Models/
    ├─ Program.cs
    └─ LocalBlockchain.csproj

---

## ⚙️ Setup

### 1. Clone

```bash
git clone https://github.com/m4rkos/local-blockchain.git
cd local-blockchain
dotnet restore
dotnet run

API will be available at: http://localhost:5000
```

#### Get All Blocks and Transactions
```
GET /api/blockchain/get-blocks

Json Response:
[
    {
        "index":0,
        "hash":"000104D32E25A40C183CD2FE31D85B385C41C9C9121D293F9DCBAC5D1EFEA116",
        "prev":"0",
        "nonce":4176,
        "data":[
            {
                "msg":"Genesis Block",
                "amount":0,
                "from":"UNKNOWN",
                "to":"UNKNOWN",
                "currency":"ALL",
                "signature":null,
                "publicKey":null
            }
        ],
        "timestamp":"2026-02-08T19:36:46.5148185Z"
    }
]
```

#### Add Transactions

```
POST /api/blockchain/add-transaction

Headers:
- string: pub	"Public Key"
- string: priv	"Private Key"

Json Body: 
[
  {
    "msg": "Send Transaction",
    "amount": 1.0,
    "from": "Marcos",
    "to": "John",
    "currency": "ETH"
  }
]
```

## 🔐 Signature

The system generates:

- Public Key
- Private Key

These keys validate transaction authenticity.

## 💾 Persistence

Currently uses SQLite for local storage.
The database file path can be configured via connection string.

Future possibilities:

- PostgreSQL
- MongoDB
- Neo4j (graph visualization)

---

### Author **Marcos Eduardo**  