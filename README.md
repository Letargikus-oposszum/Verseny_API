# Verseny_API
yes
The error:
System.NotSupportedException: Deserialization of interface or abstract types is not supported. Type 'System.Net.Http.IHttpClientFactory'. Path: $ | LineNumber: 0 | BytePositionInLine: 1.
 ---> System.NotSupportedException: Deserialization of interface or abstract types is not supported. Type 'System.Net.Http.IHttpClientFactory'.
   --- End of inner exception stack trace ---
   at System.Text.Json.ThrowHelper.ThrowNotSupportedException(ReadStack& state, Utf8JsonReader& reader, Exception innerException)
   at System.Text.Json.ThrowHelper.ThrowNotSupportedException_DeserializeNoConstructor(JsonTypeInfo typeInfo, Utf8JsonReader& reader, ReadStack& state)
   at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value)
   at System.Text.Json.Serialization.JsonConverter`1.TryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value, Boolean& isPopulatedValue)
   at System.Text.Json.Serialization.JsonConverter`1.ReadCore(Utf8JsonReader& reader, T& value, JsonSerializerOptions options, ReadStack& state)
   at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.ContinueDeserialize(ReadBufferState& bufferState, JsonReaderState& jsonReaderState, ReadStack& readStack, T& value)
   at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.DeserializeAsync(Stream utf8Json, CancellationToken cancellationToken)
   at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.DeserializeAsObjectAsync(Stream utf8Json, CancellationToken cancellationToken)
   at Microsoft.AspNetCore.Http.HttpRequestJsonExtensions.ReadFromJsonAsync(HttpRequest request, JsonTypeInfo jsonTypeInfo, CancellationToken cancellationToken)
   at Microsoft.AspNetCore.Http.HttpRequestJsonExtensions.ReadFromJsonAsync(HttpRequest request, JsonTypeInfo jsonTypeInfo, CancellationToken cancellationToken)
   at Microsoft.AspNetCore.Http.RequestDelegateFactory.<HandleRequestBodyAndCompileRequestDelegateForJson>g__TryReadBodyAsync|101_0(HttpContext httpContext, Type bodyType, String parameterTypeName, String parameterName, Boolean allowEmptyRequestBody, Boolean throwOnBadRequest, JsonTypeInfo jsonTypeInfo)
   at Microsoft.AspNetCore.Http.RequestDelegateFactory.<>c__DisplayClass101_2.<<HandleRequestBodyAndCompileRequestDelegateForJson>b__2>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
