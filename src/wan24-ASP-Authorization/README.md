# wan24-ASP-Authorization

This library helps with ASP.NET authorization in case you'd like to use 
dynamic authorization (authZ) attributes.

**NOTE**: Authentication (authN) is required to be setup - otherwise ASP.NET 
won't authorize anything and fail.

## How to get it

This library is available as 
[NuGet package](https://www.nuget.org/packages/wan24-ASP-Authorization/).

## Usage

```cs
var builder = WebApplication.CreateBuilder(args);
...
// Sadly authorization won't work without authentication
builder.Services.AddAuthentication...;
...
// Will call builder.Services.AddAuthorization(), too
builder.AddWan24AuthZ();
...
var app = builder.Build();
...
// Mandatory..
app.UseAuthentication();
...
// Authorize after authentication (and routing, etc.)
app.UseAuthorization();
...
app.Run();
```

Or to use a fake authentication handler, which authenticates **EVERYTHING**:

```cs
var builder = WebApplication.CreateBuilder(args);
...
// CAUTION: Authenticates EVERYTHING!
builder.AddWan24FakeAuthN();
...
// Will call builder.Services.AddAuthorization(), too
builder.AddWan24AuthZ();
...
var app = builder.Build();
...
// Mandatory..
app.UseAuthentication();
...
// Authorize after authentication (and routing, etc.)
app.UseAuthorization();
...
app.Run();
```

**CAUTION**: The fake authentication handler authenticates EVERYTHING and 
makes it possible to use authorization in case you don't want to use 
authentication.

### Create a custom authorization attribute

Your custom authorization attribute:

```cs
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class YourAuthZAttribute() : AuthZAttributeBase()
{
	public override Task<bool?> HandleRequirementAsync(
		AuthorizationHandlerContext context, 
		HttpContext? httpContext, 
		AuthZRequirement requirement
		)
	{
		// Handle your authorization logic based on the given context informations and
		// return NULL, if you've called context.Succeed/Fail from within this method
		// return TRUE, if authorized
		// return FALSE, if unauthorized
	}
}
```

Usage in an API controller:

```cs
[YourAuthZAttribute]
public class YourController : ControllerBase
{
	...
}
```

### Factory authorization attributes

These attributes are available from the `wan24.ASP.Authorization.Attributes` 
namespace:

#### Authorize networks

Using the `NetworksAttribute` you can authorize access based on the peers IP 
address (taken from `HttpContext.Connection.RemoteIpAddress`):

```cs
[Networks("127.0.0.1/32")]
```

The networks must be CIDR notated. You can also unauthorize specific networks 
using this attribute:

```cs
[Networks(deny: true, "127.0.0.1/32")]
```

**NOTE**: Multiple attributes are allowed.
