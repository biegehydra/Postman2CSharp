# Postman2CSharp

### Save yourself hours, days, or even weeks

[Postman2CSharp](https://postman2csharp.com) is a FREE tool that allows you to generate fully featured ApiClients by simply inputting your Postman collection json. All generated ApiClient projects come fully packaged and organized and have no dependencies on outside libraries.

Generated ApiClients are highly configurable to suit your needs.

## Features

* Generates an ApiClient service, interface, and boilerplate test class
* Generates classes for Requests, Responses, Formdata, Query Parameters, and GraphQL Parameters
* Choose your json library, System.Text.Json or Newtonsoft
* Keeps code clean and consistent
* Dedupes classes for you to try to make your life easier
  * Dedupe process is highly optimized and configurable
* Writes xml comments for you based on descriptions
* Configure exception handling and logging
* Support for cancellation tokens
* Export the entire generated ApiClient project as a neatly organized zip folder.
* Easily view generated code using UI
* Standalone json to C# classes converter
* And much much more...

## Samples

Samples are available in the source code of this repo [here](https://github.com/biegehydra/Postman2CSharp/tree/master/samples)

## Data Privacy

Your collections never leave your computer. The website is a standalone blazor wasm app and makes no calls to a backend. 

## Latest Fixes and Improvements
* Add guided tours to improve user experience
* Fix issues when the url port has a variable `http://localhost:{{port}}/path"
* GraphQL
  * Generate paramaters class from parameters json
  * Optionally put graph ql queries in a seperate file from the service
* Add search feature to the tree on the collection page
* Add option to flatten collection hierarchy
* Add support for `Head` and `Option` http types
* Group responses by response type for better code generations
```
if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
{
    return await response.ReadJsonAsync<BadRequestResponse>();
}
```
* Remove async/await on functions that don't need it
```
public Task<PlaceDetailsResponse> PlaceDetails(PlaceDetailsParameters queryParameters)
{
    return _httpClient.GetFromJsonAsync<PlaceDetailsResponse>("$"details/json"");
}
```

## Contributions, Feature Requests, Feedback

Both the website and core library are open source so feel free to contribute or request a feature there.

## Road Map

As of 8-14-2023 the project is reaching a stable state. The ApiClient generator itself has reached a mature state and will likely not have many changes to its Api. I only have a few ideas left on improvement to this project.

* Add more documentation in code and on website
* Improve user interface
* Create system to add plugins which would allow other developers to modify existing code generation procedures. For example, to a plugin for more verbose error handling with Poly or a custom http client like RestClient instead of HttpClient.
* Improve site SEO

## Tech Stack

The website is a standalone blazor wasm app. It uses Prerendering via this amazing project [BlazorWasmPreRendering.Build](https://github.com/jsakamoto/BlazorWasmPreRendering.Build) by  jsakamoto. It uses guided tours through a fork of [Blazor.DriverJs](https://github.com/ilsadq/Blazor.DriverJs). Class generation uses a heavily modified fork of [Json2CSharp](https://github.com/Json2CSharp/Json2CSharpCodeGenerator) It is hosted and distributed with s3 and cloudfront.
