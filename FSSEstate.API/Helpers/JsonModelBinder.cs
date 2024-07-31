using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace FSSEstate.API.Helpers;

public class JsonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult != ValueProviderResult.None)
        {
            var value = valueProviderResult.FirstValue;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    var result = JsonConvert.DeserializeObject(value, bindingContext.ModelType);
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
                catch (JsonException)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid JSON format");
                }
            }
        }
        return Task.CompletedTask;
    }
}
