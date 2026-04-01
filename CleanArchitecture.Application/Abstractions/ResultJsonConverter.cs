using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CleanArchitecture.Application.Abstractions;

public class ResultJsonConverter<T> : JsonConverter<Result<T>>
{
    private class ResultDTO
    {
        public bool Success { get; set; }
        public T? Value { get; set; }
        public ErrorMessage? Message { get; set; }
    };

    public override Result<T>? ReadJson(JsonReader reader, Type objectType, Result<T>? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var dto = serializer.Deserialize<ResultDTO?>(reader);

        if (dto is null)
            return null;

        else if (dto.Success && dto.Value is not null)
            return Result<T>.Ok(dto.Value);
        else
            return Result<T>.Error(dto.Message ?? GeneralErrors.UnexpectedFailure);
    }

    public override void WriteJson(JsonWriter writer, Result<T>? value, JsonSerializer serializer)
    {
        if (value?.Success ?? false)
            serializer.Serialize(writer, new ResultDTO { Success = true, Value = value.Value, Message = value.Message });
        else
            serializer.Serialize(writer, new ResultDTO { Success = false, Value = default, Message = value?.Message });
    }
}
