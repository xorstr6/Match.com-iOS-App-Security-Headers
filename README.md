# Match.com-iOS-App-Security-Headers

**Install BouncyCastle from NuGet Packages for this to work**

Usage:

First you parse the token from the first request to /api/auth/anonymous
Then the token is used as key for encryption.
The data being encrypted is "email:password"

Ex. 

```csharp
var Encryption = Encrypt(Email + ":" + Password, token);
