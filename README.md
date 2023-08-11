# PkceChallenge

This library is a direct port of the npm package [pkce-challenge](https://www.npmjs.com/package/pkce-challenge) to .NET7.

Generate or verify a Proof Key for Code Exchange (PKCE) challenge pair.

Read more about [PKCE](https://www.oauth.com/oauth2-servers/pkce/authorization-request/).

## Installation

```bash
dotnet add package PkceChallenge
```
## Usage
Default length for the verifier is 43:

```csharp
using static Pkce.Challenge.Pkce;

var challengePair = PkceChallenge();
```

Returns a `ChallengePair` record object with the following properties:

```csharp
public string CodeVerifier { get; init; }
public string CodeChallenge { get; init; }
```

And the json representation similar to:

```json
{
  "codeVerifier": "u1ta-MQ0e7TcpHjgz33M2DcBnOQu~aMGxuiZt0QMD1C",
  "codeChallenge": "CUZX5qE8Wvye6kS_SasIsa8MMxacJftmWdsIA_iKp3I"
}
```

### Specify a verifier length

```csharp
using static Pkce.Challenge.Pkce;

var challenge = PkceChallenge(128);
return challenge.Length == 128; // true
```

### Challenge verification

```csharp
using static Pkce.Challenge.Pkce;

return VerifyChallenge(challenge.CodeVerifier, challenge.CodeChallenge) == true; // true
```

### Challenge generation from existing code verifier

```csharp
using static Pkce.Challenge.Pkce;

return GenerateChallenge(challenge.CodeVerifier) == challenge.CodeChallenge; // true
```