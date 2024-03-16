namespace Blazor.DriverJs.Models;

public record DriverModel(IEnumerable<PopoverModel> Steps, DriverConfigurationModel? Configuration);
