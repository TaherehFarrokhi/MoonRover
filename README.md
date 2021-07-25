# MoonRover

The repo contains the MoonRover API that facilitate control of a Pluto Rover.

The project has been developed on .Net 5 so in order to build/run the project .net 5.0 SDK/runtime need to be installed. You can download them from https://dotnet.microsoft.com/download/dotnet/5.0 depend on your operating system and also your hardware configuration.

## How to build

In order to build the project:

```

git clone https://github.com/TaherehFarrokhi/MoonRover

cd MoonRover.Cli

dotnet build
dotnet run
 
```

### API

The API contains the controller interface for sending command to the PlutoRover. Here is an example of how to use it:

```csharp

var environment = new PlutoEnvironment(100, 100);
var controller = new PlutoController(environment, new DirectionCalculator(), new LocationCalculator(environment));

controller.ExecuteCommand("command");

```
