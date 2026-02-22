> ⚠️ This is a demo project for educational and architectural demonstration purposes.

# HelpDeskCopilot (.NET AI + RAG Demo)

A practical AI integration project built with ASP.NET Core demonstrating:

- Secure OpenAI integration (user-secrets)
- Structured JSON output with validation + repair retry
- SQLite persistence + response caching
- Document ingestion + chunking
- Embeddings + cosine similarity search
- RAG (Retrieval-Augmented Generation)
- Prompt injection resistance
- Grounded ticket summarization using internal runbooks

This project demonstrates how to build a production-minded AI backend in .NET.

---

# Architecture Overview

User Request  
→ Retrieve relevant document chunks (semantic search via embeddings)  
→ Inject chunks into prompt  
→ Generate structured response  
→ Return answer + sources  

Key Components:

- Minimal API (.NET)
- EF Core + SQLite
- OpenAI Chat API
- OpenAI Embeddings API
- Local document ingestion (`docs/` folder)

---

# Tech Stack

- .NET Web API (Minimal APIs)
- OpenAI (Chat + Embeddings)
- SQLite (local persistence)
- EF Core
- Swagger (OpenAPI)

---

# Features

## Health & Connectivity
- `GET /health`
- `GET /test-ai`

## Structured Ticket Summarization
- `POST /summarize-ticket`
- JSON schema enforced
- Retry if invalid JSON
- Cached by SHA-256 hash

## Document Ingestion
- `POST /docs/ingest`
- Reads `.md` and `.txt` files from `/docs`
- Chunks with overlap
- Generates embeddings
- Stores in SQLite

## Semantic Search
- `GET /docs/search?query=...&take=5`
- Embeds query
- Computes cosine similarity
- Returns ranked chunks

## RAG Question Answering
- `GET /ask?question=...`
- Retrieves top chunks
- Applies threshold + context budget
- Prompt injection resistant
- Returns answer + sources

## RAG Ticket Summarization
- `POST /summarize-ticket-rag`
- Retrieves relevant runbook docs
- Produces structured JSON response
- Grounded in internal documentation

---

# Setup

## Prerequisites

- .NET SDK installed
- OpenAI API key with billing enabled

---

## 1. Restore

```bash
dotnet restore
```

---

## 2. Configure User Secrets

Navigate to the API project folder:

```bash
dotnet user-secrets init
```

Set secrets:

```bash
dotnet user-secrets set "AI:ApiKey" "YOUR_OPENAI_API_KEY"
dotnet user-secrets set "AI:BaseUrl" "https://api.openai.com/v1"
dotnet user-secrets set "AI:Model" "gpt-4o-mini"
dotnet user-secrets set "AI:EmbeddingModel" "text-embedding-3-small"
```

---

## 3. Run the API

```bash
dotnet run --project HelpDeskCopilot.Api
```

Open Swagger:

```
https://localhost:<port>/swagger
```

---

# Usage Guide

## Step 1: Add Documents

Place `.md` or `.txt` files in:

```
/docs
```

Example files:
- oncall.txt
- architecture.md
- faq.md
- security_guideline.md

---

## Step 2: Ingest Documents

```
POST /docs/ingest
```

This:
- Deletes previous chunks
- Rebuilds embeddings
- Stores vectors in SQLite

---

## Step 3: Test Semantic Search

```
GET /docs/search?query=sync failing&take=5
```

---

## Step 4: Ask Grounded Questions

```
GET /ask?question=What should I check if sync is failing?
```

Unrelated question:

```
GET /ask?question=Who is the CEO of Google?
```

Expected result:

```
Not found in docs.
```

---

## Step 5: RAG Ticket Summarization

```
POST /summarize-ticket-rag
```

Body:

```json
{
  "ticketText": "Multiple users reporting bank sync failures and API latency spikes."
}
```

Returns:

- summary
- key_points
- action_items
- draft_reply
- sources used

---

# Security Considerations

- User secrets for API keys
- Threshold filtering of weak retrieval
- Context size budgeting
- Prompt injection resistance
- Deterministic fallback when no relevant context
- JSON schema enforcement with retry repair

---

# Demo Scope Limitations

- Full reindex ingestion (not incremental)
- Embeddings stored as JSON (not vector DB)
- Cosine similarity computed in-memory
- No authentication layer

---

# Production Upgrade Path

- Use EF Core migrations
- Use pgvector or Azure AI Search
- Token-based context budgeting
- Add rate limiting
- Add structured output schema enforcement
- Add telemetry and logging
- Background ingestion job

---

# What This Project Demonstrates

- End-to-end AI backend integration in .NET
- Practical RAG implementation
- Secure AI usage patterns
- Prompt injection mitigation
- Structured output enforcement
- Caching + persistence
- Production-oriented architecture

---