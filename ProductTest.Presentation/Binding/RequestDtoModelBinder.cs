// using System.ComponentModel;
// using System.Reflection;
// using System.Text.Json;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.ModelBinding;

// namespace ProductTest.Presentation.Binding;

// public sealed class RequestDtoModelBinder : IModelBinder
// {
//     private static readonly JsonSerializerOptions JsonSerializerOptions = new()
//     {
//         PropertyNameCaseInsensitive = true
//     };

//     public async Task BindModelAsync(ModelBindingContext bindingContext)
//     {
//         var modelType = bindingContext.ModelType;
//         var model = await CreateModelAsync(bindingContext, modelType);

//         foreach (var property in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
//         {
//             if (!property.CanWrite)
//             {
//                 continue;
//             }

//             var rawValue = GetBindingValue(bindingContext, property);

//             if (string.IsNullOrWhiteSpace(rawValue))
//             {
//                 continue;
//             }

//             var convertedValue = ConvertValue(rawValue, property.PropertyType);
//             property.SetValue(model, convertedValue);
//         }

//         bindingContext.Result = ModelBindingResult.Success(model);
//     }

//     private static async Task<object> CreateModelAsync(ModelBindingContext bindingContext, Type modelType)
//     {
//         if (RequiresBodyBinding(modelType) &&
//             bindingContext.HttpContext.Request.ContentLength is > 0)
//         {
//             bindingContext.HttpContext.Request.EnableBuffering();

//             var model = await JsonSerializer.DeserializeAsync(
//                 bindingContext.HttpContext.Request.Body,
//                 modelType,
//                 JsonSerializerOptions,
//                 bindingContext.HttpContext.RequestAborted);

//             bindingContext.HttpContext.Request.Body.Position = 0;

//             if (model is not null)
//             {
//                 return model;
//             }
//         }

//         return Activator.CreateInstance(modelType)
//             ?? throw new InvalidOperationException($"Cannot create instance of {modelType.Name}.");
//     }

//     private static bool RequiresBodyBinding(Type modelType) =>
//         modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
//             .Any(property =>
//                 property.GetCustomAttribute<FromRouteAttribute>() is null &&
//                 property.GetCustomAttribute<FromQueryAttribute>() is null);

//     private static string? GetBindingValue(ModelBindingContext bindingContext, PropertyInfo property)
//     {
//         var fromRoute = property.GetCustomAttribute<FromRouteAttribute>();
//         if (fromRoute is not null)
//         {
//             var routeKey = string.IsNullOrWhiteSpace(fromRoute.Name) ? property.Name : fromRoute.Name;
//             return bindingContext.ActionContext.RouteData.Values[routeKey]?.ToString();
//         }

//         var fromQuery = property.GetCustomAttribute<FromQueryAttribute>();
//         if (fromQuery is not null)
//         {
//             var queryKey = string.IsNullOrWhiteSpace(fromQuery.Name) ? property.Name : fromQuery.Name;
//             return bindingContext.HttpContext.Request.Query[queryKey].ToString();
//         }

//         return null;
//     }

//     private static object? ConvertValue(string rawValue, Type targetType)
//     {
//         var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

//         if (underlyingType == typeof(string))
//         {
//             return rawValue;
//         }

//         var converter = TypeDescriptor.GetConverter(underlyingType);
//         return converter.ConvertFromInvariantString(rawValue);
//     }
// }
