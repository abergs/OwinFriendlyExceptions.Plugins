# OwinFriendlyExceptions.Plugins

See [Owin Frienly Exceptions](https://github.com/abergs/OwinFriendlyExceptions) for the owin middleware. This repository contains different framework plugins.

## Web Api 2 plugin

Installation:  

`Install-package OwinFriendlyExceptions.Plugins.WebApi2`

1. `Install-package OwinFriendlyExceptions.Plugins.WebApi2`
2. `app.UseFriendlyExceptions(exceptionsToHandle, new [] {new WebApi2ExceptionProvider()});`
3. `config.Services.Replace(typeof(IExceptionHandler), new WebApi2ExceptionHandler(exceptionsToHandle));`
Install the package, and supply the WebApi Exception Provider to the OwinFriendlyExceptions extension method.
In order for the Plugin to get swallowed exceptions you have to replace the ExcepionHandler service in Web Api.
The plugin takes a list of which exceptions we can handle (that same collection fed to the middleware itself), so WebApi can still take care of unhandled exceptions for you.
