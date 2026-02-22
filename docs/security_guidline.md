# FinTrack Pro - Security Guidelines

## Authentication

- JWT-based authentication
- Token expiry: 60 minutes
- Refresh token expiry: 7 days

## Authorization

Role-based access:
- Admin
- Standard User
- Read-only Auditor

## Data Encryption

- Data in transit: TLS 1.2+
- Data at rest: AES-256

## Sensitive Data Handling

- Bank account numbers masked except last 4 digits
- No logs should contain:
  - Full account numbers
  - Access tokens
  - Passwords

## Security Incident Response

1. Identify breach scope
2. Revoke compromised tokens
3. Rotate secrets
4. Notify affected users within 24 hours
5. Document incident
