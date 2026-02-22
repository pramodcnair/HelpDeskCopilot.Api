# FinTrack Pro - Frequently Asked Questions

## 1. How do I reset my password?
Go to the login page and click "Forgot Password". 
You will receive a reset link valid for 15 minutes.

## 2. Why is my bank sync failing?
Common reasons:
- Expired bank authentication token
- Bank API downtime
- Multi-factor authentication required

To fix:
1. Go to Settings > Bank Connections
2. Reconnect your bank
3. Complete MFA if prompted

## 3. How often does data sync?
Bank data sync runs:
- Automatically every 6 hours
- Manually when user clicks "Sync Now"

## 4. What happens if a transaction is duplicated?
The system uses transaction ID + timestamp hashing.
Duplicates are automatically ignored.

## 5. Can I export my reports?
Yes. Reports can be exported as:
- CSV
- PDF
- Excel

## 6. How long is my data stored?
User financial data is stored for:
- Active accounts: Unlimited
- Deleted accounts: 30 days retention
