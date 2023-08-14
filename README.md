# Postman2CSharp

### Save yourself hours, days, or even weeks

[Postman2CSharp](https://postman2csharp.com) is a FREE tool that allows you to generate fully featured ApiClients by simply inputting your Postman collections. I hope this tool can make your development process easier, quicker, and more efficient. All generated ApiClient projects come fully packaged and organized and have no dependencies on outside libraries. (Unless you choose Newtonsoft.Json as your json library or choose to handle multiple types with OneOf<,>).

Generated ApiClients are highly configurable to suit your needs. Full explanations about configurations can be found [here](https://postman2csharp.com/ApiClient-Configurations-Explained).

## Introduction

I'm tired of making ApiClients, you're tired of making ApiClients, we're all tired of it. It's always the same bs boilerplate code. You gotta keep looking back and forth between postman and visual studio to make sure you don't forget anything. We've all been there. We'll now you don't have to. You can use postman2csharp and be sure that all request parts are included in the generated code.

This doesn't however mean that the generated ApiClient are production ready. You will still need to review and fix the generated code.

## Features

* Generates an ApiClient, ApiClient interface, and boilerplate test class
* Generates classes for Requests, Response, Formdata, and Query Parameters
* Choose your json library, System.Text.Json or Newtonsoft
   * Will affect extension methods used and attributes on properties of classes
* Keeps code clean and consistent
* Dedupes classes for you to try to make your life easier
  * Dedupe process is highly optimized and configurable
* Writes xml comments for you based on descriptions
* Configure error handling
   * Returns default or rethrow
   * Configure logging
   * Configure what exceptions to catch
* Choose whether or not to use cancellation tokens
* Configure all other settings found at [Json2CSharp](https://json2csharp.com/)
* Export the entire generated ApiClient project as a zip folder.
   * It is organized neatly
* Easily view generated code using UI
* Standalone json to C# classes converter
* And much much more...

## Samples

Samples are available in the source code of this repo [here](https://github.com/biegehydra/Postman2CSharp/tree/master/samples)

## Data Privacy

Your collections never leave your computer. The website is a standalone blazor wasm app and makes no http calls to a backend. 

## Contributions, Feature Requests, Feedback

Both the website and core library are open source so feel free to contribute or request a feature there.

## Road Map

As of 8=14=2023 the project is reaching a stable state. The ApiClient generator itself has reached a mature state and will likely not have many changes to its Api. I only have a few ideas left on improvement to this project.

* Add more documentation in code and on website
* Improve user interface
* Create system to add plugins which would allow other developers to modify existing code generation procedures. For example, to a plugin for more verbose error handling with Poly or a custom http client like RestClient instead of HttpClient.
* Improve site SEO

## Tech Stack

The website is a standalone blazor wasm app. It uses Prerendering via this amazing project [BlazorWasmPreRendering.Build](https://github.com/jsakamoto/BlazorWasmPreRendering.Build) by  jsakamoto. It is hosted and distributed with s3 and cloudfront.
